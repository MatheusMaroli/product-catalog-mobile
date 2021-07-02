
using Mobile.Models;
using Mobile.Repository;
using Mobile.Views;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class CatalogViewModel : BaseViewModel
    {

        private TagRepository _repository;

        private ObservableCollection<Tag> _tags;
        public ObservableCollection<Tag> Tags
        {
            get => _tags;
            set => SetProperty(ref _tags, value); 
        }

        public Command GoToCatalogProductCommand { get; set; }

        public CatalogViewModel()
        {
            _repository = new TagRepository(_catalogApi);
            GoToCatalogProductCommand = new Command(async (parameter) =>
               await Shell.Current.GoToAsync($"{nameof(CatalogProductPage)}?{nameof(CatalogProductViewModel.TagId)}={(int)parameter}"));
            Title = "Catálogo";
        }

        protected override void OnAppearing(object param)
        {            
            LoadTags();
        }

        private async void LoadTags()
        {
            try
            {
                IsBusy = true;
                var tags = await _repository.GetAll();
                Tags = new ObservableCollection<Tag>(tags);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                ShowFailNotification("Ocorreu uma falha para carregar o catálogo");
            }
            finally
            {
                IsBusy = false;
            }
           
        }
    }
}
