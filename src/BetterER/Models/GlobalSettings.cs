using System;

namespace BetterER.Models
{
    [Serializable]
    public class GlobalSettings
    {
        public string LanguageKey { get; set; }
        public string DefaultStoragePath { get; set; }
        public bool DarkModeEnabled { get; set; }
    }
}
