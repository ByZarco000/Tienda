using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Media; //libreria para reproducie audio
using System.Windows.Forms;

namespace Tienda
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
            this.toolTip1.SetToolTip(this.btn_info, "Informacion");

            this.toolTip1.SetToolTip(this.Btn_Enviar, "Enviar");
        }

        private void Btn_Iniciar_Click_1(object sender, EventArgs e)
        {
            //Manda "nombre" a oto form
            Operaciones opp = new Operaciones();
            String nombre = opp.IniciarSecion(Nickname.Text, password.Text);
            Menu mp = new Menu();
            mp.label1.Text = nombre;

            //comprueba si  no estas registrado
            if (nombre == "error")
            {
                //Datos Message Box ordenados c:
                string message = "Pide Ayuda a tu 'Administrador'";
                string titulo = "¡USUARIO NO REGISTRADO!";
                MessageBoxButtons tipoboton = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                DialogResult result = MessageBox.Show(message, titulo, tipoboton, icon);

                //comprueva si das YES O NO en el message box
                if (result == DialogResult.OK)
                {
                    Inicio ir = new Inicio();
                    ir.Show();
                    this.Hide();
                }
            }
            else
            { // Si estas registrado el codigo continua
                mp.Show();
                this.Hide();

                //SoundPlayer player = new SoundPlayer();
                //player.SoundLocation = "./Gats.wav";
                //player.Play();

            }
        }

        //Barra de Nombre
        private void Nickname_Click_1(object sender, EventArgs e)
        {
            if (Nickname.Text == "Escriba su nick name")
            {
                Nickname.Text = "";
            }
        }

        //Mostrar info
        private void Btn_info_Click(object sender, EventArgs e)
        {
            Pnl_info1.Visible = true;
            Pnl_info2.Visible = false;
        }

        //Ocultar info
        private void Btn_info_KeyPress(object sender, KeyPressEventArgs e)
        {
            Pnl_info1.Visible = false;
            Pnl_info2.Visible = false;
        }

        //Salto de linea a Password
        private void Nickname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                password.Focus();
            }

        }

        //Salto de linea a boton iniciar
        private void Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btn_Iniciar.Focus();
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            if (password.Enabled == true)
            {

                if (password.PasswordChar == '*')
                {
                    password.PasswordChar = '\0';
                    pictureBox2.Visible = true;
                    pictureBox1.Visible = false;
                }
            }
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            if (password.PasswordChar == '\0')
            {
                password.PasswordChar = '*';
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
            }
        }

        private void Btn_Enviar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Pnl_info2.Visible = false;
            Pnl_info1.Visible = false;
        }

        private void Btn_Enviar_Click(object sender, EventArgs e)
        {
            // 1. Crear la conexión a la base de datos
            MySqlConnection connection = new MySqlConnection("server=localhost;user=root;database=tienda;port=3306;password=root");
            connection.Open();

            // 2. Crear la consulta SQL para recuperar los datos del empleado
            string query = "SELECT nick,pass,nombre FROM usuarios WHERE codigo = @codigo and idusuarios !=1";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@codigo", textBox1.Text);

            // 3. Ejecutar la consulta y recuperar los resultados
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Pic_Aceptado.Visible = true;
                Pic_Rechazado.Visible = false;
                Font myFont = new Font("Arial", 14, FontStyle.Bold);
                // 4. Mostrar el número y la contraseña del empleado
                MessageBox.Show("Nick Name: " + reader["nick"].ToString() + "\nContraseña: " + reader["pass"].ToString(), "Datos Empleado: " + reader["nombre"].ToString(), MessageBoxButtons.OK);

                textBox1.Text = "";
            }
            else
            {
                Pic_Aceptado.Visible = false;
                Pic_Rechazado.Visible = true;
                MessageBox.Show("¡Codigo de Empleado Incorrecto!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                textBox1.Text = "";
            }

            // 5. Cerrar la conexión a la base de datos
            reader.Close();
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pnl_info2.Visible = true;
            Pnl_info1.Visible = false;
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Pnl_info2.Visible = false;
            Pnl_info1.Visible = false;
        }

        private void Btn_Enviar_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Pnl_info2.Visible = false;
            Pnl_info1.Visible = false;
        }

        //Variable bool Statica, ya que se puede regresar al Form "Inicio"
        //private static bool isLoaded = false;
        private void Inicio_Load(object sender, EventArgs e)
        {
            //Utilizamos una variable booleana para verificar si el sonido ya se reprodujo o No,
            //Esto para que solo se reproduzca una vez (Al iniciar la App)

            //if (!isLoaded)
            //{
            //    SoundPlayer player = new SoundPlayer();
            //    player.SoundLocation = "./Inicio.wav";
            //    player.Play();
            //    isLoaded = true;
            //}
        }

        //Button Recuperar Constraseña
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Btn_Enviar.Focus();
            }
        }




        //FIN
    }
}
