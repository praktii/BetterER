namespace BetterER.ViewModels
{
    public class BaseDialogViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public BaseDialogViewModel(string title) : base(title)
        {
        }
    }
}
