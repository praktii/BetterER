using BetterER.MVVM;
using System;

namespace BetterER.ViewModels
{
    public class ReportErrorViewModel : BaseViewModel
    {
        public RelayCommand SendCommand { get; }
        public RelayCommand AbortCommand { get; }

        public event EventHandler DialogResultTrueRequest;
        public event EventHandler DialogResultFalseRequest;

        public ReportErrorViewModel(string title) : base(title)
        {
            SendCommand = new RelayCommand(Send, CanSend);
            AbortCommand = new RelayCommand(Abort);
        }

        private void Abort()
        {
            DialogResultFalseRequest.Invoke(this, new EventArgs());
        }

        private void Send()
        {
            DialogResultTrueRequest.Invoke(this, new EventArgs());
        }

        private bool CanSend()
        {
            return true;
        }
    }
}
