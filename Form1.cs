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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_entrar_Click(object sender, EventArgs e)
        {
            if (tbusername.Text == "masterpro" && tbpassword.Text == "passwordpro")
            {
                Menu menu = new Menu();
                this.Hide();
                menu.Show();
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña Incorrectos :'( .");
            }
            


        }

        private void loginClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
