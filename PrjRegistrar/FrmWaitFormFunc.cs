using System.Threading;
using System.Windows.Forms;

namespace PrjRegistrar
{
    class FrmWaitFormFunc
    {
        FrmWaitForm wait;
        Thread loadthread;

        public void Show()
        {
            loadthread = new Thread(new ThreadStart(LoadingProcess));
            loadthread.Start();
        }

        public void Show(Form parent)
        {
            loadthread = new Thread(new ParameterizedThreadStart(LoadingProcess));
            loadthread.Start(parent);
        }

        public void Close()
        {
            if (wait != null)
            {
                wait.BeginInvoke(new System.Threading.ThreadStart(wait.CloseWaitForm));
                wait = null;
                loadthread = null;
            }
        }

        private void LoadingProcess()
        {
            wait = new FrmWaitForm();
            wait.ShowDialog();
        }

        private void LoadingProcess(object parent)
        {
            Form parent1 = parent as Form;
            wait = new FrmWaitForm(parent1);
            wait.ShowDialog();
        }
    }

    
}
