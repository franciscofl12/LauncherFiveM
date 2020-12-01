using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Xisko
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
            if (Directory.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "fivem", "FiveM.app")) == true)
            {
                Application.Run(new ServerAPI());
            }
            else
            {
                MessageBox.Show("No se ha podido encontrar FiveM instalado en el Sistema, si piensa que es un problema , pongasé en contacto con el desarrollador.");
            }
        }
    }
}
