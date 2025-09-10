using System.Windows.Forms;

namespace PrjRegistrar
{
    public partial class FrmWaitForm : Form
    {
        public FrmWaitForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public FrmWaitForm(Form parent)
        {
            InitializeComponent();
            if (parent != null)
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        public void CloseWaitForm()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
        }
    }
}
