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
    public partial class RegistroProveedor : Form
    {
        public RegistroProveedor()
        {
            InitializeComponent();
        }

        private void registrarProvedoresClosed(object sender, FormClosedEventArgs e)
        {
            Menu menu = new Menu();
            this.Dispose();
            menu.Show();
        }

        private void tbclaveKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnregistrar_Click(object sender, EventArgs e)
        {
            string provider_id = tbclave.Text;
            string name = tbname.Text;
            String texto;
            if ( provider_id != "" && name != "")
            {
                texto = String.Format(
                    "{0}\n{1}",provider_id,name
                );
            }
            else
            {
                texto = "Datos incompletos";
            }
            MessageBox.Show(texto);
        }
    }
}
