using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using BetterER.Controller;
using BetterER.Models;
using BetterER.MVVM;
using ControlzEx.Theming;

namespace BetterER.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly ConfigurationController<GlobalSettings> _configurationController;

        private Dictionary<string, string> _languageDictionary;
        public Dictionary<string, string> LanguageDictionary
        {
            get => _languageDictionary;
            set { _languageDictionary = value; OnPropertyChanged(); }
        }

        private GlobalSettings _globalSettings;
        public GlobalSettings GlobalSettings
        {
            get => _globalSettings;
            set
            {
                _globalSettings = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand AbortCommand { get; }
        public RelayCommand SelectDefaultStoragePathCommand { get; }

        public event EventHandler DialogResultTrueRequest;
        public event EventHandler DialogResultFalseRequest;

        public SettingsViewModel(string title) : base(title)
        {
            _configurationController = new ConfigurationController<GlobalSettings>();
            _configurationController.ConfigFileName = "GlobalSettings.json";
            LanguageDictionary = new Dictionary<string, string> { { "de-DE", "Deutsch" }, { "en-EN", "English" } };
            GlobalSettings = new GlobalSettings();
            LoadSettings();

            SaveCommand = new RelayCommand(Save, CanSave);
            AbortCommand = new RelayCommand(Abort);
            SelectDefaultStoragePathCommand = new RelayCommand(SelectDefaultStoragePath);
        }

        private void SelectDefaultStoragePath()
        {
            var browserDialog = new FolderBrowserDialog();
            browserDialog.Description = Properties.strings.SelectDefaultStoragePath;
            var result = browserDialog.ShowDialog();
            if (result == DialogResult.OK)
                GlobalSettings.DefaultStoragePath = browserDialog.SelectedPath;
        }

        private void Abort()
        {
            DialogResultFalseRequest.Invoke(this, new EventArgs());
        }

        private bool CanSave()
        {
            return true;
        }

        private void Save()
        {
            try
            {
                _configurationController.Save(GlobalSettings);
                if (GlobalSettings.DarkModeEnabled)
                    ThemeManager.Current.ChangeTheme(System.Windows.Application.Current, "Dark.Blue");
                else
                    ThemeManager.Current.ChangeTheme(System.Windows.Application.Current, "Light.Blue");

                DialogResultTrueRequest.Invoke(this, new EventArgs());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        private void LoadSettings()
        {
            try
            {
                GlobalSettings = _configurationController.Load();
                if (GlobalSettings.DefaultStoragePath == null || string.IsNullOrWhiteSpace(GlobalSettings.LanguageKey))
                    SetSettings();
            }
            catch (Exception)
            {
                SetSettings();
            }
        }

        private void SetSettings()
        {
            GlobalSettings.LanguageKey = "en-EN";
            GlobalSettings.DefaultStoragePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BetterER", "Diagrams");
            _configurationController.Save(GlobalSettings);
        }
    }
}
