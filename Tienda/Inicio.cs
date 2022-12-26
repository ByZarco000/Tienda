using System;
using System.Media; //libreria para reproducie audio
using System.Windows.Forms;
using System.Drawing;

namespace Tienda
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
            this.toolTip1.SetToolTip(this.btn_info,"Informacion");
        }

        private void btn_Iniciar_Click(object sender, EventArgs e)
        {
            //Manda "nombre" a oto form
            Operaciones opp = new Operaciones();
            String nombre = opp.IniciarSecion(Email.Text, password.Text);
            Menu mp = new Menu();
            mp.label1.Text = nombre;

            //comprueba si  no estas registrado

            if (nombre == "error")
            {
                //Datos Message Box ordenados c:
                string message = "¿Quieres registrarte?";
                string titulo = "¡USUARIO NO REGISTRADO!";
                MessageBoxButtons tipoboton = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                DialogResult result = MessageBox.Show(message, titulo, tipoboton, icon);

                //comprueva si das YES O NO en el message box
                if (result == DialogResult.Yes)
                {
                    Registro ir = new Registro();
                    ir.Show();
                    this.Hide();
                }
                else { }

            }
            else
            {
                // Si estas registrado el codigo continua
                mp.Show();
                this.Hide();

                SoundPlayer player = new SoundPlayer();
                player.SoundLocation = "C:/Users/cardr/Downloads/Imagenes/Interfaz/Gifs/stayin-alive-music.wav";
                player.Play();
            }
        }

        private void btn_Registrar_Click(object sender, EventArgs e)
        {
            Registro ir = new Registro();
            ir.Show();
            this.Hide();
        }

        private void Email_Click(object sender, EventArgs e)
        {
            if (Email.Text == "Escriba su nick name")
            {

                Email.Text = "";
                Email.ForeColor = Color.Black;
            }
        } 

        private void password_Click(object sender, EventArgs e)
        {
            if (password.Text == "123")
            {
                password.Text = "";
                password.ForeColor = Color.Black;
            }
        }

        private void Email_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                password.Focus();
            }
        }

        private void password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btn_Iniciar.Focus();
            }
        }

        private void btn_info_Click(object sender, EventArgs e)
        {
            panel_info1.Visible = true;
        }

        private void btn_info_KeyPress(object sender, KeyPressEventArgs e)
        {
            panel_info1.Visible = false;
        }






        //
    }
}
