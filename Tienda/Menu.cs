using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data; //Buscar consultas
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Media; //libreria para reproducir audio
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        //Variable pública
        int valorid; //Producto
        int valorid2; //Empleado
        int valorid3; //Proveedor
        private DataTable carrito = new DataTable();

        //-------------------------------------------------------------------------------------------------------------------------------//

        public Menu()
        {
            InitializeComponent();
            //Salira Menu
            this.toolTip1.SetToolTip(this.Btn_Menu, "Cerrar Sesion");

            //Registrs Empleado
            this.toolTip1.SetToolTip(this.RegistrarUsuario, "Registra Empleado");
            //Elimina Empleado
            this.toolTip1.SetToolTip(this.Btn_EliminaUsuarios, "Eliminar Empleado");
            //Modifica Empleado
            this.toolTip1.SetToolTip(this.Btn_ModificaEmpleado, "Modifica Empleado");
            //Busca Empleado
            this.toolTip1.SetToolTip(this.Btn_BuscaUsuarios, "Busca Empleado");
            //Limpiar Campos
            this.toolTip1.SetToolTip(this.button3, "Limpiar Campos");

            //Registrar Producto:
            this.toolTip1.SetToolTip(this.Btn_RegistrarProduct, "Registrar Producto");
            //Elimina Producto:
            this.toolTip1.SetToolTip(this.Btn_borrar, "Eliminar Producto");
            //Buscar Producto:
            this.toolTip1.SetToolTip(this.Btn_Buscar, "Buscar Producto");
            //Modifica Producto:
            this.toolTip1.SetToolTip(this.Btn_ModificaProducto, "Modifica Producto");
            //Limpia Campos
            this.toolTip1.SetToolTip(this.Btn_Clean, "Limpia Campos");

            //Registrar Proveedor:
            this.toolTip1.SetToolTip(this.Btn_RegProveedor, "Registrar Proveedor");
            //Eliminar Proveedor:
            this.toolTip1.SetToolTip(this.Elimina_Proveedor, "Eliminar Proveedor");
            //Limpiar Campos
            this.toolTip1.SetToolTip(this.Btn_Limpiar, "Limpiar Campos");
            //Modificar Proveedor
            this.toolTip1.SetToolTip(this.Btn_ModificaProveedor, "Modificar Proveedor");
            //Buscar Proveedor
            this.toolTip1.SetToolTip(this.Btn_BusProveedor, "Buscar Proveedor");

            //Agregar al carrito
            this.toolTip1.SetToolTip(this.Btn_Carrito, "Agregar al Carrito");
            //Realizar Venta
            this.toolTip1.SetToolTip(this.Btn_Compra, "Realizar Venta");
            //Buscar Producto
            this.toolTip1.SetToolTip(this.Btn_BusquedaVen, "Buscar Venta");
            //Eliminar del Carrito
            this.toolTip1.SetToolTip(this.Btn_EliminarUltimo, "Eliminar del Carrito");
        }

        //Pause y Play PictureBox
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.Stop();
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //    SoundPlayer player = new SoundPlayer();
            //    player.SoundLocation = "./Gats.wav";
            //    player.Play();
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

        //Evalua si esta seguro de salir de la app
        private void Button5_Click(object sender, EventArgs e)
        {
            //SoundPlayer player = new SoundPlayer();
            //player.SoundLocation = "./Fin.wav";
            //player.PlaySync();

            DialogResult result = MessageBox.Show("¿Esta Seguro De Salir de la Aplicacion?", "Salir App", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            //SoundPlayer player = new SoundPlayer();
            //player.Stop();

            Inicio ir = new Inicio();
            ir.Show();
            this.Hide();
        }

        //Btn Proveedor1
        private void Button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            Btn_RegProveedor.Enabled = true;

            Select_Proveedor.Visible = true;
            Select_Empleado.Visible = false;
            Select_Producto.Visible = false;
            Select_Venta.Visible = false;
            Btn_EliminarUltimo.Visible = false;
            panel34.Visible = false;

            Operaciones op = new Operaciones();
            op.CargarDatosProveedor(dataGridView1);
            op.CargarDatosProveedor2(comboBox3);
            op.CargarDatosCaducados(dataGridView2);

            //SoundPlayer player = new SoundPlayer();
            //player.Stop();

            SelectFondo.Visible = false;

            pnl_GENERAL.Visible = true;
            panelContenedor.Visible = true;

            pnl_1.Visible = false;
            pnl_2.Visible = true;
            pnl_3.Visible = false;
            pnl_4.Visible = false;

        }

        //Btn_Productos2
        private void Button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            Btn_RegistrarProduct.Enabled = true;    

            Select_Producto.Visible = true;
            Select_Empleado.Visible = false;
            Select_Venta.Visible = false;
            Select_Proveedor.Visible = false;
            Btn_EliminarUltimo.Visible = false;
            panel34.Visible = false;

            Operaciones op = new Operaciones();

            op.CargarDatosProducto(dataGridView1);
            op.CargarDatosProveedor2(comboBox1);
            op.CargarDatosProducto(comboBox2);
            op.CargarDatosCaducados(dataGridView2);

            //SoundPlayer player = new SoundPlayer();
            //player.Stop();

            SelectFondo.Visible = false;

            pnl_GENERAL.Visible = true;
            panelContenedor.Visible = true;

            pnl_1.Visible = true;
            pnl_2.Visible = false;
            pnl_3.Visible = false;
            pnl_4.Visible = false;

            VerificarCantidadProductos();
        }

        //Btn_Empleado3
        private void Btn_Empleado_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            RegistrarUsuario.Enabled = true;

            Select_Empleado.Visible = true;
            Select_Proveedor.Visible = false;
            Select_Venta.Visible = false;
            Select_Producto.Visible = false;
            Btn_EliminarUltimo.Visible = false;
            panel34.Visible = false;

            Operaciones op = new Operaciones();
            op.CargarDatosUsuario2(dataGridView1);
            op.CargarDatosUsuarios(comboBox4);
            op.CargarDatosCaducados(dataGridView2);

            //SoundPlayer player = new SoundPlayer();
            //player.Stop();

            SelectFondo.Visible = false;

            pnl_GENERAL.Visible = true;
            panelContenedor.Visible = true;

            pnl_1.Visible = false;
            pnl_2.Visible = false;
            pnl_3.Visible = true;
            pnl_4.Visible = false;
        }

        //Btn_RealizarVentas
        private void Btn_Venta_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            Btn_EliminarUltimo.Visible = false;
            panel34.Visible = false;

            Select_Venta.Visible = true;
            Select_Empleado.Visible = false;
            Select_Producto.Visible = false;
            Select_Proveedor.Visible = false;
            Btn_EliminarUltimo.Enabled = true;

            Operaciones op = new Operaciones();
            op.CargarDatosProducto2(cmb_Nombre);
            op.CargarDatosCaducados(dataGridView2);
            op.MostrarRegistrosVenta(dataGridView1);
            ActualizarNumeroVentasHoy();

            pnl_GENERAL.Visible = true;
            panelContenedor.Visible = true;
            SelectFondo.Visible = false;

            pnl_1.Visible = false;
            pnl_2.Visible = false;
            pnl_3.Visible = false;
            pnl_4.Visible = true;

        }

        private void Limpeza()
        {
            txt_nombre.Text = "";
            txt_ApellidoP.Text = "";
            txt_ApellidoM.Text = "";
            txt_nick.Text = "";
            txt_password.Text = "";
            txt_sexo.Text = "";
            txt_salario.Text = "";
            txt_codigo.Text = "";
            MaskedContacto.Text = "";
            comboBox1.Text = "";

            txt_NombreEmp.Text = "";
            txt_Direccion.Text = "";
            MaskedContact.Text = "";
            txt_Ubicacion.Text = "";

            txt_NomProducto.Text = "";
            txt_Cant.Text = "";
            txt_CodProducto.Text = "";
            txt_PrecioProducto.Text = "";

            txt_Efectivo.Text = "";
            txt_Total.Text = "";
            txt_Cambio.Text = "";
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        private void Timer_Hora_Tick(object sender, EventArgs e)
        {
            lbl_Hora.Text = DateTime.Now.ToLongTimeString();
            lbl_fecha.Text = DateTime.Now.ToShortDateString();
        }

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_Direccion.Focus();
            }
        }

        private void TextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_Ubicacion.Focus();
            }
        }

        private void txt_Localidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                MaskedContact.Focus();
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_Cant.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_CodProducto.Focus();
            }
        }

        private void txt_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_ApellidoP.Focus();
            }
        }

        private void txt_nick_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_password.Focus();
            }
        }

        private void txt_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_sexo.Focus();
            }
        }

        private void txt_salario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_codigo.Focus();
            }
        }

        private void txt_ApellidoP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_ApellidoM.Focus();
            }
        }

        private void txt_ApellidoM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                MaskedContacto.Focus();
            }
        }

        private void MaskedContacto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_codigo.Focus();
            }
        }

        private void txt_codigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_nick.Focus();
            }
        }

        private void txt_sexo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_salario.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_PrecioProducto.Focus();
            }
        }


        //Productos
        private void Btn_Clean_Click(object sender, EventArgs e)
        {
            Btn_RegistrarProduct.Enabled = true; 
            Operaciones op = new Operaciones();
            op.CargarDatosProducto(dataGridView1);
            Limpeza();
        }

        //Empleados
        private void button3_Click(object sender, EventArgs e)
        {
            Operaciones op = new Operaciones();
            op.CargarDatosUsuario2(dataGridView1);
            Limpeza();
            RegistrarUsuario.Enabled = true;
        }

        //Proveedor
        private void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            Btn_RegProveedor.Enabled = true;
            Operaciones op = new Operaciones();
            op.CargarDatosProveedor(dataGridView1);
            Limpeza();
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //ALMACENA EMPLEADOS

        private void RegistrarUsuario_Click(object sender, EventArgs e)
        {
            string userInput = txt_nombre.Text.ToLower();
            if (userInput.Contains("administrador"))
            {
                MessageBox.Show("¡No Puede Utilizar el Nombre 'Administrador'", "Error Registro Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Limpeza();
            }
            else
            {
                try
                {
                    //Recibe la Cantidad de Registros Almacenados
                    int recibeEmpleadoRegistrado = 0;

                    //Instanciamos el objeto de la clase Operaciones
                    Operaciones op = new Operaciones();

                    //Se envian los datos a almacenar en el metodo almacena producto
                    recibeEmpleadoRegistrado = op.RegistrarUsuario(txt_nombre.Text, txt_ApellidoP.Text, txt_ApellidoM.Text, Int32.Parse(txt_salario.Text), txt_nick.Text, txt_password.Text, txt_sexo.Text, Int32.Parse(txt_codigo.Text), MaskedContacto.Text);

                    //Se evalua si se ingreso el registro
                    if (recibeEmpleadoRegistrado > 0)
                    {
                        //En casos de registro , manda el msj alma8cenado
                        MessageBox.Show("¡Empleado Almacenado!", "Almacenado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //actualiza combobox de eliminacion
                        op.CargarDatosUsuarios(comboBox4);
                        op.CargarDatosUsuario2(dataGridView1);
                        Limpeza();
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine("{0} Exception caught.", ex);
                    MessageBox.Show("¡Debes de Llenar Todos los Campos 'Correctamente'!", "Error Registro Empleado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //BOTON PARA ELIMINAR EMPLEADOS

        private void Btn_EliminaUsuarios_Click(object sender, EventArgs e)
        {
            ConectarBD();
            ConectarTienda.Open();

            DialogResult result = MessageBox.Show("¿Esta Seguro De Eliminar?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //Instanciamos el objeto de la clase Operaciones
                Operaciones op = new Operaciones();

                string cadena = "delete from usuarios where idusuarios=" + Label_Busqueda2.Text;
                MySqlCommand comando = new MySqlCommand(cadena, ConectarTienda);
                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant > 0)
                {
                    RegistrarUsuario.Enabled = true;
                    MessageBox.Show("¡Empleado Eliminado!", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    op.CargarDatosUsuario2(dataGridView1);
                    op.CargarDatosUsuarios(comboBox4);
                    Limpeza();
                }
                else
                {
                    MessageBox.Show("No existe el Usuario con el Código Ingresado", "!No Existe¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ConectarTienda.Close();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //BOTOS BUSCA EMPLEADOS

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label_Busqueda2.Text = comboBox4.SelectedValue.ToString();
        }

        private void Btn_BuscaUsuarios_Click(object sender, EventArgs e)
        {
            RegistrarUsuario.Enabled = false;

            ConectarBD();
            ConectarTienda.Open();

            //Instanciamos el objeto de la clase Operaciones
            Operaciones op = new Operaciones();

            string cod = Label_Busqueda2.Text;
            string consulta = "select nombre, apellidop, apellidom, salario, nick, pass, sexo, codigo, contacto from usuarios where idusuarios=" + cod;
            MySqlDataAdapter adaptador = new MySqlDataAdapter(consulta, ConectarTienda);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            MySqlCommand comando = new MySqlCommand(consulta, ConectarTienda);
            MySqlDataReader lector;
            lector = comando.ExecuteReader();
            ConectarTienda.Close();

            //Convertimos a int al id
            valorid2 = Convert.ToInt32(Label_Busqueda2.Text);
            //Generamos el vector para recibir valores
            String[] Empleado = new String[9];

            Empleado = op.CargarDatosUsuario3(valorid2);
            txt_nombre.Text = Empleado[0]; //Nombre
            txt_ApellidoP.Text = Empleado[1]; //ApellidoP
            txt_ApellidoM.Text = Empleado[2]; //ApellidoM
            txt_salario.Text = Empleado[3]; //Salario
            txt_nick.Text = Empleado[4]; //Nick
            txt_password.Text = Empleado[5]; //Password
            txt_sexo.Text = Empleado[6]; //Sexo
            txt_codigo.Text = Empleado[7];  //Codigo
            MaskedContacto.Text = Empleado[8]; //Contacto
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //MODIFICA EMPLEADO EMPLEADO

        private void Btn_ModificaEmpleado_Click(object sender, EventArgs e)
        {
            
            try
            {
                //Vaiable para recibir el resultado
                int resActualizacion = 0;
                Operaciones op = new Operaciones();
                //IdPlatillo
                resActualizacion = ModificaUsuario(txt_nombre.Text, txt_ApellidoP.Text, txt_ApellidoM.Text, Int32.Parse(txt_salario.Text), txt_nick.Text, txt_password.Text, txt_sexo.Text, Int32.Parse(txt_codigo.Text), MaskedContacto.Text);
                if (resActualizacion > 0)
                {
                    RegistrarUsuario.Enabled = true;
                    MessageBox.Show("¡Cambios Realizados!", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    op.CargarDatosUsuario2(dataGridView1);
                    op.CargarDatosUsuarios(comboBox4);

                    ConectarTienda.Close();
                    Limpeza();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                MessageBox.Show("¡Primero Realice una 'Busqueda' para poder Modificar!", "Error Modificar Empleado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public int ModificaUsuario(String nombre, String apellidop, String apellidom, int salario, String nick, String pass, String sexo, int codigo, String contacto)
        {
            int res = 0;
            ConectarBD();
            //conectaRestaurante.Open();
            if (ConectarTienda.State == ConnectionState.Closed)
            {
                ConectarTienda.Open();
            }
            //Sentencia para modificar
            MySqlCommand actualizaDatos = new MySqlCommand("update usuarios set nombre=@name,apellidop=@app,apellidom=@apm,salario=@sal,nick=@ni,pass=@password,sexo=@sex,codigo=@cod, contacto=@cont where idusuarios=" + Label_Busqueda2.Text, ConectarTienda);
            actualizaDatos.Parameters.AddWithValue("@name", nombre);
            actualizaDatos.Parameters.AddWithValue("@app", apellidop);
            actualizaDatos.Parameters.AddWithValue("@apm", apellidom);
            actualizaDatos.Parameters.AddWithValue("@sal", salario);
            actualizaDatos.Parameters.AddWithValue("@ni", nick);
            actualizaDatos.Parameters.AddWithValue("@password", pass);
            actualizaDatos.Parameters.AddWithValue("@sex", sexo);
            actualizaDatos.Parameters.AddWithValue("@cod", codigo);
            actualizaDatos.Parameters.AddWithValue("@cont", contacto);

            try
            {
                res = actualizaDatos.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
            ConectarTienda.Close();
            return res;
        }



        //-------------------------------------------------------------------------------------------------------------------------------//

        //MANDA LLENAR LOS DATOS DEL PROVEEDOR

        private void Btn_Prvedor_Click(object sender, EventArgs e)
        {
            // Verifica si todos los campos tienen algún valor
            if (string.IsNullOrEmpty(txt_NombreEmp.Text) ||
                    string.IsNullOrEmpty(txt_Direccion.Text) ||
                    string.IsNullOrEmpty(txt_Ubicacion.Text) ||
                    string.IsNullOrEmpty(MaskedContact.Text))
            {
                MessageBox.Show("¡Debes de Llenar Todos los Campos 'Correctamente'!", "Error Registro Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Recibe la Cantidad de Registros Almacenados
                int recibeProveedorRegistrado = 0;

                //Instanciamos el objeto de la clase Operaciones
                Operaciones op = new Operaciones();

                //Se envian los datos a almacenar en el metodo almacena producto
                recibeProveedorRegistrado = op.AlmacenaProveedor(txt_NombreEmp.Text, txt_Direccion.Text, txt_Ubicacion.Text, MaskedContact.Text);

                //Se evalua si se ingreso el registro
                if (recibeProveedorRegistrado > 0)
                {
                    //En casos de registro , manda el msj alma8cenado
                    MessageBox.Show("¡Proveedor Registrado!", "Almacenado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //llena el DataGridView
                    op.CargarDatosProveedor(dataGridView1);
                    //llena el combo box
                    op.CargarDatosProveedor2(comboBox1);
                    //Se crearonn 2 metodos uno para cada lenado
                    op.CargarDatosProveedor2(comboBox3);
                    //limpia los datos de las cajas 
                    Limpeza();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //ELIMINAR PROVEEDOR//

        private void Elimina_Proveedor_Click(object sender, EventArgs e)
        {
            ConectarBD();
            ConectarTienda.Open();

            DialogResult result = MessageBox.Show("¿Esta Seguro De Eliminar?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //Instanciamos el objeto de la clase Operaciones
                Operaciones op = new Operaciones();

                string cod = Label_Busqueda3.Text;
                string cadena = "delete from proveedor where idproveedor=" + cod;
                MySqlCommand comando = new MySqlCommand(cadena, ConectarTienda);
                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant == 1)
                {
                    Btn_RegProveedor.Enabled = true;
                    MessageBox.Show("¡Proveedor Eliminado!", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    op.CargarDatosProveedor(dataGridView1);
                    op.CargarDatosProveedor2(comboBox3);
                    Limpeza();
                }
                else
                {
                    MessageBox.Show("No se encontro el Proveedor Seleccionado");
                    ConectarTienda.Close();
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label_Busqueda3.Text = comboBox3.SelectedValue.ToString();
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //BUSCA PROVEEDOR

        private void Btn_BuscaProveedor_Click(object sender, EventArgs e)
        {
            Btn_RegProveedor.Enabled = false;

            ConectarBD();
            ConectarTienda.Open();

            //Instanciamos el objeto de la clase Operaciones
            Operaciones op = new Operaciones();

            string cod = Label_Busqueda3.Text;
            string consulta = "select nombre, direccion, ubicacion, contacto from proveedor where idproveedor=" + cod;
            MySqlDataAdapter adaptador = new MySqlDataAdapter(consulta, ConectarTienda);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            MySqlCommand comando = new MySqlCommand(consulta, ConectarTienda);
            MySqlDataReader lector;
            lector = comando.ExecuteReader();
            ConectarTienda.Close();

            //Convertimos a int al id
            valorid3 = Convert.ToInt32(Label_Busqueda3.Text);
            //Generamos el vector para recibir valores
            String[] Proveedor = new String[5];

            Proveedor = op.CargarDatosProveedor3(valorid3);
            txt_NombreEmp.Text = Proveedor[0]; //Nombre
            txt_Direccion.Text = Proveedor[1]; //Direccion
            txt_Ubicacion.Text = Proveedor[2]; //Localidad
            MaskedContact.Text = Proveedor[3]; //Contacto
        }

        private void Btn_ModificaProveedor_Click(object sender, EventArgs e)
        {
            // Verifica si todos los campos tienen algún valor
            if (string.IsNullOrEmpty(txt_NombreEmp.Text) ||
                    string.IsNullOrEmpty(txt_Direccion.Text) ||
                    string.IsNullOrEmpty(txt_Ubicacion.Text) ||
                    string.IsNullOrEmpty(MaskedContact.Text))
            {
                MessageBox.Show("¡Primero Realice una 'Busqueda' para poder Modificar!", "Error Modificar Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Vaiable para recibir el resultado
                int resActualizacion = 0;
                Operaciones op = new Operaciones();
                //IdPlatillo
                resActualizacion = ModificaProveedor(txt_NombreEmp.Text, txt_Direccion.Text, txt_Ubicacion.Text, MaskedContact.Text);
                if (resActualizacion > 0)
                {
                    Btn_RegProveedor.Enabled = true;
                    MessageBox.Show("¡Cambios Realizados!", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    op.CargarDatosProveedor(dataGridView1);
                    op.CargarDatosProveedor2(comboBox3);

                    Limpeza();
                }
            }
        }

        public int ModificaProveedor(String nombre, String direccion, String ubicacion, String contacto)
        {
            int res = 0;
            ConectarBD();
            //conectaRestaurante.Open();
            if (ConectarTienda.State == ConnectionState.Closed)
            {
                ConectarTienda.Open();
            }
            //Sentencia para modificar
            MySqlCommand actualizaDatos = new MySqlCommand("update proveedor set nombre=@name,direccion=@dire, ubicacion=@loc, contacto=@con where idproveedor=" + Label_Busqueda3.Text, ConectarTienda);
            actualizaDatos.Parameters.AddWithValue("@name", nombre);
            actualizaDatos.Parameters.AddWithValue("@dire", direccion);
            actualizaDatos.Parameters.AddWithValue("@loc", ubicacion);
            actualizaDatos.Parameters.AddWithValue("@con", contacto);

            try
            {
                res = actualizaDatos.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
            ConectarTienda.Close();
            return res;
        }



        //-------------------------------------------------------------------------------------------------------------------------------//

        //MANDA LLENAR LOS DATOS DE PRODUCTO
        private void Btn_RegistrarProduct_Click(object sender, EventArgs e)
        {
            ConectarBD();
            ConectarTienda.Open();

            try
            {
                //Recibe la Cantidad de Registros Almacenados
                int recibeProductoRegistrado = 0;

                //Instanciamos el objeto de la clase Operaciones
                Operaciones op = new Operaciones();

                //Se envían los datos a almacenar en el método AlmacenaProductos
                recibeProductoRegistrado = op.AlmacenaProductos(txt_NomProducto.Text, Int32.Parse(Lbl_Proveedor.Text), Int32.Parse(txt_CodProducto.Text), Int32.Parse(txt_Cant.Text), dateTimePicker1.Value, float.Parse(txt_PrecioProducto.Text));

                //Se evalúa si se ingresó el registro
                if (recibeProductoRegistrado > 0)
                {
                    //En caso de registro, muestra el mensaje almacenado
                    MessageBox.Show("¡Producto Almacenado!", "Almacenado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    op.CargarDatosProducto(dataGridView1);
                    op.CargarDatosProducto(comboBox2);
                    op.CargarDatosCaducados(dataGridView2);

                    Limpeza();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                MessageBox.Show("¡Debes de Llenar Todos los Campos 'Correctamente'!", "Error Registro Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ConectarTienda.Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //ELIMINAR PRODUCTO//

        private void Btn_borrar_Click(object sender, EventArgs e)
        {
            ConectarBD();
            ConectarTienda.Open();
            DialogResult result = MessageBox.Show("¿Esta Seguro De Eliminar?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //Instanciamos el objeto de la clase Operaciones
                Operaciones op = new Operaciones();
                string cod = Label_Busqueda.Text;
                string cadena = "delete from productos where idproducto=" + cod;
                MySqlCommand comando = new MySqlCommand(cadena, ConectarTienda);
                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant > 0)
                {
                    Btn_RegistrarProduct.Enabled = true;
                    MessageBox.Show("¡Producto Eliminado!", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    op.CargarDatosProducto(dataGridView1);
                    op.CargarDatosProducto(comboBox2);
                    op.CargarDatosCaducados(dataGridView2);
                    Limpeza();
                }
                else
                {
                    MessageBox.Show("No existe un artículo con el código ingresado");
                    ConectarTienda.Close();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //BUSCAR UN PRODUCTO POR MEDIO DE SU CODIGO

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label_Busqueda.Text = comboBox2.SelectedValue.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lbl_Proveedor.Text = comboBox1.SelectedValue.ToString();
        }

        private void Btn_Buscar_Click(object sender, EventArgs e)
        {
            Btn_RegistrarProduct.Enabled = false;

            ConectarBD();
            ConectarTienda.Open();
            //Instanciamos el objeto de la clase Operaciones
            Operaciones op = new Operaciones();
            string cod = Label_Busqueda.Text;
            string consulta = "SELECT p.nombre AS NombreProducto, p.caducidad AS Caducidad, p.cantidad AS Cantidad, pr.nombre AS Proveedor, p.precio AS Precio, p.codigo AS Codigo FROM productos p INNER JOIN proveedor pr ON p.idproveedor = pr.idproveedor WHERE p.idproducto = " + cod;

            MySqlDataAdapter adaptador = new MySqlDataAdapter(consulta, ConectarTienda);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                string nombreProveedor = dt.Rows[0]["Proveedor"].ToString();
                // Aquí se asigna el nombre del proveedor al ComboBox
                comboBox1.Text = nombreProveedor;
            }

            MySqlCommand comando = new MySqlCommand(consulta, ConectarTienda);
            MySqlDataReader lector;
            lector = comando.ExecuteReader();
            ConectarTienda.Close();

            //Convertimos a int al id
            valorid = Convert.ToInt32(Label_Busqueda.Text);
            //Generamos el vector para recibir valores
            String[] Producto = new String[6];

            Producto = op.CargarDatosProducto3(valorid);
            txt_NomProducto.Text = Producto[0]; //Nombre
            txt_CodProducto.Text = Producto[1]; //codigo
            txt_Cant.Text = Producto[2]; //Cantidad
            dateTimePicker1.Text = Producto[3]; //Caducidad
            Lbl_Proveedor.Text = Producto[4]; //Proveedor
            txt_PrecioProducto.Text = Producto[5]; //Precio
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //BUSCAR UN PRODUCTO POR MEDIO DE SU CODIGO
        private void Btn_ModificaProducto_Click(object sender, EventArgs e)
        {
            try
            {
                //Vaiable para recibir el resultado
                int resActualizacion = 0;
                Operaciones op = new Operaciones();
                //IdPlatillo
                resActualizacion = ModificaProducto3(txt_NomProducto.Text, Int32.Parse(txt_CodProducto.Text), Int32.Parse(txt_Cant.Text), dateTimePicker1.Value, Int32.Parse(Lbl_Proveedor.Text), float.Parse(txt_PrecioProducto.Text));
                if (resActualizacion > 0)
                {
                    Btn_RegistrarProduct.Enabled = true;
                    MessageBox.Show("¡Cambios Realizados!", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    op.CargarDatosProducto(dataGridView1);
                    op.CargarDatosProducto(comboBox2);
                    op.CargarDatosCaducados(dataGridView2);
                    ConectarTienda.Close();
                    Limpeza();

                    op.CargarDatosProveedor2(comboBox1);

                }
            }


            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                MessageBox.Show("¡Primero Realice una 'Busqueda' para poder Modificar!", "Error Modificar Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        //Método para modificar los datos del productos
        public int ModificaProducto3(String nombre, int codigo, int cantidad, DateTime caducidad, int proveedor, float precio)
        {
            int res = 0;
            ConectarBD();
            //ConectaRestaurante.Open();
            if (ConectarTienda.State == ConnectionState.Closed)
            {
                ConectarTienda.Open();
            }
            //Sentencia para modificar
            MySqlCommand actualizaDatos = new MySqlCommand("update productos set nombre=@name,codigo=@cod,cantidad=@can,caducidad=@cad,idproveedor=@prove,precio=@prec where idproducto=" + Label_Busqueda.Text, ConectarTienda);
            actualizaDatos.Parameters.AddWithValue("@name", nombre);
            actualizaDatos.Parameters.AddWithValue("@cod", codigo);
            actualizaDatos.Parameters.AddWithValue("@can", cantidad);
            actualizaDatos.Parameters.AddWithValue("@cad", caducidad);
            actualizaDatos.Parameters.AddWithValue("@prove", proveedor);
            actualizaDatos.Parameters.AddWithValue("@prec", precio);

            try
            {
                res = actualizaDatos.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
            ConectarTienda.Close();
            return res;
        }


        private void Menu_Load(object sender, EventArgs e)
        {
            // Establecer colores en TextBoxes ReadOnly
            txt_Total.ReadOnly = true;
            txt_Total.BackColor = Color.Black;
            txt_Total.ForeColor = Color.Red;

            txt_Cambio.ReadOnly = true;
            txt_Cambio.BackColor = Color.Black;
            txt_Cambio.ForeColor = Color.Chartreuse;

            txt_Precio.ReadOnly = true;
            txt_Precio.BackColor = Color.White;
            txt_Precio.ForeColor = Color.Red;

            txt_Stock.ReadOnly = true;
            txt_Stock.BackColor = Color.White;
            txt_Stock.ForeColor = Color.MediumBlue;

            txt_Ventas.ReadOnly = true;
            txt_Ventas.BackColor = Color.Black;
            txt_Ventas.ForeColor = Color.Red;

            RecuperaImagen();

            //Soluciona ERROR Carga DATAGRIDVIEW
            Operaciones op = new Operaciones();
            op.CargarDatosProveedor(dataGridView1);

            //Filtra y hace cammbios en MENU
            if (label1.Text == "Administrador")
            {
                label1.ForeColor = System.Drawing.Color.Red;
            }

            if (label1.Text != "Administrador")
            {
                label1.ForeColor = System.Drawing.Color.Blue;
                Btn_Empleado.Enabled = false;
                Btn_Proveedor.Enabled = false;
                Btn_Producto.Enabled = false;

                //Btn_ModVenta.Enabled = false;
                //Btn_EliminarVenta.Enabled = false;
            }
        }

        //Variable bool.
        private bool soundPlayed = false;
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv.Columns[e.ColumnIndex].Name == "estado")  //Si es la columna a evaluar
            {
                if (e.Value.ToString().Contains("Caducado"))   //Si el valor de la celda contiene la palabra hora
                {
                    //Utilizamos una variable booleana para verificar si el sonido ya se reprodujo o No,
                    //Esto para que solo se reproduzca una vez y no cada que se pulza el boton.

                    if (!soundPlayed)
                    {
                        SoundPlayer player = new SoundPlayer();
                        player.SoundLocation = "./Noti.wav";
                        player.Play();
                        soundPlayed = true;
                    }

                    e.CellStyle.ForeColor = Color.Red;
                    //Notificacion Productos Caducados
                    NotifyIcon.Icon = new System.Drawing.Icon(Path.GetFullPath("./Logo.ico"));
                    NotifyIcon.BalloonTipTitle = "Productos Caducados";
                    NotifyIcon.BalloonTipText = "Ver Detalles";
                    NotifyIcon.ShowBalloonTip(3000);

                    //Mostrar solo 1na vez
                    NotifyIcon.Visible = false;
                }

                if (e.Value.ToString().Contains("Vigente"))   //Si el valor de la celda contiene la palabra hora
                {
                    e.CellStyle.ForeColor = Color.Green;
                }

                if (e.Value.ToString().Contains("Proximo"))   //Si el valor de la celda contiene la palabra hora
                {
                    e.CellStyle.ForeColor = Color.Indigo;
                }

            }
        }

        // Venta - Proceso llenado del Precio y Stock, dependiendo del producto seleccionado
        private void cmb_Nombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConectarBD();
            ConectarTienda.Open();

            // Obtener el nombre del producto seleccionado en el ComboBox
            string nombreProducto = cmb_Nombre.Text;

            // Realizar la consulta a la base de datos para obtener el precio y la cantidad disponible del producto seleccionado
            MySqlCommand consultaProducto = new MySqlCommand("SELECT precio, cantidad FROM tienda.productos WHERE nombre = @NombreProducto AND cantidad > 0", ConectarTienda);
            consultaProducto.Parameters.AddWithValue("@NombreProducto", nombreProducto);
            MySqlDataReader reader = consultaProducto.ExecuteReader();

            // Verificar si se encontró el producto en la base de datos
            if (reader.Read())
            {
                // Obtener el precio y la cantidad disponible desde los resultados de la consulta
                decimal precio = reader.GetDecimal("precio");
                int cantidadDisponible = reader.GetInt32("cantidad");

                // Mostrar el precio y la cantidad disponible en los TextBox correspondientes
                txt_Precio.Text = precio.ToString();
                txt_Stock.Text = cantidadDisponible.ToString();
            }
            else
            {
                txt_Precio.Text = string.Empty;
                txt_Stock.Text = string.Empty;
            }

            reader.Close();
            ConectarTienda.Close();
        }

        private void TipoUsuario(String nombre)
        {

            ConectarBD();
            if (ConectarTienda.State == ConnectionState.Closed)
            {
                ConectarTienda.Open();
            }

            //Sentencia para modificar
            MySqlCommand actualizaDatos = new MySqlCommand("select nombre=@name from usuarios", ConectarTienda);
            actualizaDatos.Parameters.AddWithValue("@name", nombre);

            ConectarTienda.Close();
        }

        private void SelectFondo_Click(object sender, EventArgs e)
        {
            // Conectarse a la base de datos
            ConectarBD();
            ConectarTienda.Open();

            // Abre un OpenFileDialog para que el usuario seleccione una imagen
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.png, *.bmp) | *.jpg; *.jpeg; *.png; *.bmp";
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    // Carga la imagen seleccionada en un objeto Bitmap
                    Bitmap imagenSeleccionada = new Bitmap(openFileDialog.FileName);

                    // Muestra la imagen en el PictureBox
                    pictureBox1.Image = imagenSeleccionada;

                    // Convierte la imagen a un arreglo de bytes para guardarla en la base de datos
                    byte[] imagenBytes;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        imagenSeleccionada.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        imagenBytes = stream.ToArray();
                    }

                    // Actualiza la imagen en la base de datos
                    using (MySqlConnection connection = new MySqlConnection(CadenaConexion))
                    {
                        connection.Open();

                        string sql = "UPDATE imagenes SET nombre = @nombre, imagen = @imagen WHERE id = 1";

                        MySqlCommand command = new MySqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@nombre", openFileDialog.SafeFileName);
                        command.Parameters.AddWithValue("@imagen", imagenBytes);

                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void RecuperaImagen()
        {
            // Conectarse a la base de datos
            ConectarBD();
            ConectarTienda.Open();

            MySqlConnection connection = new MySqlConnection(CadenaConexion);
            connection.Open();

            // Preparar una consulta SQL para recuperar la imagen de la base de datos
            string query = "SELECT imagen FROM imagenes WHERE id = @id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", 1); // Reemplazar con el ID de la imagen que deseas mostrar

            // Ejecutar la consulta SQL y obtener el resultado como un objeto MemoryStream
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                byte[] imageBytes = (byte[])reader["imagen"];
                MemoryStream ms = new MemoryStream(imageBytes);

                // Convertir el MemoryStream en un objeto Bitmap y mostrarlo en el PictureBox
                Bitmap image = new Bitmap(ms);
                pictureBox1.Image = image;
            }
        }

        // Método para obtener los productos desde la base de datos
        private DataTable ObtenerProductos()
        {
            ConectarBD();
            ConectarTienda.Open();

            DataTable productos = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(CadenaConexion))
            {
                MySqlCommand command = new MySqlCommand("SELECT idproducto, nombre, precio FROM productos", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(productos);
            }
            return productos;
        }


        private void SelectFondo_MouseMove(object sender, MouseEventArgs e)
        {
            SelectFondo.ForeColor = Color.Red;
        }

        private void SelectFondo_MouseLeave(object sender, EventArgs e)
        {
            SelectFondo.ForeColor = Color.AliceBlue;
        }

        private void Btn_Carrito_Click(object sender, EventArgs e)
        {
            txt_Total.Enabled = true;
            txt_Cambio.Enabled = true;
            txt_Efectivo.Enabled = true;

            dataGridView1.DataSource = null;
            if (Btn_EliminarUltimo.Visible == false)
            {
                Btn_EliminarUltimo.Visible = true;
                panel34.Visible = true;
            }

            // Verificar si las columnas ya existen antes de agregarlas
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Columna1", "Producto");
                dataGridView1.Columns.Add("Columna2", "Cantidad");
                dataGridView1.Columns.Add("Columna3", "Precio por Unidad");
            }

            // Obtener el nombre del producto seleccionado en el ComboBox
            string nombreProducto = cmb_Nombre.Text;

            // Obtener la cantidad del producto ingresada en el TextBox
            int cantidadProducto;
            if (!int.TryParse(DownCantidad.Text, out cantidadProducto))
            {
                MessageBox.Show("La cantidad ingresada no es válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtener el precio del producto ingresado en el TextBox
            decimal precioProducto;
            if (!decimal.TryParse(txt_Precio.Text, out precioProducto))
            {
                MessageBox.Show("El precio ingresado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool productoEncontrado = false;

            // Consultar la base de datos para obtener los productos vigentes
            List<string> productosVigentes = ObtenerProductosVigentes();

            // Verificar si el producto seleccionado está en la lista de productos vigentes
            if (productosVigentes.Contains(nombreProducto))
            {
                // Obtener la cantidad disponible del producto en la base de datos
                int cantidadDisponible = ObtenerCantidadDisponible(nombreProducto);

                // Calcular la cantidad total del producto después de agregarlo al carrito
                int cantidadTotal = cantidadProducto;

                // Buscar el producto en la tabla
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value != null && dataGridView1.Rows[i].Cells[0].Value.ToString() == nombreProducto)
                    {
                        // El producto ya existe en la tabla, actualizar la cantidad
                        int cantidadActual;
                        if (!int.TryParse(dataGridView1.Rows[i].Cells[1].Value.ToString(), out cantidadActual))
                        {
                            MessageBox.Show("La cantidad del producto en la tabla no es válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        cantidadTotal += cantidadActual;
                        productoEncontrado = true;
                        break;
                    }
                }

                // Verificar si la cantidad total excede la cantidad disponible en la base de datos
                if (cantidadTotal > cantidadDisponible)
                {
                    MessageBox.Show("La cantidad ingresada excede la cantidad disponible en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (productoEncontrado)
                {
                    // Buscar el producto en la tabla y actualizar la cantidad
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value != null && dataGridView1.Rows[i].Cells[0].Value.ToString() == nombreProducto)
                        {
                            int cantidadActual;
                            if (!int.TryParse(dataGridView1.Rows[i].Cells[1].Value.ToString(), out cantidadActual))
                            {
                                MessageBox.Show("La cantidad del producto en la tabla no es válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            cantidadActual += cantidadProducto;
                            dataGridView1.Rows[i].Cells[1].Value = cantidadActual.ToString();
                            break;
                        }
                    }
                }
                else
                {
                    // Agregar una nueva fila al DataGridView con el nombre, cantidad y precio del producto
                    dataGridView1.Rows.Add(nombreProducto, cantidadProducto, precioProducto);
                }
            }
            else
            {
                MessageBox.Show("El Producto Seleccionado No Está Vigente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Configurar la columna de producto para mostrar el valor correcto
            dataGridView1.Columns["Columna1"].DataPropertyName = "Producto";

            // Calcular la suma de los precios en la columna "Precio"
            decimal totalPrecio = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int cantidad;
                if (!int.TryParse(dataGridView1.Rows[i].Cells[1].Value.ToString(), out cantidad))
                {
                    MessageBox.Show("La cantidad del producto en la tabla no es válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                decimal precio;
                if (!decimal.TryParse(dataGridView1.Rows[i].Cells[2].Value.ToString(), out precio))
                {
                    MessageBox.Show("El precio del producto en la tabla no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                totalPrecio += cantidad * precio;
            }

            // Mostrar el resultado de la suma en el TextBox
            txt_Total.Text = totalPrecio.ToString();

            // Verificar si se ingresó un monto de pago válido antes de calcular el cambio
            if (!string.IsNullOrEmpty(txt_Efectivo.Text))
            {
                // Obtener el monto con el que paga el cliente
                decimal montoPago;
                if (!decimal.TryParse(txt_Efectivo.Text, out montoPago))
                {
                    MessageBox.Show("El monto de pago ingresado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Calcular el cambio
                decimal cambio = montoPago - totalPrecio;

                if (cambio <= 0)
                {
                    txt_Cambio.Text = "00.00";
                }
                else
                {
                    // Mostrar el cambio en el TextBox correspondiente
                    txt_Cambio.Text = cambio.ToString();
                }
            }

            DownCantidad.Value = 1;
        }


        private List<string> ObtenerProductosVigentes()
        {
            List<string> productosVigentes = new List<string>();

            ConectarBD();
            ConectarTienda.Open();

            // Realizar la consulta a la base de datos para obtener los productos vigentes
            MySqlCommand consultaVigentes = new MySqlCommand("SELECT nombre FROM tienda.productos WHERE caducidad >= CURDATE()", ConectarTienda);
            MySqlDataReader reader = consultaVigentes.ExecuteReader();

            // Leer los resultados de la consulta y agregar los productos vigentes a la lista
            while (reader.Read())
            {
                string nombreProducto = reader.GetString("nombre");
                productosVigentes.Add(nombreProducto);
            }

            reader.Close();
            ConectarTienda.Close();

            return productosVigentes;
        }

        private int ObtenerCantidadDisponible(string nombreProducto)
        {
            int cantidadDisponible = 0;

            ConectarBD();
            ConectarTienda.Open();

            // Realizar la consulta a la base de datos para obtener la cantidad disponible del producto
            MySqlCommand consultaCantidad = new MySqlCommand("SELECT cantidad FROM tienda.productos WHERE nombre = @NombreProducto", ConectarTienda);
            consultaCantidad.Parameters.AddWithValue("@NombreProducto", nombreProducto);
            object resultado = consultaCantidad.ExecuteScalar();
            if (resultado != null && resultado != DBNull.Value)
            {
                cantidadDisponible = Convert.ToInt32(resultado);
            }

            ConectarTienda.Close();

            return cantidadDisponible;
        }

        private void txt_Efectivo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Verificar si se ha ingresado un monto en el campo de efectivo
                if (!string.IsNullOrEmpty(txt_Efectivo.Text))
                {
                    // Obtener el total a pagar
                    decimal total = decimal.Parse(txt_Total.Text);

                    // Obtener el monto con el que paga el cliente
                    decimal montoPago = decimal.Parse(txt_Efectivo.Text);

                    // Calcular el cambio
                    decimal cambio = montoPago - total;

                    // Si el cambio es menor que cero, establecer el valor del TextBox a cero
                    if (cambio <= 0)
                    {
                        txt_Cambio.Text = "00.00";
                    }
                    else
                    {
                        // Mostrar el cambio en el TextBox correspondiente
                        txt_Cambio.Text = cambio.ToString();
                    }

                }
            }
            catch {
                MessageBox.Show("Ingrese una Cantidad Válida en el Campo de Efectivo", "Error de Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Efectivo.Text = "";
            }
        }

        private void Btn_EliminarUltimo_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Limite-Revertir Cambios", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Btn_EliminarUltimo.Visible = false;
                panel34.Visible = false;
            }
            else
            {
                // Obtener el precio del último producto agregado
                decimal precioUltimoProducto = Convert.ToDecimal(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Columna3"].Value);

                // Eliminar la última fila del DataGridView
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);

                // Calcular la nueva suma de los precios en la columna "Precio"
                decimal totalPrecio = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    totalPrecio += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Columna3"].Value);
                }

                // Actualizar el valor del total a pagar
                txt_Total.Text = totalPrecio.ToString();

                // Actualizar el valor del cambio, si se ingresó un monto de pago válido
                if (!string.IsNullOrEmpty(txt_Efectivo.Text))
                {
                    decimal montoPago = decimal.Parse(txt_Efectivo.Text);
                    decimal cambio = montoPago - totalPrecio;
                    txt_Cambio.Text = cambio.ToString();
                }
            }
        }

        private void Btn_Compra_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de diálogo de confirmación al usuario
            DialogResult resultado = MessageBox.Show("¿Está seguro de realizar la venta?", "Confirmación de Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verificar si el usuario hizo clic en el botón "Sí"
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    ConectarBD();
                    ConectarTienda.Open();

                    // Obtener la fecha de venta del control lbl_fecha
                    DateTime FechaVenta = DateTime.Parse(lbl_fecha.Text);

                    // Obtener el total de venta del control txt_Total
                    decimal totalVenta = decimal.Parse(txt_Total.Text);

                    // Obtener el efectivo ingresado en el control txt_Efectivo
                    string efectivoTexto = txt_Efectivo.Text;
                    decimal efectivo = 0;

                    // Verificar si el campo de efectivo está vacío o no es un número válido
                    if (string.IsNullOrWhiteSpace(efectivoTexto) || !decimal.TryParse(efectivoTexto, out efectivo))
                    {
                        MessageBox.Show("Ingrese una Cantidad Válida en el Campo de Efectivo.", "Error de Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Verificar si el efectivo es suficiente para realizar la venta
                    if (efectivo >= totalVenta)
                    {
                        // Obtener el vendedor del control label1
                        string vendedor = label1.Text;

                        // Insertar los datos de venta en la tabla "Venta"
                        string queryVenta = "INSERT INTO Venta (FechaVenta, Total, Vendedor) VALUES (@FechaVenta, @Total, @Vendedor)";

                        using (MySqlConnection connection = new MySqlConnection(CadenaConexion))
                        {
                            connection.Open();

                            using (MySqlCommand commandVenta = new MySqlCommand(queryVenta, connection))
                            {
                                commandVenta.Parameters.AddWithValue("@FechaVenta", FechaVenta);
                                commandVenta.Parameters.AddWithValue("@Total", totalVenta);
                                commandVenta.Parameters.AddWithValue("@Vendedor", vendedor);
                                commandVenta.ExecuteNonQuery();

                                // Obtener el ID de la venta generada
                                long ventaId = commandVenta.LastInsertedId;

                                // Insertar los datos de detalle de venta en la tabla "DetalleVenta"
                                string queryDetalleVenta = "INSERT INTO DetalleVenta (VentaId, Producto, Cantidad, Precio) VALUES (@VentaId, @Producto, @Cantidad, @Precio)";

                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                                    {
                                        string producto = row.Cells[0].Value.ToString();
                                        int cantidad = Convert.ToInt32(row.Cells[1].Value);
                                        decimal precio = Convert.ToDecimal(row.Cells[2].Value);

                                        using (MySqlCommand commandDetalleVenta = new MySqlCommand(queryDetalleVenta, connection))
                                        {
                                            commandDetalleVenta.Parameters.AddWithValue("@VentaId", ventaId);
                                            commandDetalleVenta.Parameters.AddWithValue("@Producto", producto);
                                            commandDetalleVenta.Parameters.AddWithValue("@Cantidad", cantidad);
                                            commandDetalleVenta.Parameters.AddWithValue("@Precio", precio);
                                            commandDetalleVenta.ExecuteNonQuery();

                                            // Restar la cantidad vendida de la cantidad actual del producto
                                            string queryActualizarCantidad = "UPDATE productos SET cantidad = cantidad - @Cantidad WHERE nombre = @Producto";
                                            using (MySqlCommand commandActualizarCantidad = new MySqlCommand(queryActualizarCantidad, connection))
                                            {
                                                commandActualizarCantidad.Parameters.AddWithValue("@Cantidad", cantidad);
                                                commandActualizarCantidad.Parameters.AddWithValue("@Producto", producto);
                                                commandActualizarCantidad.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //En casos de registro , manda el msj almacenado
                        MessageBox.Show("¡Venta Realizada!", "Se Realizo una Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_Precio.Clear();
                        txt_Stock.Clear();
                        cmb_Nombre.DataSource = null; // Elimina la fuente de datos actual
                        cmb_Nombre.Items.Clear(); // Elimina los elementos restantes en el ComboBox

                        Operaciones op = new Operaciones();
                        op.CargarDatosProducto2(cmb_Nombre);
                        op.CargarDatosCaducados(dataGridView2);
                        dataGridView1.Columns.Clear();
                        op.MostrarRegistrosVenta(dataGridView1);
                        Limpeza();

                        ActualizarNumeroVentasHoy();

                        txt_Total.Enabled = false;
                        txt_Cambio.Enabled = false;
                        txt_Efectivo.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("El Efectivo Ingresado NO es Suficiente para Realizar la Venta.", "Error de Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                catch
                {
                    MessageBox.Show("Primero Agrega al Carrito", "Error de Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        // Variable booleana para controlar si la notificación ya se ha mostrado
        private bool notificacionMostrada = false;

        // Método para verificar la cantidad de productos y mostrar notificación si es necesario
        private void VerificarCantidadProductos()
        {
            if (!notificacionMostrada)
            {
                List<string> productosBajosStock = ObtenerProductosBajosStock(10); // Obtener productos con cantidad <= 5

                if (productosBajosStock.Count > 0)
                {
                    // Construir el mensaje con los productos bajos en stock
                    string mensaje = "Los Siguientes Productos Tienen una Cantidad Baja de Stock:\n";
                    foreach (string producto in productosBajosStock)
                    {
                        mensaje += "- " + producto + "\n";
                    }

                    // Mostrar MessageBox con los productos bajos en stock
                    MessageBox.Show(mensaje, "Productos Bajos en Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Actualizar el estado de la variable notificacionMostrada
                    notificacionMostrada = true;
                }
            }
        }

        // Método para obtener productos con cantidad baja de stock
        private List<string> ObtenerProductosBajosStock(int cantidadMaxima)
        {
            List<string> productosBajosStock = new List<string>();

            ConectarBD();
            ConectarTienda.Open();

            // Realizar la consulta a la base de datos para obtener los productos con cantidad <= cantidadMaxima
            MySqlCommand consultaProductosBajosStock = new MySqlCommand("SELECT nombre FROM tienda.productos WHERE cantidad <= @CantidadMaxima", ConectarTienda);
            consultaProductosBajosStock.Parameters.AddWithValue("@CantidadMaxima", cantidadMaxima);
            MySqlDataReader reader = consultaProductosBajosStock.ExecuteReader();

            // Leer los resultados de la consulta y agregar los productos a la lista
            while (reader.Read())
            {
                string nombreProducto = reader.GetString("nombre");
                productosBajosStock.Add(nombreProducto);
            }

            reader.Close();
            ConectarTienda.Close();

            return productosBajosStock;
        }

        //BUSQUEDA FILTRO FECHAS ----------------------------------------------------------


        private void Btn_LimpiarVenta_Click(object sender, EventArgs e)
        {
            Limpeza();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            // Llamar al método para actualizar el número de ventas realizadas ese día
            ActualizarNumeroVentasHoy();
        }

        private void ActualizarNumeroVentasHoy()
        {
            // Obtener la fecha actual
            DateTime fechaActual = DateTime.Today;

            // Crear la conexión a la base de datos
            using (MySqlConnection conexion = new MySqlConnection(CadenaConexion))
            {
                // Abrir la conexión
                conexion.Open();

                // Crear la consulta para obtener el número de ventas realizadas hoy
                string consulta = $"SELECT COUNT(*) AS TotalVentas FROM Venta WHERE FechaVenta = '{fechaActual:yyyy-MM-dd}'";

                // Crear el comando con la consulta y la conexión
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    // Ejecutar el comando y obtener los datos en un DataTable
                    using (MySqlDataAdapter adaptador = new MySqlDataAdapter(comando))
                    {
                        DataTable tablaVentas = new DataTable();
                        adaptador.Fill(tablaVentas);

                        // Verificar si hay ventas hoy
                        if (tablaVentas.Rows.Count > 0)
                        {
                            // Obtener el número de ventas hoy
                            int numeroVentasHoy = Convert.ToInt32(tablaVentas.Rows[0]["TotalVentas"]);

                            // Agregar un "0" antes del número si es menor que 10
                            string numeroVentasHoyFormateado = numeroVentasHoy < 10 ? numeroVentasHoy.ToString("D2") : numeroVentasHoy.ToString();

                            // Mostrar el número de ventas en el TextBox o en el control que desees
                            lbl_Ventas.Text = numeroVentasHoyFormateado;
                        }
                        else
                        {
                            // No hay ventas hoy, mostrar cero
                            lbl_Ventas.Text = "0";
                        }
                    }
                }
            }
        }

        //Venta - Proceso llenado del Precio y Stock, dependiendo del producto seleccionado
        private void cmb_Nombre_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ConectarBD();
            ConectarTienda.Open();

            // Obtener el nombre del producto seleccionado en el ComboBox
            string nombreProducto = cmb_Nombre.Text;

            // Realizar la consulta a la base de datos para obtener el precio y la cantidad disponible del producto seleccionado
            MySqlCommand consultaProducto = new MySqlCommand("SELECT precio, cantidad FROM tienda.productos WHERE nombre = @NombreProducto", ConectarTienda);
            consultaProducto.Parameters.AddWithValue("@NombreProducto", nombreProducto);
            MySqlDataReader reader = consultaProducto.ExecuteReader();

            // Verificar si se encontró el producto en la base de datos
            if (reader.Read())
            {
                // Obtener el precio y la cantidad disponible desde los resultados de la consulta
                decimal precio = reader.GetDecimal("precio");
                int cantidadDisponible = reader.GetInt32("cantidad");

                // Mostrar el precio y la cantidad disponible en los TextBox correspondientes
                txt_Precio.Text = precio.ToString();
                txt_Stock.Text = cantidadDisponible.ToString();
            }

            reader.Close();
            ConectarTienda.Close();
        }


        private void Btn_BusquedaVen_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            Btn_EliminarUltimo.Visible = false;
            panel34.Visible = false;

            // Obtener la fecha ingresada por el usuario desde el DateTimePicker
            DateTime fecha = Date.Value.Date;

            // Calcular el total de ventas para la fecha ingresada
            decimal totalVentas = CalcularTotalVentasPorFecha(fecha);

            if (totalVentas > 0)
            {
                MessageBox.Show("El total de ventas para la fecha " + fecha.ToString("yyyy-MM-dd") + " es: " + totalVentas.ToString("N2"), "Total de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mostrar las ventas en el DataGridView
                MostrarVentasEnDataGridView(fecha);
            }
            else
            {
                MessageBox.Show("No se encontraron ventas para la fecha " + fecha.ToString("yyyy-MM-dd"), "Sin Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //-
        private decimal CalcularTotalVentasPorFecha(DateTime fecha)
        {
            decimal totalVentas = 0;

            try
            {
                ConectarBD();

                // Consulta SQL para calcular el total de ventas filtrando por fecha
                string query = "SELECT SUM(Total) FROM Venta WHERE FechaVenta = @FechaVenta";

                using (MySqlConnection connection = new MySqlConnection(CadenaConexion))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FechaVenta", fecha);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            totalVentas = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular el total de ventas: " + ex.Message, "Error de Cálculo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return totalVentas;
        }

        private void MostrarVentasEnDataGridView(DateTime fecha)
        {
            try
            {
                ConectarBD();

                string consulta = "SELECT V.FechaVenta, V.Total, V.Vendedor, " +
                                  "GROUP_CONCAT(CONCAT(D.Producto, ' (', D.Cantidad, ')') SEPARATOR ', ') AS ProductosVendidos " +
                                  "FROM Venta AS V " +
                                  "INNER JOIN " +
                                  "(SELECT VentaId, Producto, SUM(Cantidad) AS Cantidad " +
                                  "FROM DetalleVenta " +
                                  "GROUP BY VentaId, Producto) AS D " +
                                  "ON V.VentaId = D.VentaId " +
                                  "WHERE V.FechaVenta = @FechaVenta " +
                                  "GROUP BY V.FechaVenta, V.Total, V.Vendedor";

                using (MySqlConnection connection = new MySqlConnection(CadenaConexion))
                {
                    connection.Open();

                    // Crear adaptador y DataTable para almacenar los resultados de la consulta
                    MySqlDataAdapter adaptador = new MySqlDataAdapter(consulta, connection);
                    DataTable dtVentas = new DataTable();

                    // Pasar la fecha seleccionada como parámetro a la consulta
                    adaptador.SelectCommand.Parameters.AddWithValue("@FechaVenta", fecha);

                    // Limpiar el DataTable antes de llenarlo con los nuevos datos
                    dtVentas.Clear();

                    // Llenar el DataTable con los datos de la consulta
                    adaptador.Fill(dtVentas);

                    // Verificar si se encontraron registros
                    if (dtVentas.Rows.Count > 0)
                    {
                        // Asignar el DataTable como origen de datos del DataGridView
                        dataGridView1.DataSource = dtVentas;

                        // Ajustar el ancho de las columnas del DataGridView para que se ajusten automáticamente al contenido
                        dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron ventas para la fecha seleccionada.", "Sin Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar las ventas: " + ex.Message, "Error de Mostrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Paint(object sender, PaintEventArgs e)
        {
            // Obtén el botón y la forma del botón
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            Rectangle bounds = new Rectangle(0, 0, button.Width, button.Height);

            // Configura el estilo de dibujo para bordes redondeados
            int cornerRadius = 20; // Define el radio de los bordes redondeados (puedes ajustarlo según tus preferencias)
            using (GraphicsPath path = new GraphicsPath())
            using (Pen pen = new Pen(Color.Black, 2)) // Define el color y el grosor del borde
            {
                // Agrega una forma con bordes redondeados al path
                path.AddArc(bounds.X, bounds.Y, cornerRadius, cornerRadius, 180, 90); // Esquina superior izquierda
                path.AddArc(bounds.Right - cornerRadius, bounds.Y, cornerRadius, cornerRadius, 270, 90); // Esquina superior derecha
                path.AddArc(bounds.Right - cornerRadius, bounds.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 90); // Esquina inferior derecha
                path.AddArc(bounds.X, bounds.Bottom - cornerRadius, cornerRadius, cornerRadius, 90, 90); // Esquina inferior izquierda

                // Dibuja el botón con los bordes redondeados
                button.Region = new Region(path);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void pnl_4_Paint(object sender, PaintEventArgs e)
        {

        }





        //Fin
    }
}