using BetterER.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace BetterER
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        [STAThread]
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var splashScreen = new Dialog.SplashScreen();
            this.MainWindow = splashScreen;
            splashScreen.Show();

            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(1500);
                this.Dispatcher.Invoke(() =>
                {
                    var mainWindow = new MainWindow();
                    var mainViewModel = new MainViewModel("BetterER");
                    this.MainWindow = mainWindow;
                    mainWindow.DataContext = mainViewModel;
                    mainWindow.Show();
                    splashScreen.Close();
                });
            });
        }
    }
}
