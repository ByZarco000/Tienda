using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms; //Da Acceso al DataGridView
using System.Windows.Media;
using Color = System.Drawing.Color;

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

        // ALMACENAR USUARIO

        public int RegistrarUsuario(String nombre, String apellidop, String apellidom, int salario, String nick, String pass, String sexo, int codigo, String contacto)
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
            MySqlCommand ingresaDatosUsuarios = new MySqlCommand("insert into usuarios values(0,@name,@app,@apm,@saldo,@usuario,@contra,@sex,@cod,@cont)", ConectarTienda);

            //Se asociona los parametros en el orden de almacenamiento
            ingresaDatosUsuarios.Parameters.AddWithValue("@name", nombre);
            ingresaDatosUsuarios.Parameters.AddWithValue("@app", apellidop);
            ingresaDatosUsuarios.Parameters.AddWithValue("@apm", apellidom);
            ingresaDatosUsuarios.Parameters.AddWithValue("@saldo", salario);
            ingresaDatosUsuarios.Parameters.AddWithValue("@usuario", nick);
            ingresaDatosUsuarios.Parameters.AddWithValue("@contra", pass);
            ingresaDatosUsuarios.Parameters.AddWithValue("@sex", sexo);
            ingresaDatosUsuarios.Parameters.AddWithValue("@cod", codigo);
            ingresaDatosUsuarios.Parameters.AddWithValue("@cont", contacto);

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

        //Metos llenar combobox de ventana usuarios
        public void CargarDatosUsuarios(ComboBox consultaGeneral)
        {
            ConectarBD();
            ConectarTienda.Open();
            //Realiza la consulta de Base de datos con la conexion
            MySqlCommand ListaUsuarios = new MySqlCommand("select idusuarios, nombre from usuarios where idusuarios !=1", ConectarTienda);

            //Tomar los registros de la consulta
            MySqlDataAdapter listado = new MySqlDataAdapter(ListaUsuarios);

            //Carga en memoria los datos de la consulta
            DataSet datosUsuarios = new DataSet();

            //Genera el listado de los registros de la consulta
            listado.Fill(datosUsuarios);

            consultaGeneral.ValueMember = "idusuarios";
            consultaGeneral.DisplayMember = "nombre";

            //Se entrega los datos al contenedor del DataGridView
            consultaGeneral.DataSource = datosUsuarios.Tables[0];
            ConectarTienda.Close();
        }

        //Metodo para Cargar los dastos registrados en el dataGridView
        public void CargarDatosUsuario2(DataGridView consultaGeneral)
        {
            ConectarBD();
            ConectarTienda.Open();
            //Realiza la consulta de Base de datos con la conexion
            MySqlCommand ListaUsuarios = new MySqlCommand("select Nombre as Nombre, apellidop as ApellidoP, apellidom as ApellidoM, nick as NickName, pass as Password, sexo as Sexo, salario as $alario, codigo as Codigo, contacto as Contacto from usuarios where idusuarios !=1", ConectarTienda);

            //Tomar los registros de la consulta
            MySqlDataAdapter listado = new MySqlDataAdapter(ListaUsuarios);

            //Carga en memoria los datos de la consulta
            DataSet datosUsuarios = new DataSet();

            //Genera el listado de los registros de la consulta
            listado.Fill(datosUsuarios);

            //Se entrega los datos al contenedor del DataGridView
            consultaGeneral.DataSource = datosUsuarios.Tables[0];
            ConectarTienda.Close();
        }

        public String[] CargarDatosUsuario3(int codigo)
        {
            //Declaramos un vector de 4 celdas para los datos del platillo seleccionado
            String[] Empleado = new String[9];
            ConectarBD();
            ConectarTienda.Open();
            MySqlCommand datosP = new MySqlCommand("select nombre, apellidop, apellidom, salario, nick, pass, sexo, codigo, contacto from usuarios where idusuarios=@cod", ConectarTienda);
            datosP.Parameters.AddWithValue("@cod", codigo);
            //Realizar la consulta
            MySqlDataReader filtroP = datosP.ExecuteReader();
            //Revisar si los datos fueron leidos
            if (filtroP.Read())
            {
                Empleado[0] = filtroP["nombre"].ToString();
                Empleado[1] = filtroP["apellidop"].ToString();
                Empleado[2] = filtroP["apellidom"].ToString();
                Empleado[3] = filtroP["salario"].ToString();
                Empleado[4] = filtroP["nick"].ToString();
                Empleado[5] = filtroP["pass"].ToString();
                Empleado[6] = filtroP["sexo"].ToString();
                Empleado[7] = filtroP["codigo"].ToString();
                Empleado[8] = filtroP["contacto"].ToString();
            }
            ConectarTienda.Close();
            return Empleado;
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        // ALMACENAR PRODUCTO EN LA BASE DE DATOS
        public int AlmacenaProductos(String nombre, int idproveedor, int codigo, int cantidad, DateTime caducidad, float precio)
        {
            //Variable para recibir los registros almacenados
            int ProductoAlmacenado = 0;

            ConectarBD();

            //Evalua si la conexion a la base de datos esta abierta 
            if (ConectarTienda.State != ConnectionState.Open)
            {
                //Abre la conexion a la Base de Datos 
                ConectarTienda.Open();
            }

            // Se genera la consulta en Mysql para almacenar los datos
            MySqlCommand ingresaDatosProducto = new MySqlCommand("insert into productos (idproveedor, nombre, codigo, cantidad, caducidad, precio) values (@prov, @name, @cod, @cant, @cad, @prec)", ConectarTienda);

            //Se asociona los parametros en el orden de almacenamiento 
            ingresaDatosProducto.Parameters.AddWithValue("@name", nombre);
            ingresaDatosProducto.Parameters.AddWithValue("@prov", idproveedor);
            ingresaDatosProducto.Parameters.AddWithValue("@cod", codigo);
            ingresaDatosProducto.Parameters.AddWithValue("@cant", cantidad);
            ingresaDatosProducto.Parameters.AddWithValue("@cad", caducidad);
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

        // Método para cargar los datos registrados en el DataGridView
        public void CargarDatosProducto(DataGridView consultaGeneral)
        {
            // Establecer la conexión con la base de datos
            ConectarBD();
            ConectarTienda.Open();

            // Realizar la consulta a la base de datos para obtener los datos de los productos y el nombre de su proveedor
            MySqlCommand consultaProductos = new MySqlCommand("SELECT p.nombre AS NombreProducto, p.caducidad AS Caducidad, p.cantidad AS Cantidad, pr.nombre AS Proveedor, p.precio AS Precio, p.codigo AS Codigo FROM productos p INNER JOIN proveedor pr ON p.idproveedor = pr.idproveedor", ConectarTienda);

            // Crear un adaptador para leer los datos de la consulta
            MySqlDataAdapter adaptador = new MySqlDataAdapter(consultaProductos);

            // Crear un DataSet para almacenar los datos de la consulta
            DataSet datosProducto = new DataSet();

            // Llenar el DataSet con los datos de la consulta
            adaptador.Fill(datosProducto);

            // Asignar los datos al control DataGridView
            consultaGeneral.DataSource = datosProducto.Tables[0];

            // Cerrar la conexión con la base de datos
            ConectarTienda.Close();

            // Suscribirse al evento CellFormatting del DataGridView
            consultaGeneral.CellFormatting += DataGridView_CellFormatting;
        }

        // Evento CellFormatting del DataGridView
        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender; // Obtener una referencia al DataGridView

            // Verificar si la columna actual es la columna "Cantidad"
            if (dataGridView.Columns[e.ColumnIndex].Name == "Cantidad" && e.RowIndex >= 0)
            {
                int cantidad = Convert.ToInt32(e.Value);

                // Obtener la fila actual
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];

                // Pintar toda la fila de rojo si la cantidad es igual a 0
                if (cantidad == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.PaleVioletRed;
                }
                // Pintar toda la fila de amarillo si la cantidad es mayor a 0 pero menor o igual a 5
                else if (cantidad > 0 && cantidad <= 10)
                {
                    row.DefaultCellStyle.BackColor = Color.Khaki;
                }
                else
                {
                    // Restaurar el color de fondo predeterminado si la cantidad no cumple ninguna condición
                    row.DefaultCellStyle.BackColor = dataGridView.DefaultCellStyle.BackColor;
                }
            }
        }

        //"Cargardatos producto2"
        public void CargarDatosProducto(ComboBox consultaGeneral)
        {
            ConectarBD();
            ConectarTienda.Open();
            //Realiza la consulta de Base de datos con la conexion
            MySqlCommand ListaProductos = new MySqlCommand("select idproducto,nombre,precio from productos", ConectarTienda);

            //Tomar los registros de la consulta
            MySqlDataAdapter listado = new MySqlDataAdapter(ListaProductos);

            //Carga en memoria los datos de la consulta
            DataSet datosProducto = new DataSet();

            //Genera el listado de los registros de la consulta
            listado.Fill(datosProducto);
            consultaGeneral.ValueMember = "idproducto";
            consultaGeneral.DisplayMember = "nombre";

            //Se entrega los datos al contenedor del DataGridView
            consultaGeneral.DataSource = datosProducto.Tables[0];

            ConectarTienda.Close();
        }

        public void CargarDatosProducto2(ComboBox consultaGeneral)
        {
            ConectarBD();
            ConectarTienda.Open();

            // Realiza la consulta de Base de datos con la conexión
            MySqlCommand ListaProductos = new MySqlCommand("SELECT idproducto, nombre, precio FROM productos WHERE caducidad >= CURDATE() AND cantidad > 0", ConectarTienda);

            // Tomar los registros de la consulta
            MySqlDataAdapter listado = new MySqlDataAdapter(ListaProductos);

            // Carga en memoria los datos de la consulta
            DataSet datosProducto = new DataSet();

            // Genera el listado de los registros de la consulta
            listado.Fill(datosProducto);

            // Configura el ComboBox para mostrar el nombre del producto y utilizar el ID del producto como valor
            consultaGeneral.ValueMember = "idproducto";
            consultaGeneral.DisplayMember = "nombre";

            // Asigna los datos al ComboBox
            consultaGeneral.DataSource = datosProducto.Tables[0];

            ConectarTienda.Close();
        }

        //Método para colocar los datos filtrados
        public String[] CargarDatosProducto3(int codigo)
        {
            //Declaramos un vector de 4 celdas para los datos del platillo seleccionado
            String[] producto = new String[6];
            ConectarBD();
            ConectarTienda.Open();
            MySqlCommand datosP = new MySqlCommand("SELECT p.nombre, p.codigo, p.cantidad, p.caducidad, p.idproveedor, p.precio, pr.nombre AS NombreProveedor FROM productos p INNER JOIN proveedor pr ON p.idproveedor = pr.idproveedor WHERE p.idproducto = @cod", ConectarTienda);
            datosP.Parameters.AddWithValue("@cod", codigo);

            //Realizar la consulta
            MySqlDataReader filtroP = datosP.ExecuteReader();
            //Revisar si los datos fueron leidos
            if (filtroP.Read())
            {
                producto[0] = filtroP["nombre"].ToString();
                producto[1] = filtroP["codigo"].ToString();
                producto[2] = filtroP["cantidad"].ToString();
                producto[3] = filtroP["caducidad"].ToString();
                producto[4] = filtroP["idproveedor"].ToString();
                producto[5] = filtroP["precio"].ToString();
            }

            ConectarTienda.Close();
            return producto;
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        //ALMACENA PROVEEDOR EN LA BASE DE DATOS

        public int AlmacenaProveedor(String nombre, String direccion, String Ubicacion, String contacto)
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
            MySqlCommand ingresaDatosProducto = new MySqlCommand("insert into proveedor values(0, @name, @dire, @loc, @contact)", ConectarTienda);

            //Se asociona los parametros en el orden de almacenamiento 
            ingresaDatosProducto.Parameters.AddWithValue("@name", nombre);
            ingresaDatosProducto.Parameters.AddWithValue("@dire", direccion);
            ingresaDatosProducto.Parameters.AddWithValue("@loc", Ubicacion);
            ingresaDatosProducto.Parameters.AddWithValue("@contact", contacto);

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
            MySqlCommand ListaProductos = new MySqlCommand("select nombre as Nombre ,direccion as Direccion ,ubicacion as Ubicacion, contacto as Contacto from proveedor", ConectarTienda);

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
            MySqlCommand ListaProveedor = new MySqlCommand("select idproveedor, nombre from proveedor", ConectarTienda);

            //Tomar los registros de la consulta
            MySqlDataAdapter listado = new MySqlDataAdapter(ListaProveedor);

            //Carga en memoria los datos de la consulta
            DataSet datosProveedor = new DataSet();

            //Genera el listado de los registros de la consulta
            listado.Fill(datosProveedor);

            consultaGeneral.ValueMember = "idproveedor";
            consultaGeneral.DisplayMember = "nombre";

            //Se entrega los datos al contenedor del DataGridView
            consultaGeneral.DataSource = datosProveedor.Tables[0];

            ConectarTienda.Close();
        }

        public String[] CargarDatosProveedor3(int idproveedor)
        {
            //Declaramos un vector de 4 celdas para los datos del platillo seleccionado
            String[] Proveedor = new String[5];
            ConectarBD();
            ConectarTienda.Open();
            MySqlCommand datosP = new MySqlCommand("select nombre, direccion, ubicacion, contacto from proveedor where idproveedor=@cod", ConectarTienda);
            datosP.Parameters.AddWithValue("@cod", idproveedor);
            //Realizar la consulta
            MySqlDataReader filtroP = datosP.ExecuteReader();
            //Revisar si los datos fueron leidos
            if (filtroP.Read())
            {
                Proveedor[0] = filtroP["nombre"].ToString();
                Proveedor[1] = filtroP["direccion"].ToString();
                Proveedor[2] = filtroP["ubicacion"].ToString();
                Proveedor[3] = filtroP["contacto"].ToString();

            }
            ConectarTienda.Close();
            return Proveedor;
        }

        //-------------------------------------------------------------------------------------------------------------------------------//
        public void CargarDatosCaducados(DataGridView consultaGeneral)
        {
            ConectarBD();
            ConectarTienda.Open();

            // Realiza la consulta de Base de datos con la conexion
            MySqlCommand ListaProductos = new MySqlCommand("SELECT nombre, CASE WHEN caducidad < CURDATE() THEN 'Caducado' WHEN caducidad <= DATE_ADD(CURDATE(), INTERVAL 6 DAY) THEN 'Proximo' ELSE 'Vigente' END AS estado FROM tienda.productos WHERE cantidad > 0 ORDER BY caducidad ASC", ConectarTienda);

            // Tomar los registros de la consulta
            MySqlDataAdapter listado = new MySqlDataAdapter(ListaProductos);

            // Carga en memoria los datos de la consulta
            DataSet datosProveedor = new DataSet();

            // Genera el listado de los registros de la consulta
            listado.Fill(datosProveedor);

            // Se entrega los datos al contenedor del DataGridView
            consultaGeneral.DataSource = datosProveedor.Tables[0];

            ConectarTienda.Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------//

        public void MostrarRegistrosVenta(DataGridView dataGridView1)
        {
            // Limpiar el DataGridView antes de cargar los datos
            dataGridView1.DataSource = null;


            ConectarBD();
            ConectarTienda.Open();

            string consulta = "SELECT FechaVenta, Total, Vendedor FROM Venta ORDER BY FechaVenta DESC";

            MySqlCommand comando = new MySqlCommand(consulta, ConectarTienda);
            MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);

            dataGridView1.DataSource = dt;

            ConectarTienda.Close();
        }

        private DataTable ObtenerDatosTabla(string consulta)
        {
            // Aquí debes agregar tu código para obtener los datos de la base de datos según la consulta especificada
            // En este ejemplo, asumimos que ya tienes una función o método para obtener los datos de la base de datos

            // Crear una conexión a la base de datos y realizar la consulta
            MySqlConnection conexion = new MySqlConnection(CadenaConexion);
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
            DataTable tablaDatos = new DataTable();

            // Llenar la tabla con los datos obtenidos de la consulta
            adaptador.Fill(tablaDatos);

            // Cerrar la conexión a la base de datos
            conexion.Close();

            return tablaDatos;
        }





        //Fin
    }
}
