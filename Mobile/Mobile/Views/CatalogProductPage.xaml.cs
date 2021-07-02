using Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogProductPage : BaseView
    {
        public CatalogProductPage()
        {
            InitializeComponent();
            this.BindingContext = new CatalogProductViewModel();
        }
    }
}