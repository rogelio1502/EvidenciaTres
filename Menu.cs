using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvidenciaTres
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void menuClosed(object sender, FormClosedEventArgs e)
        {
            
            this.Dispose();
            
            try
            {
                Environment.Exit(0);

            }
            catch (Exception)
            {
                Environment.Exit(0);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistroProveedor rp = new RegistroProveedor();
            rp.Show();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistrarProductos rp = new RegistrarProductos();
            rp.Show();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RegistrarVenta rv = new RegistrarVenta();
            rv.Show();
            this.Dispose();
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Dispose();
        }
    }
}
