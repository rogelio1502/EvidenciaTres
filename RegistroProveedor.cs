using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.IO;

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
                Proveedor proveedor = new Proveedor()
                {
                    Id=Convert.ToInt32(provider_id),
                    Name = name
                     
                };
                ListaProveedores listaProveedores = new ListaProveedores();
                bool save = listaProveedores.registrarProveedor(proveedor);
                if (save)
                {
                    texto = "Proveedor Guardado con éxito.";
                    clear();
                }
                else
                {
                    texto = "Error al guardar la información.";
                }
            }
            else
            {
                texto = "Datos incompletos";
            }
            MessageBox.Show(texto);
        }
        public void clear()
        {
            tbclave.Text = "";
            tbname.Text = "";
        }
    }
    class ListaProveedores
    {
        public List<Proveedor> Proveedores = new List<Proveedor>();
        public bool registrarProveedor(Proveedor proveedor)
        {
            try
            {
                using (StreamReader r = new StreamReader(@"C:\FilesC#\proveedores.json"))
                {
                    string json = r.ReadToEnd();
                    List<Proveedor> proveedores = JsonConvert.DeserializeObject<List<Proveedor>>(json);
                    if (proveedores != null)
                    {
                        foreach (Proveedor item in proveedores)
                        {
                            Proveedores.Add(item);
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

                Proveedores.Add(proveedor);

                string json_data = JsonConvert.SerializeObject(Proveedores);
                File.WriteAllText(@"C:\FilesC#\proveedores.json", json_data);

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
                return false;

            }

            return true;
        }
    }
    class Proveedor
    {

        public int Id { get; set; }

        public string Name { get; set; }

       


    }
}
