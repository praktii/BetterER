using BetterER.Controller.Contracts;
using BetterER.Dialog;
using BetterER.ViewModels;

namespace BetterER.Controller
{
    public class WindowController : IWindowController
    {
        public void ShowSettingsWindow()
        {
            var baseDialogViewModel = new BaseDialogViewModel(Properties.strings.Settings);
            var settingsViewModel = new SettingsViewModel(Properties.strings.Settings);
            baseDialogViewModel.CurrentViewModel = settingsViewModel;
            var baseDialog = new BaseDialogWindow { DataContext = baseDialogViewModel };
            settingsViewModel.DialogResultFalseRequest += baseDialog.OnDialogResultFalse;
            settingsViewModel.DialogResultTrueRequest += baseDialog.OnDialogResultTrue;
            var result = baseDialog.ShowDialog();
        }

        public void ShowAboutWindow()
        {
            var baseDialogViewModel = new BaseDialogViewModel(Properties.strings.About);
            var aboutViewModel = new AboutViewModel(Properties.strings.About);
            baseDialogViewModel.CurrentViewModel = aboutViewModel;
            var baseDialog = new BaseDialogWindow { DataContext = baseDialogViewModel };
            aboutViewModel.CloseRequest += baseDialog.OnCloseDialog;
            var result = baseDialog.ShowDialog();
        }

        public void ShowReportErrorWindow()
        {
            var baseDialogViewModel = new BaseDialogViewModel(Properties.strings.ReportError);
            var reportErrorViewModel= new ReportErrorViewModel(Properties.strings.ReportError);
            baseDialogViewModel.CurrentViewModel = reportErrorViewModel;
            var baseDialog = new BaseDialogWindow { DataContext = baseDialogViewModel };
            reportErrorViewModel.DialogResultFalseRequest += baseDialog.OnDialogResultFalse;
            reportErrorViewModel.DialogResultTrueRequest += baseDialog.OnDialogResultTrue;
            var result = baseDialog.ShowDialog();
        }
    }
}
