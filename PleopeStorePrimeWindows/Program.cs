using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

namespace PleopeStorePrimeWindows
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmAutenticar frmAutenticar = new frmAutenticar();
            DialogResult resposta =  frmAutenticar.ShowDialog();

            if (resposta == DialogResult.OK)
            {
                Application.Run(new frmPrincipal());
            }
        }
    }
}
