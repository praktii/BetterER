using BetterER.Controller.Contracts;
using BetterER.Dialog;
using BetterER.ViewModels;

namespace BetterER.Controller
{
    public class WindowController : IWindowController
    {
        public void ShowSettingsWindow()
        {
            var baseDialogViewModel = new BaseDialogViewModel("Einstellungen")
            {
                CurrentViewModel = new SettingsViewModel("Einstellungen")
            };
            var baseDialog = new BaseDialogWindow{DataContext = baseDialogViewModel};
            baseDialog.ShowDialog();
        }

        public void ShowAboutWindow()
        {
            var baseDialogViewModel = new BaseDialogViewModel("Über BetterER")
            {
                CurrentViewModel = new AboutViewModel("Über BetterER")
            };
            var baseDialog = new BaseDialogWindow { DataContext = baseDialogViewModel };
            baseDialog.ShowDialog();
        }

        public void ShowReportErrorWindow()
        {
            var baseDialogViewModel = new BaseDialogViewModel("Fehler melden")
            {
                CurrentViewModel = new ReportErrorViewModel("Fehler melden")
            };
            var baseDialog = new BaseDialogWindow { DataContext = baseDialogViewModel };
            baseDialog.ShowDialog();
        }
    }
}
