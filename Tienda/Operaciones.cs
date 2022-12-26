using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms; //Da Acceso al DataGridView


namespace Tienda
{
    internal class Operaciones
    {
        //CONECTAR BASE DE DATOS
        public String CadenaConexion = "Database=Tienda;Server=localhost;Uid=root;Pwd=root";

        private MySqlConnection ConectarTienda;

        public void ConectarBD()
        {
            ConectarTienda = new MySqlConnection(CadenaConexion);
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //METODO INICIA SECION

        public String IniciarSecion(String nick, String pass)
        {

            String nombre = null;

            ConectarBD();
            ConectarTienda.Open();

            MySqlCommand datosUsuario = new MySqlCommand("select nombre from usuarios where nick=@nk and pass=@ps", ConectarTienda);


            datosUsuario.Parameters.AddWithValue("@nk", nick);
            datosUsuario.Parameters.AddWithValue("@ps", pass);
            MySqlDataReader datosLeidos = datosUsuario.ExecuteReader();

            if (datosLeidos.Read())
            {
                nombre = datosLeidos["nombre"].ToString();
            }
            else
            {

                nombre = "error";
            }

            ConectarTienda.Close();
            return nombre;

        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        // ALMACENAR USUARIO EN LA BASE DE DATOS

        public int RegistraUsuario(String nick, String nombre, String pass)
        {
            //Variable para recibir los registros almacenados
            int UsuariosRegistrados = 0;

            //Solicita la conexio a la base de datos
            ConectarBD();

            //Evalua si la conexion a la base de datos esta abierta 
            if (ConectarTienda.State != ConnectionState.Open)
            {
                //Abre la conexion a la Base de Datos 
                ConectarTienda.Open();
            }

            // Se genera la consulta en Mysql para almacenar los datos
            MySqlCommand ingresaDatosUsuarios = new MySqlCommand("insert into usuarios values(@usuario,@name,@contra)", ConectarTienda);

            //Se asociona los parametros en el orden de almacenamiento 
            ingresaDatosUsuarios.Parameters.AddWithValue("@usuario", nick);
            ingresaDatosUsuarios.Parameters.AddWithValue("@name", nombre);
            ingresaDatosUsuarios.Parameters.AddWithValue("@contra", pass);

            //Ejecutar la consulta
            try
            {
                UsuariosRegistrados = ingresaDatosUsuarios.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }

            //Cierra la conexion de la base de Datos 
            ConectarTienda.Close();

            // Envia la Cantidad de registros almacenados
            return UsuariosRegistrados;
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        // ALMACENAR PRODUCTO EN LA BASE DE DATOS

        public int AlmacenaProductos(float codigo, String nombre, int cantidad, DateTime caducidad, String proveedor, String precio)
        {
            //Variable para recibir los registros almacenados
            int ProductoAlmacenado = 0;

            //Solicita la conexio a la base de datos
            ConectarBD();

            //Evalua si la conexion a la base de datos esta abierta 
            if (ConectarTienda.State != ConnectionState.Open)
            {
                //Abre la conexion a la Base de Datos 
                ConectarTienda.Open();
            }

            // Se genera la consulta en Mysql para almacenar los datos
            MySqlCommand ingresaDatosProducto = new MySqlCommand("insert into productos values(0,@cod, @name, @cant, @cad, @prov, @prec)", ConectarTienda);

            //Se asociona los parametros en el orden de almacenamiento 
            ingresaDatosProducto.Parameters.AddWithValue("@cod", codigo);
            ingresaDatosProducto.Parameters.AddWithValue("@name", nombre);
            ingresaDatosProducto.Parameters.AddWithValue("@cant", cantidad);
            ingresaDatosProducto.Parameters.AddWithValue("@cad", caducidad);
            ingresaDatosProducto.Parameters.AddWithValue("@prov", proveedor);
            ingresaDatosProducto.Parameters.AddWithValue("@prec", precio);

            //Ejecutar la consulta
            try
            {
                ProductoAlmacenado = ingresaDatosProducto.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }

            //Cierra la conexion de la base de Datos 
            ConectarTienda.Close();

            // Envia la Cantidad de registros almacenados
            return ProductoAlmacenado;
        }

        //Metodo para Cargar los dastos registrados en el dataGridView
        public void CargarDatosProducto(DataGridView consultaGeneral)
        {

            ConectarBD();
            ConectarTienda.Open();
            //Realiza la consulta de Base de datos con la conexion
            MySqlCommand ListaProductos = new MySqlCommand("select idproducto as Numero ,codigo as Codigo, Nombre as Nombre, caducidad as Caducidad, cantidad as Cantidad, proveedor as Proveedor, precio as Precio from productos", ConectarTienda);

            //Tomar los registros de la consulta
            MySqlDataAdapter listado = new MySqlDataAdapter(ListaProductos);

            //Carga en memoria los datos de la consulta
            DataSet datosProducto = new DataSet();

            //Genera el listado de los registros de la consulta
            listado.Fill(datosProducto);

            //Se entrega los datos al contenedor del DataGridView
            consultaGeneral.DataSource = datosProducto.Tables[0];
            ConectarTienda.Close();
        }

        //"Cargardatos2producto2"

        public void CargarDatosProducto2(ComboBox consultaGeneral)
        {
            ConectarBD();
            ConectarTienda.Open();
            //Realiza la consulta de Base de datos con la conexion
            MySqlCommand ListaProductos = new MySqlCommand("select idproducto as Numero,codigo as Codigo, Nombre as Nombre, codigo as Codigo from productos", ConectarTienda);

            //Tomar los registros de la consulta
            MySqlDataAdapter listado = new MySqlDataAdapter(ListaProductos);

            //Carga en memoria los datos de la consulta
            DataSet datosProducto = new DataSet();

            //Genera el listado de los registros de la consulta
            listado.Fill(datosProducto);
            consultaGeneral.ValueMember = "Codigo";
            consultaGeneral.DisplayMember = "Codigo";

            //Se entrega los datos al contenedor del DataGridView
            consultaGeneral.DataSource = datosProducto.Tables[0];
            ConectarTienda.Close();
        }
        //-------------------------------------------------------------------------------------------------------------------------------//

        //ALMACENA PROVEEDOR EN LA BASE DE DATOS

        public int AlmacenaProveedor(String nombre, String direccion, String contacto, String siglas)
        {
            //Variable para recibir los registros almacenados
            int ProveedorAlmacenado = 0;

            //Solicita la conexio a la base de datos
            ConectarBD();

            //Evalua si la conexion a la base de datos esta abierta 
            if (ConectarTienda.State != ConnectionState.Open)
            {
                //Abre la conexion a la Base de Datos 
                ConectarTienda.Open();
            }

            // Se genera la consulta en Mysql para almacenar los datos
            MySqlCommand ingresaDatosProducto = new MySqlCommand("insert into proveedor values(0, @name, @dire, @contact ,@sigl)", ConectarTienda);

            //Se asociona los parametros en el orden de almacenamiento 
            ingresaDatosProducto.Parameters.AddWithValue("@name", nombre);
            ingresaDatosProducto.Parameters.AddWithValue("@dire", direccion);
            ingresaDatosProducto.Parameters.AddWithValue("@contact", contacto);
            ingresaDatosProducto.Parameters.AddWithValue("@sigl", siglas);
            //Ejecutar la consulta
            try
            {
                ProveedorAlmacenado = ingresaDatosProducto.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }

            //Cierra la conexion de la base de Datos 
            ConectarTienda.Close();

            // Envia la Cantidad de registros almacenados
            return ProveedorAlmacenado;
        }

        public void CargarDatosProveedor(DataGridView consultaGeneral)
        {
            ConectarBD();
            ConectarTienda.Open();

            //Realiza la consulta de Base de datos con la conexion
            MySqlCommand ListaProductos = new MySqlCommand("select idproveedor as Numero ,nombre as Nombre ,direccion as Direccion ,contacto as Contacto, siglas as Siglas from proveedor", ConectarTienda);

            //Tomar los registros de la consulta
            MySqlDataAdapter listado = new MySqlDataAdapter(ListaProductos);

            //Carga en memoria los datos de la consulta
            DataSet datosProveedor = new DataSet();

            //Genera el listado de los registros de la consulta
            listado.Fill(datosProveedor);

            //Se entrega los datos al contenedor del DataGridView
            consultaGeneral.DataSource = datosProveedor.Tables[0];
            ConectarTienda.Close();
        }

        public void CargarDatosProveedor2(ComboBox consultaGeneral)
        {
            ConectarBD();
            ConectarTienda.Open();
            //Realiza la consulta de Base de datos con la conexion
            MySqlCommand ListaProductos = new MySqlCommand("select Siglas from proveedor", ConectarTienda);

            //Tomar los registros de la consulta
            MySqlDataAdapter listado = new MySqlDataAdapter(ListaProductos);

            //Carga en memoria los datos de la consulta
            DataSet datosProveedor = new DataSet();

            //Genera el listado de los registros de la consulta
            listado.Fill(datosProveedor);
            consultaGeneral.ValueMember = "Siglas";
            consultaGeneral.DisplayMember = "Siglas";
            //Se entrega los datos al contenedor del DataGridView
            consultaGeneral.DataSource = datosProveedor.Tables[0];
            ConectarTienda.Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        public void CargarDatosProveedor3(ComboBox consultaGeneral)
        {
            ConectarBD();
            ConectarTienda.Open();
            //Realiza la consulta de Base de datos con la conexion
            MySqlCommand ListaProveedor = new MySqlCommand("select idproveedor as Numero from proveedor", ConectarTienda);

            //Tomar los registros de la consulta
            MySqlDataAdapter listado = new MySqlDataAdapter(ListaProveedor);

            //Carga en memoria los datos de la consulta
            DataSet datosProveedor = new DataSet();

            //Genera el listado de los registros de la consulta
            listado.Fill(datosProveedor);
            consultaGeneral.ValueMember = "Numero";
            consultaGeneral.DisplayMember = "Numero";

            //Se entrega los datos al contenedor del DataGridView
            consultaGeneral.DataSource = datosProveedor.Tables[0];
            ConectarTienda.Close();
        }



    }
}

