using BetterER.MVVM;
using System;

namespace BetterER.ViewModels
{
    public class AboutViewModel: BaseViewModel
    {
        public event EventHandler CloseRequest;

        public RelayCommand CloseCommand { get; }
        public AboutViewModel(string title) : base(title)
        {
            CloseCommand = new RelayCommand(Close);
        }

        private void Close()
        {
            CloseRequest.Invoke(this, new EventArgs());
        }
    }
}
