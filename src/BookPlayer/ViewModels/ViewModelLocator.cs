using CommonServiceLocator;

namespace BookPlayer.ViewModels
{
    /// <summary>
    /// Helper class used to get reference to a view model
    /// </summary>
    public static class ViewModelLocator
    {
        static ViewModelLocator()
        {
        }

        public static PlayerViewModel Player
        {
            get { return ServiceLocator.Current.GetInstance<PlayerViewModel>(); }
        }

        
        public static AboutViewModel About
        {
            get { return ServiceLocator.Current.GetInstance<AboutViewModel>(); }
        }

        public static BookshelfViewModel BookShelf
        {
            get { return ServiceLocator.Current.GetInstance<BookshelfViewModel>(); }
        }

        public static SettingsViewModel Settings
        {
            get { return ServiceLocator.Current.GetInstance<SettingsViewModel>(); }
        }
    }
}
