using Mobile.Models;
using Mobile.Repository;
using Mobile.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    [QueryProperty("TagId", "TagId")]
    public class CatalogProductViewModel : BaseViewModel
    {

        private TagRepository _repository;

        private int _tagId;
        public int TagId
        {
            get => _tagId;
            set => _tagId = value;
        }

        private ObservableCollection<ProductCatalog> _productCatalog;
        public ObservableCollection<ProductCatalog> ProductCatalog
        {
            get => _productCatalog;
            set => SetProperty(ref _productCatalog, value);
        }

        public Command ShoppingCartCommand { get; set; }
        public Command GoToDetailProductCommand { get; set; }



        public CatalogProductViewModel() {
            Title = "Vitrine de produtos";
            _repository = new TagRepository(_catalogApi);
            ShoppingCartCommand = new Command(() => ShowFailNotification("Método não implementado"));
            GoToDetailProductCommand = new Command(
                async (parameter) => await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?{nameof(ProductDetailViewModel.ProductId)}={(int)parameter}"));

        }


        protected async override void OnAppearing(object param)
        {
            try
            {
                IsBusy = true;
                var productsCatalog = await _repository.GetProductCatalog(TagId);
                ProductCatalog = new ObservableCollection<ProductCatalog>(productsCatalog);       
                
                if (! ProductCatalog.Any())
                {
                    ShowSuccessNotification("Esse catálogo não possui nenhum produto cadastrado", 
                        async( sender, e) => await Shell.Current.GoToAsync(".."));
                }
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.ToString());
                ShowFailNotification("Falha para carregar vítrine de produto");
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}
