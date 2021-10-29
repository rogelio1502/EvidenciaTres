using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace EvidenciaTres
{
    public partial class RegistrarVenta : Form
    {
        public RegistrarVenta()
        {
            InitializeComponent();
        }

        private void registrarVentaClosed(object sender, FormClosedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Dispose();
        }

        private int toInt(Object str)
        {
            return Convert.ToInt32(str);
        }

        private void changeStateAndValueNUP(NumericUpDown numericUpDown)
        {
            if (numericUpDown.Enabled)
            {
                numericUpDown.Enabled = false;
                numericUpDown.Value = 0;

            }
            else
            {
                numericUpDown.Enabled = true;
            }
        }

        private int getPrice(Label p)
        {
            int precio = toInt(p.Text.Replace("$", ""));

            return precio;
        }

        private int getQuantity(NumericUpDown nup)
        {
            decimal q = nup.Value;

            int cantidad = toInt(q);
            return cantidad;
        }

        private void addItem(NumericUpDown nup, Label p, Label name, List<Item> items)
        {

            int cantidad = getQuantity(nup);
            int precio = getPrice(p);
            int subtotal = cantidad * precio;
            string nombre = name.Text;
            Item item = new Item() { Name = nombre, Precio = precio, Q = cantidad, SubTotal= subtotal };
            items.Add(item);
        }

        private void changeTotal()
        {
            List<Label> labelsSubTotales = new List<Label>();

            int totalVenta = 0;

            labelsSubTotales.Add(lbltotal1);
            labelsSubTotales.Add(lbltotal2);
            labelsSubTotales.Add(lbltotal3);
            labelsSubTotales.Add(lbltotal4);
            labelsSubTotales.Add(lbltotal5);

            foreach (var total in labelsSubTotales)
            {
                totalVenta += toInt(total.Text);
            }

            lbltotaltotal.Text = Convert.ToString(totalVenta);

        }
        private int getTotal()
        {
            return toInt(lbltotaltotal.Text);
        }

        private void changeSubTotal(Label subtotal, Label precio, NumericUpDown nup)
        {

            int _cantidad = getQuantity(nup);

            int _precio = getPrice(precio);

            int _subtotal = _cantidad * _precio;

            subtotal.Text = Convert.ToString(_subtotal);
            
            changeTotal();


        }

        private void cb1_CheckedChanged(object sender, EventArgs e)
        {
            changeStateAndValueNUP(numericUpDown1);
        }

        private void cb2_CheckedChanged(object sender, EventArgs e)
        {
            changeStateAndValueNUP(numericUpDown2);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            changeStateAndValueNUP(numericUpDown3);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            changeStateAndValueNUP(numericUpDown4);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            changeStateAndValueNUP(numericUpDown5);
        }
        

        private void terminarComprabtn_Click(object sender, EventArgs e)
        {
            List<Item> items = new List<Item>();
            if (cb1.Checked)
            {
                addItem(numericUpDown1, lblprecio1, lbl1, items);
                
            }
            if (cb2.Checked)
            {
                addItem(numericUpDown2, lblprecio2, lbl2, items);

            }
            if (checkBox1.Checked)
            {
                addItem(numericUpDown3, lblprecio3, lbl3, items);

            }
            if (checkBox2.Checked)
            {
                addItem(numericUpDown4, lblprecio4, lbl4, items);

            }
            if (checkBox3.Checked)
            {
                addItem(numericUpDown5, lblprecio5, lbl5, items);

            }
            if(items.Count > 0)
            {
                bool everything_correct = true ;
                foreach(Item item in items)
                {
                    if(item.Q == 0)
                    {
                        MessageBox.Show("Tienes un artículo agregado, pero no especificas cantidad...");
                        everything_correct = false;
                        break;
                    }

                }
                if (everything_correct)
                {
                    Venta venta = new Venta { Items = items,Total = getTotal() };
                    bool done = venta.registrarVenta();
                    if (done)
                    {
                        MessageBox.Show("Compra realizada con exito");

                        Menu menu = new Menu();
                        menu.Show();
                        this.Dispose();
                    }
                    
                    
                }

            }
            else
            {
                MessageBox.Show("Ningun artículo añadido aún...");
            }
        }

        

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            changeSubTotal(lbltotal1, lblprecio1, numericUpDown1);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            changeSubTotal(lbltotal2, lblprecio2, numericUpDown2);

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            changeSubTotal(lbltotal3, lblprecio3, numericUpDown3);

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            changeSubTotal(lbltotal4, lblprecio4, numericUpDown4);

        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            changeSubTotal(lbltotal5, lblprecio5, numericUpDown5);

        }
    }
    class Item {
        
        public string Name {
            get; 
            set; 
        }

        
        public int Q { 
            get; 
            set; 
        }

        public int Precio { 
            get; 
            set; 
        }

        public int SubTotal
        {
            get;
            set;
        }
    }
    class Venta
    {
        public List<Item> Items { get; set; }

        public List<Item> Ventas = new List<Item>();

        public int Total { get; set; }
        
        public bool registrarVenta()
        {
            try
            {
                using (StreamReader r = new StreamReader(@"C:\FilesC#\transacciones.json"))
                {
                    string json = r.ReadToEnd();
                    List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                    if(items != null)
                    {
                        foreach (Item item in items)
                        {
                            Ventas.Add(item);
                        }
                    }
                    
                }
            }
            catch (System.IO.FileNotFoundException )
            {
                
                //toleramos el error
            }

            try
            {
                foreach (Item item in Items)
                {
                    Ventas.Add(item);
                }
                string json_data = JsonConvert.SerializeObject(Ventas);
                File.WriteAllText(@"C:\FilesC#\transacciones.json", json_data);
                
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
                return false;

            }
            finally
            {
                generarTicket();
            }
            return true;
        }
        public void generarTicket()
        {
            string fecha_actual = DateTime.Now.ToString("dd-MM-yyyy");
            string hora_actual = DateTime.Now.ToString("hh:mm:ss tt");

            string ticketTexto = String.Format("Compra Realizada el {0} a las {1}",
                DateTime.Now.ToString("dd-MM-yyyy"), 
                DateTime.Now.ToString("hh:mm:ss tt"));
            ticketTexto += "\n";
            ticketTexto += "Producto\t\tPrecio\tCantidad\tSubtotal";
             
            foreach (Item item in Items)
            {
                String itemTexto;
                ticketTexto += "\n";
                itemTexto = String.Format("{0}\t\t{1}\t{2}\t\t{3}", item.Name, item.Precio, item.Q, item.SubTotal);
                ticketTexto += itemTexto;
                
            }
            ticketTexto += String.Format("\n\t\t\t\t\tTotal = {0}",Total);
            string hora_path = hora_actual.Replace(" ", "-");
            hora_path = hora_path.Replace(":", "-");
            string path = String.Format(@"C:\FilesC#\ticketVenta{0}{1}.txt",fecha_actual,hora_path);
            File.WriteAllText(path,ticketTexto);
            MessageBox.Show(String.Format("Ticket Generado con éxito.\nSe ha guardado en\n{0}", path));
        }

    }
    

}
