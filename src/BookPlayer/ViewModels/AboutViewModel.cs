
using Xamarin.Essentials;

namespace BookPlayer.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
        }

        public string Version => VersionTracking.CurrentVersion.ToString();
    }
}