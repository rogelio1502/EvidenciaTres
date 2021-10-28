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
    public partial class RegistrarProductos : Form
    {
        public RegistrarProductos()
        {
            InitializeComponent();
        }

        private void registrarProductosClosed(object sender, FormClosedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Dispose();
        }

        private void tbcodigoKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbpriceKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            //only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void tbbotonKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnregistrar_Click(object sender, EventArgs e)
        {

            string product_id = tbcodigo.Text;
            string name = tbnombre.Text;
            string price = tbprice.Text;
            string stock = tbstock.Text;
            String texto;
            if(product_id != "" && name != "" && price != "" && stock != "")
            {
                texto = String.Format("{0}\n{1}\n{2}\n{3}", product_id, name, price, stock);

            }
            else
            {
                texto = "Datos Incompletos.";
            }

            MessageBox.Show(texto);
        }
    }
}
