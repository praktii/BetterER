using BetterER.Controller.Contracts;
using BetterER.Dialog;
using BetterER.ViewModels;

namespace BetterER.Controller
{
    public class WindowController : IWindowController
    {
        public void ShowSettingsWindow()
        {
            var baseDialogViewModel = new BaseDialogViewModel(Properties.strings.Settings)
            {
                CurrentViewModel = new SettingsViewModel(Properties.strings.Settings)
            };
            var baseDialog = new BaseDialogWindow{DataContext = baseDialogViewModel};
            baseDialog.ShowDialog();
        }

        public void ShowAboutWindow()
        {
            var baseDialogViewModel = new BaseDialogViewModel(Properties.strings.About)
            {
                CurrentViewModel = new AboutViewModel(Properties.strings.About)
            };
            var baseDialog = new BaseDialogWindow { DataContext = baseDialogViewModel };
            baseDialog.ShowDialog();
        }

        public void ShowReportErrorWindow()
        {
            var baseDialogViewModel = new BaseDialogViewModel(Properties.strings.ReportError)
            {
                CurrentViewModel = new ReportErrorViewModel(Properties.strings.ReportError)
            };
            var baseDialog = new BaseDialogWindow { DataContext = baseDialogViewModel };
            baseDialog.ShowDialog();
        }
    }
}
