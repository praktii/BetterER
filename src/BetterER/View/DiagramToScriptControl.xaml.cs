using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Reflection;
using System.Windows.Controls;
using System.Xml;

namespace BetterER.View
{
    /// <summary>
    /// Interaction logic for DiagramToScriptControl.xaml
    /// </summary>
    public partial class DiagramToScriptControl : UserControl
    {
        public DiagramToScriptControl()
        {
            InitializeComponent();
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BetterER.sql.xshd"))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    ScriptAvalonEdit.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
        }
    }
}
