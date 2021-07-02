using Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : BaseView
    {
        public ProductsPage()
        {
            InitializeComponent();
            this.BindingContext = new ProductsViewModel();
        }
    }
}