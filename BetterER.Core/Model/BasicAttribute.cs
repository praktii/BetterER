namespace BetterER.Core.Model
{
    public class BasicAttribute
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool NotNull { get; set; }
        public string Default { get; set; }
        public bool IsPrimary { get; set; }
    }
}
