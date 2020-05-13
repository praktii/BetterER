using System;
using System.Collections.Generic;
using BetterER.Controller;
using BetterER.Models;
using BetterER.MVVM;

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

        public event EventHandler DialogResultTrueRequest;
        public event EventHandler DialogResultFalseRequest;

        public SettingsViewModel(string title) : base(title)
        {
            _configurationController = new ConfigurationController<GlobalSettings>();
            _configurationController.ConfigFileName = "GlobalSettings.json";
            LanguageDictionary = new Dictionary<string, string> {{"de-DE", "Deutsch"}, {"en-EN", "English"}};
            GlobalSettings = new GlobalSettings();
            LoadSettings();

            SaveCommand = new RelayCommand(Save, CanSave);
            AbortCommand = new RelayCommand(Abort);
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
                DialogResultTrueRequest.Invoke(this, new EventArgs());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void LoadSettings()
        {
            try
            {
                GlobalSettings = _configurationController.Load();
            }
            catch (Exception)
            {
                GlobalSettings.LanguageKey = "en-EN";
                _configurationController.Save(GlobalSettings);
            }
        }
    }
}
