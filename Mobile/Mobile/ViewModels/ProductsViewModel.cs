using Mobile.Models;
using Mobile.Repository;
using Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private ProductRepository _repository;
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        public Command GoToUpdateCommand { get; set; }
        public Command GoToRegisterCommand { get; set; }
        public Command GoToDetailCommand { get; set; }

        public ProductsViewModel()
        {
            Title = "Controle de produtos";
            _repository = new ProductRepository(_catalogApi);
            GoToUpdateCommand = new Command(async (parameter) =>
               await Shell.Current.GoToAsync($"{nameof(NewProductPage)}?{nameof(NewProductViewModel.ProductId)}={(int)parameter}"));
            GoToRegisterCommand = new Command(async() => await Shell.Current.GoToAsync($"{nameof(NewProductPage)}"));
            GoToDetailCommand = new Command(
                async (parameter) => await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?{nameof(ProductDetailViewModel.ProductId)}={(int)parameter}"));



        }

        protected async override void OnAppearing(object param)
        {
            try
            {
                IsBusy = true;
                var products = await _repository.GetAll();
                Products = new ObservableCollection<Product>(products);
                if (!Products.Any())
                    ShowFailNotification("Sistema não possui nenhum produto cadastrado");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
