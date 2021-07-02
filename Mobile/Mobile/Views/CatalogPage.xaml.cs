
using Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogPage : BaseView
    {
        public CatalogPage()
        {
            InitializeComponent();
            this.BindingContext = new CatalogViewModel();
        }
    }
}