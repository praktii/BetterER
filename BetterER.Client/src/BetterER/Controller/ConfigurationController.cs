using System;
using System.IO;
using System.Text.Json;

namespace BetterER.Controller
{
    public class ConfigurationController<T>
    {
        private const string ConfigDirectory = "BetterERConfigurations";
        public string ConfigPath { get; set; }

        private string _configFileName;
        public string ConfigFileName
        {
            get => _configFileName;
            set => _configFileName = Path.Combine(ConfigPath, value);
        }

        public ConfigurationController()
        {
            ConfigPath = Path.Combine(Environment.CurrentDirectory, ConfigDirectory);
            Directory.CreateDirectory(ConfigPath);
        }

        public void Save(T obj)
        {
            var fileName = Path.Combine(ConfigPath, ConfigFileName);
            var jsonString = JsonSerializer.Serialize(obj);
            File.WriteAllText(fileName, jsonString);
        }

        public T Load()
        {
            var filePath = Path.Combine(ConfigPath, ConfigFileName);
            var jsonString = File.ReadAllText(filePath);
            var test = JsonSerializer.Deserialize<T>(jsonString);
            return test;
        }
    }
}
