using BetterER.ViewModels;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using BetterER.Controller;
using BetterER.Models;

namespace BetterER
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly ConfigurationController<GlobalSettings> _configurationController = new ConfigurationController<GlobalSettings>();
        private GlobalSettings _globalSettings;

        [STAThread]
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _globalSettings = new GlobalSettings();
            _configurationController.ConfigFileName = "GlobalSettings.json";
            LoadConfig();
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

        private void LoadConfig()
        {
            try
            {
                _globalSettings = _configurationController.Load();
                Thread.CurrentThread.CurrentCulture = new CultureInfo(_globalSettings.LanguageKey);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(_globalSettings.LanguageKey);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
