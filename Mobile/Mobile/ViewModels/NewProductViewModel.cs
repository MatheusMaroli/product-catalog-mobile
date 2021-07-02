using Mobile.Models;
using Mobile.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    public class NewProductViewModel : BaseViewModel
    {

        private int _productId;
        public int ProductId {
            get => _productId;
            set => SetProperty(ref _productId, value); 
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private string _price; 
        public string Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private List<KeyValuePair<string, string>> _tags;
        public List<KeyValuePair<string, string>> Tags
        {
            get => _tags.ToList();
            set => SetProperty(ref _tags, value);
        }

        private KeyValuePair<string, string> _selectedTag;
        public KeyValuePair<string, string> SelectedTag
        {
            get => _selectedTag;
            set {

                if (!string.IsNullOrEmpty(value.Key))
                    AddProductTag(int.Parse(value.Key), value.Value);
                SetProperty(ref _selectedTag, value);
            }
        }


        private ObservableCollection<Tag> _productTags;
        public ObservableCollection<Tag> ProductTags
        {
            get => _productTags;
            set => SetProperty(ref _productTags, value);
        }

        private byte[] _productImagebyte;
        private ImageSource _productImage;
        public ImageSource ProductImage
        {
            get => _productImage;
            set => SetProperty(ref _productImage, value);
        }


        public Command SaveProductCommand { get; set; }
        public Command DeleteProductTagCommand { get; set; }
        public Command TakePhotoCommand { get; set; }
        public Command Create1000ProductCommand { get; set; }

        private TagRepository _tagRepository;
        private ProductRepository _productRepository;


        public NewProductViewModel()
        {
            Title = "Cadastro de Produto";
            _tagRepository = new TagRepository(_catalogApi);
            _productRepository = new ProductRepository(_catalogApi);
            Tags = new List<KeyValuePair<string, string>>();          
        
            ProductTags = new ObservableCollection<Tag>();
            DeleteProductTagCommand = new Command(DeleteProductTag);
            TakePhotoCommand = new Command(TakeProductPhoto);
            SaveProductCommand = new Command(SalveProduct);
            Create1000ProductCommand = new Command(Create1000);
        }

        private async void Create1000(object obj)
        {
            if ( ! IsRegister())
            {
                ShowFailNotification("Só é possivel criar 1000 produtos em modo de inserção");
                return;
            }


            if (Validate())
            {
                try
                {
                    IsBusy = true;
                    var product = new Product()
                    {
                        Id = ProductId,
                        Name = Name,
                        Description = Description,
                        Price = decimal.Parse(Price),
                        Img = _productImagebyte
                    };                  

                     await _productRepository.Create1000(product, _productTags);
                    ShowSuccessNotification("Criado 1000 produtos",
                        async (sender, e) => await Shell.Current.GoToAsync(".."));
                  

                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.ToString());
                    ShowFailNotification("Falha para realizar o cadastro de 1000 produto");

                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        protected async override void OnAppearing(object param)
        {
            try
            {
                IsBusy = true;
                await LoadTagPicker();
                await LoadProductIfUpdate();
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.ToString());
                ShowFailNotification("Falha para carregar dados do produto");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadProductIfUpdate()
        {
            if (! IsRegister())
            {
                var product = await _productRepository.GetById(ProductId);
                Name = product.Name;
                Description = product.Description;
                Price = product.Price.ToString();
                ProductTags = new ObservableCollection<Tag>(product.Tags);
                _productImagebyte = product.Img;
                ProductImage = ImageSource.FromStream(() => new MemoryStream(_productImagebyte));
            }
        }

        private async Task LoadTagPicker()
        {
            var apiTags = await _tagRepository.GetAll();
            var localTags = new List<KeyValuePair<string, string>>();
            foreach (var currentTag in apiTags)
                localTags.Add(new KeyValuePair<string, string>(currentTag.Id.ToString(), currentTag.Name));
            Tags = new List<KeyValuePair<string, string>>(localTags);
        }

        private async void AddProductTag(int id, string name)
        {
            try
            {
                IsBusy = true;
                var localTags = new List<Tag>(ProductTags);
                localTags.Add(new Tag() { Id = id, Name = name });
                if (!IsRegister())
                    await _productRepository.AddProductTag(ProductId, id);

                ProductTags = new ObservableCollection<Tag>(localTags);              
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                ShowFailNotification("Falha para cadastrar a tag do produto. Tente novamente");
            }
            finally
            {
                IsBusy = false;
            }
   
        }

        private async void DeleteProductTag(object parameter)
        {
            try
            {
                IsBusy = true;

                if (ProductTags.Count <= 2)
                {
                    ShowFailNotification("Produto deve possui pelo menos duas tags");
                    return;
                }

                int tagId = (int)parameter;
                var localTags = new List<Tag>(ProductTags);
                var tagToDelete = localTags.FirstOrDefault(f => f.Id == tagId);

                if (tagToDelete != null)
                {
                    if (! IsRegister())
                        await _productRepository.DeleteTag(ProductId, tagId);

                    localTags.Remove(tagToDelete);
                    ProductTags = new ObservableCollection<Tag>(localTags);
                }
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.ToString());
                ShowFailNotification("Falha para excluir o tag do produto. Tente novamente");
            }
            finally
            {
                IsBusy = false;
            }  
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[input.Length];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private async void TakeProductPhoto()
        {
            try
            {
                var result = await MediaPicker.CapturePhotoAsync();

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    _productImagebyte = ReadFully(stream);
                    ProductImage = ImageSource.FromStream(() => new MemoryStream(_productImagebyte));
                    // ProductImage = ImageSource.FromStream(() => stream);
                }
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.ToString());
                ShowFailNotification("Falha para capturar foto do produto");
            }
            
        }

        private async void SalveProduct()
        {
            if(Validate())
            {
                try
                {
                    IsBusy = true; 
                    var product = new Product()
                    {
                        Id = ProductId,
                        Name = Name,
                        Description = Description,
                        Price = decimal.Parse(Price),
                        Img = _productImagebyte
                    };

                    if (IsRegister())
                    {
               
                        ProductId = await _productRepository.Create(product, _productTags);
                        ShowSuccessNotification("Produto cadastrado com sucesso",
                            async (sender, e) => await Shell.Current.GoToAsync(".."));
                    }
                    else
                    {                  

                        await _productRepository.Update(product);
                        ShowSuccessNotification("Produto Editado com sucesso",
                            async (sender, e) => await Shell.Current.GoToAsync(".."));
                    }

                }
                catch(Exception e)
                {
                    System.Console.WriteLine(e.ToString());
                    ShowFailNotification("Falha para realizar o cadastro do produto");

                }                
                finally
                {
                    IsBusy = false;
                }
            } 
        }

        private bool IsRegister()
        {
            return ProductId <= 0;
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                ShowFailNotification("Nome do produto não foi informado");
                return false;
            }
            else
            if (string.IsNullOrEmpty(Description))
            {
                ShowFailNotification("Descrição do produto não foi informado");
                return false;
            }
            else
            if (string.IsNullOrEmpty(Price))
            {
                ShowFailNotification("Preço do produto não foi informado");
                return false;
            }
            else
            if (ProductTags.Count < 2)
            {
                ShowFailNotification("Produto deve possui pelo menos duas tags");
                return false;
            }
            else
            if (ProductImage == null)
            {
                ShowFailNotification("Tire uma foto para o produto");
                return false;
            }
            else return true;
        }
    }
}
