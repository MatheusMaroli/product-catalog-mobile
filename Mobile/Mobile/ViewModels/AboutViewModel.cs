using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Sobre";
            OpenWebCommand = new Command(async () => 
            await Browser.OpenAsync("https://www.linkedin.com/in/matheus-maroli-b39a41155/"));
            OpenGitCommand = new Command(async () =>
            await Browser.OpenAsync("https://github.com/MatheusMaroli"));
        }

        public ICommand OpenWebCommand { get; }
        public ICommand OpenGitCommand { get; }
    }
}