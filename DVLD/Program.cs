using System;
using System.Windows.Forms;

namespace DVLD
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            frmLogin LoginForm = new frmLogin();

            if (LoginForm.ShowDialog() == DialogResult.OK)
            {
                System.Windows.Forms.Application.Run(
                    new DVLD.Application.frmManageApplications());
            }
        }
    }
}