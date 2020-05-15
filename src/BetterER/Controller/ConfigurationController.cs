using System;
using System.IO;
using Newtonsoft.Json;

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
            if (obj == null)
                throw new ArgumentNullException();

            using (var file = File.CreateText(ConfigFileName))
            {
                var jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(file, obj);
            }
        }

        public T Load()
        {
            using (var file = File.OpenText(ConfigFileName))
            {
                var jsonSerializer = new JsonSerializer();
                return (T)jsonSerializer.Deserialize(file, typeof(T));
            }
        }
    }
}
