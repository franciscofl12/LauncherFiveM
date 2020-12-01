using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using EpEren.Fivem.ServerStatus.ServerAPI;
using System.Linq;
namespace Xisko
{
    public partial class ServerAPI : Form
    {
        public ServerAPI()
        {   
            //if (System.Diagnostics.Process.Start(=fivem")
            InitializeComponent();
        }

        Fivem Server;
        private bool Drag;
        private int MouseX;
        private int MouseY;


        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default: break;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }
        private void PanelMove_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - this.Left;
            MouseY = Cursor.Position.Y - this.Top;
        }
        private void PanelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }

        int contador = 5;

        private void Form1_Load(object sender, EventArgs e)
        {
            Server = new Fivem("151.80.111.185:30120");
            timer1.Start();

            if (Server.GetStatu())
            {
                label6.Text = "Server ON";
                label6.ForeColor = Color.Green;
                UpdatePlayerList();
            }
            else
            {
                label6.Text = "Server ON";
                label6.ForeColor = Color.Red;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            contador--;

            if (contador <= 0)
            {
                Server = new Fivem("151.80.111.185:30120");
                if (Server != null)
                {
                    UpdatePlayerList();
                }
                contador = 5;
            }
        }

        public void UpdatePlayerList()
        {
            label3.Text = "Conectados: " + Server.GetOnlinePlayersCount().ToString() + " / " + Server.GetMaxPlayersCount();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void play_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"fivem://cfx.re/join/om4z9r");
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"ts3server://ravensrp");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"discord://discord.gg/ravensrp/755861660610330626");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string basecarpetas = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "fivem", "FiveM.app");

            var carpetas = new string[]
            {
            "FiveM Application Data",
            "cache",
            "browser",
            "db",
            "dunno",
            "priv",
            "servers",
            "subprocess"
            };

            foreach (var carpeta in carpetas)
            {
                var toDelete = System.IO.Path.Combine(basecarpetas, carpeta);
                                  
                if (Directory.Exists(toDelete))
                {
                    Directory.Delete(toDelete, true);
                    MessageBox.Show("Cache borrado!");
                }
                else
                {
                    MessageBox.Show("El cache no se borro. Tal vez esté ya borrado o la ruta del juego no es la de por defecto.");
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"http://github.com/franciscofl12");
        }
    }
}
