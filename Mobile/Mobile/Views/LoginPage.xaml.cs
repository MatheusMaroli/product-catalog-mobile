using Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : BaseView
    {
        public LoginPage() : base()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }
}