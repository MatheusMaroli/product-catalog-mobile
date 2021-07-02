using Mobile.Models;
using Mobile.Repository;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    public class ProductDetailViewModel : BaseViewModel
    {
        private int _productId;
        public int  ProductId
        {
            get => _productId;
            set => _productId = value;
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

        private string  _price;
        public string Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private ImageSource _img;
        public ImageSource Img { 
            get => _img; 
            set =>SetProperty(ref _img, value); 
        }

        private ObservableCollection<Tag> _tags;
        public ObservableCollection<Tag> Tags 
        { 
            get => _tags; 
            set => SetProperty(ref _tags, value); 
        }

        private readonly ProductRepository _repository;
        public ProductDetailViewModel()
        {
            Title = "Detalhe do produto";
            Tags = new ObservableCollection<Tag>();
            _repository = new ProductRepository(_catalogApi);
        }

        protected async override void OnAppearing(object param)
        {
            try
            {
                IsBusy = true;
                var product = await _repository.GetById(ProductId);
                Name = product.Name;
                Description = product.Description;
                Price = $"R$ {product.Price}";
                Img = ImageSource.FromStream(() => new MemoryStream(product.Img));
                Tags = new ObservableCollection<Tag>(product.Tags);
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
    }
}
