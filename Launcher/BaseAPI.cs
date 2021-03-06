﻿using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using EpEren.Fivem.ServerStatus.BaseAPI;
using System.Linq;
namespace Xisko
{
    public partial class BaseAPI : Form
    {
        public BaseAPI()
        { 
            InitializeComponent();
        }

        Fivem Server;
        int contador = 5;

        private void Form1_Load(object sender, EventArgs e)
        {
            Server = new Fivem("om4z9r");
            timer1.Start();

            if (Server.GetStatu())
            {
                label2.Text = "Server is Online";
                label2.ForeColor = Color.Green;
                UpdatePlayerList();
            }
            else
            {
                label2.Text = "Server is Offline";
                label2.ForeColor = Color.Red;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            contador--;

            if (contador <= 0)
            {
                Server = new Fivem("om4z9r");
                UpdatePlayerList();
                contador = 5;
            }
        }

            public void UpdatePlayerList()
        {
            label3.Text = "Conectados: "+Server.GetOnlinePlayersCount().ToString() + " / Max: " + Server.GetMaxPlayersCount() +" Jugadores";

        }
    }
}
