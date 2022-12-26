using MySql.Data.MySqlClient;
using System;
using System.Data; //Buscar consultas
using System.Media; //libreria para reproducir audio
using System.Windows.Forms;

namespace Tienda
{

    public partial class Menu : Form
    {
        //CONECTAR BASE DE DATOS
        public String CadenaConexion = "Database=Tienda;Server=localhost;Uid=root;Pwd=root";

        private MySqlConnection ConectarTienda;

        public void ConectarBD()
        {
            ConectarTienda = new MySqlConnection(CadenaConexion);
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        public Menu()
        {
            InitializeComponent();
            //Salira Menu
            this.toolTip1.SetToolTip(this.Btn_Menu, "Salir");

            //Registrar Producto:
            this.toolTip1.SetToolTip(this.Btn_RegistrarProduct,"Registrar Producto");

            //Registrar Proveedor:
            this.toolTip1.SetToolTip(this.Btn_Provedor,"Registrar Proveedor");

            //Elimina Producto:
            this.toolTip1.SetToolTip(this.Btn_borrar,"Eliminar Producto");

            //Eliminar Proveedor
            this.toolTip1.SetToolTip(this.Elimina_Proveedor, "Eliminar Proveedor");

           
        }

        private void Butn_Regresar_Click(object sender, EventArgs e)
        {
            Inicio regreso = new Inicio();
            regreso.Show();
            this.Hide();
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Btn_play_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.Stop();
            btn_pause.Visible = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer
            {
                SoundLocation = "C:/Users/cardr/Downloads/Imagenes/Interfaz/Gifs/stayin-alive-music.wav"
            };
            player.Play();
            btn_pause.Visible = true;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void Button2_Click_2(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.Stop();

            Inicio ir = new Inicio();
            ir.Show();
            this.Hide();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Operaciones op = new Operaciones();
            op.CargarDatosProveedor(dataGridView1);
            SoundPlayer player = new SoundPlayer();
            player.Stop();

            pnl_GENERAL.Visible = true;
            panelContenedor.Visible = true;

            pnl_1.Visible = false;
            pnl_2.Visible = true;
            pnl_3.Visible = false;
            pnl_4.Visible = false;
            pnl_5.Visible = false;
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            Operaciones op = new Operaciones();
            op.CargarDatosProducto(dataGridView1);
            op.CargarDatosProducto2(comboBox2);
            SoundPlayer player = new SoundPlayer();
            player.Stop();

            pnl_GENERAL.Visible = true;
            panelContenedor.Visible = true;

            pnl_1.Visible = false;
            pnl_2.Visible = false;
            pnl_3.Visible = true;
            pnl_4.Visible = false;
            pnl_5.Visible = false;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Operaciones op = new Operaciones();
            op.CargarDatosProducto(dataGridView1);
            op.CargarDatosProveedor2(comboBox1);

            SoundPlayer player = new SoundPlayer();
            player.Stop();

            pnl_GENERAL.Visible = true;
            panelContenedor.Visible = true;

            pnl_1.Visible = true;
            pnl_2.Visible = false;
            pnl_3.Visible = false;
            pnl_4.Visible = false;
            pnl_5.Visible = false;
        }

        private void Btn_Cash_Click(object sender, EventArgs e)
        {

            Operaciones op = new Operaciones();
            SoundPlayer player = new SoundPlayer();
            op.CargarDatosProveedor3(comboBox3);
            op.CargarDatosProveedor(dataGridView1);
            player.Stop();

            pnl_GENERAL.Visible = true;
            panelContenedor.Visible = true;

            pnl_1.Visible = false;
            pnl_2.Visible = false;
            pnl_3.Visible = false;
            pnl_4.Visible = true;
            pnl_5.Visible = false;
        }

        private void Btn_Buscar_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.Stop();

            pnl_GENERAL.Visible = true;
            panelContenedor.Visible = true;

            dataGridView1.Columns.Clear();
            pnl_1.Visible = false;
            pnl_2.Visible = false;
            pnl_3.Visible = false;
            pnl_4.Visible = false;
            pnl_5.Visible = true;
        }

        private void Limpeza()
        {
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            maskedTextBox7.Text = "";
            textBox4.Text = "";

            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            maskedTextBox3.Text = "";

        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //MANDA LLENAR LOS DATOS DEL PROVEEDOR

        private void Btn_Prvedor_Click(object sender, EventArgs e)
        {
            //Recibe la Cantidad de Registros Almacenados
            int recibeProveedorRegistrado = 0;

            //Instanciamos el objeto de la clase Operaciones
            Operaciones op = new Operaciones();

            //Se envian los datos a almacenar en el metodo almacena producto
            recibeProveedorRegistrado = op.AlmacenaProveedor(textBox5.Text, textBox6.Text, maskedTextBox7.Text, textBox4.Text);

            //Se evalua si se ingreso el registro
            if (recibeProveedorRegistrado > 0)
            {
                //En casos de registro , manda el msj alma8cenado
                MessageBox.Show("Proveedor Almacenado", "Almacenado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // llena el DataGridView
                op.CargarDatosProveedor(dataGridView1);

                //llena el combo box
                op.CargarDatosProveedor2(comboBox1);
                //Se crearonn 2 metodos uno para cada lenado

                //limpia los datos de las cajas 
                Limpeza();
            }

        }

        //MANDA LLENAR LOS DATOS DE PRODUCTO

        private void Btn_RegistrarProduct_Click(object sender, EventArgs e)
        {
            //Recibe la Cantidad de Registros Almacenados
            int recibeProductoRegistrado = 0;

            //Instanciamos el objeto de la clase Operaciones
            Operaciones op = new Operaciones();

            //Se envian los datos a almacenar en el metodo almacena producto
            recibeProductoRegistrado = op.AlmacenaProductos(float.Parse(textBox3.Text), textBox1.Text, Int32.Parse(textBox2.Text), dateTimePicker1.MaxDate, comboBox1.Text, maskedTextBox3.Text);
            // o simplemete comboBox1.Text para usar float en otra se usa

            //Se evalua si se ingreso el registro
            if (recibeProductoRegistrado > 0)
            {
                //En casos de registro , manda el msj almacenado
                MessageBox.Show("Producto Almacenado", "Almacenado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                op.CargarDatosProducto(dataGridView1);

                //limpia los datos de las cajas
                Limpeza();
            }
        }

        private void Timer_Hora_Tick(object sender, EventArgs e)
        {
            lbl_Hora.Text = DateTime.Now.ToLongTimeString();
            lbl_fecha.Text = DateTime.Now.ToShortDateString();
        }

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                textBox6.Focus();
            }
        }

        private void TextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                maskedTextBox7.Focus();
            }
        }

        private void MaskedTextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                textBox4.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                textBox2.Focus();
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                textBox3.Focus();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------//
        //ELIMINAR PRODUCTO//

        private void Btn_borrar_Click(object sender, EventArgs e)
        {
            ConectarBD();
            ConectarTienda.Open();

            //Instanciamos el objeto de la clase Operaciones
            Operaciones op = new Operaciones();

            string cod = comboBox2.Text;
            string cadena = "delete from productos where codigo=" + cod;
            MySqlCommand comando = new MySqlCommand(cadena, ConectarTienda);
            int cant;
            cant = comando.ExecuteNonQuery();
            if (cant == 1)
            {
                MessageBox.Show("Se borró el artículo");
                op.CargarDatosProducto(dataGridView1);
                op.CargarDatosProducto2(comboBox2);
            }
            else
            {
                MessageBox.Show("No existe un artículo con el código ingresado");
                ConectarTienda.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //ELIMINAR PROVEEDOR//

        private void Button2_Click_3(object sender, EventArgs e)
        {
            ConectarBD();
            ConectarTienda.Open();

            //Instanciamos el objeto de la clase Operaciones
            Operaciones op = new Operaciones();
             
            string cod = comboBox3.Text;
            string cadena = "delete from proveedor where idproveedor=" + cod;
            MySqlCommand comando = new MySqlCommand(cadena, ConectarTienda);
            int cant;
            cant = comando.ExecuteNonQuery();
            if (cant == 1)
            {
                MessageBox.Show("Se borro el Proveedor");
                op.CargarDatosProveedor(dataGridView1);
                op.CargarDatosProveedor3(comboBox3);
            }
            else
            {
                MessageBox.Show("No se encontro el Proveedor Seleccionado");
                ConectarTienda.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //BUSCAR UN PRODUCTO POR MEDIO DE SU CODIGO

        private void Button6_Click_1(object sender, EventArgs e)
        {
            ConectarBD();
            ConectarTienda.Open();

            //Instanciamos el objeto de la clase Operaciones
            Operaciones op = new Operaciones();

            string cod = textBox7.Text;
            string consulta = "select * from productos where codigo=" + cod;
            MySqlDataAdapter adaptador = new MySqlDataAdapter(consulta, ConectarTienda);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            MySqlCommand comando = new MySqlCommand(consulta, ConectarTienda);
            MySqlDataReader lector;
            lector = comando.ExecuteReader();
            Limpeza();

            ConectarTienda.Close();
        }


    }
}
