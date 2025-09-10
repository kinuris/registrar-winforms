using System.Windows.Forms;

namespace PrjRegistrar
{
    public partial class FrmProfilePicture : Form
    {
        public FrmProfilePicture()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Esc KeyPressed
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
