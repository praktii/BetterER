using System;

namespace BetterER.Dialog
{
    /// <summary>
    /// Interaction logic for BaseDialogWindow.xaml
    /// </summary>
    public partial class BaseDialogWindow
    {
        public BaseDialogWindow()
        {
            InitializeComponent();
        }

        internal void OnDialogResultTrue(object sender, EventArgs e)
        {
            DialogResult = true;
        }

        internal void OnDialogResultFalse(object sender, EventArgs e)
        {
            DialogResult = false;
        }

        internal void OnCloseDialog(object sender, EventArgs e)
        {
            Close();
        }
    }
}
