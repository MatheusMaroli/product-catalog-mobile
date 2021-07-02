using Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewProductPage : BaseView
    {
        public NewProductPage()
        {
            InitializeComponent();
            this.BindingContext = new NewProductViewModel();
        }
    }
}