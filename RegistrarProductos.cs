using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
                Producto producto = new Producto
                {
                    Id = Convert.ToInt32(product_id),
                    Name = name,
                    Price = Convert.ToInt32(price),
                    Stock = Convert.ToInt32(stock)
                };
                ListaProductos productos = new ListaProductos();
                bool save = productos.registrarProducto(producto);
                if (save)
                {
                    texto = "Producto Guardado con éxito";
                    clear();
                }
                else
                {
                    texto = "Error al guardar el producto";
                }


            }
            else
            {
                texto = "Datos Incompletos.";
            }

            MessageBox.Show(texto);
        }
        public void clear()
        {
            tbcodigo.Text = "";
            tbnombre.Text = "";
            tbprice.Text = "";
            tbstock.Text = "";
        }
        
    }
    class ListaProductos
    {
        public List<Producto> Productos = new List<Producto>();
        public bool registrarProducto(Producto producto)
        {
            try
            {
                using (StreamReader r = new StreamReader(@"C:\FilesC#\productos.json"))
                {
                    string json = r.ReadToEnd();
                    List<Producto> productos = JsonConvert.DeserializeObject<List<Producto>>(json);
                    if(productos != null)
                    {
                        foreach (Producto item in productos)
                        {
                            Productos.Add(item);
                        }
                    }
                    
                }
            }
            catch (System.IO.FileNotFoundException)
            {

                //toleramos el error
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            try
            {

                Productos.Add(producto);

                string json_data = JsonConvert.SerializeObject(Productos);
                File.WriteAllText(@"C:\FilesC#\productos.json", json_data);

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
                return false;

            }

            return true;
        }
    }
    class Producto
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int Stock { get; set; }


    }
}
    
