using System;
using System.Windows.Forms;

namespace Tienda
{
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
            this.toolTip1.SetToolTip(this.btn_Regresar, "Regresar");
        }

        private void btn_Registrar_Click(object sender, EventArgs e)
        {
            //Recibe la Cantidad de Registros Almacenados
            int recibeUsuariosRegistrados = 0;
            //Instanciamos el objeto de la clase Operaciones
            Operaciones op = new Operaciones();
            //Se envian los datos a almacenar en el metodo almacena Usuario
            recibeUsuariosRegistrados = op.RegistraUsuario(txt_nombre.Text, txt_nick.Text, txt_password.Text);
            ////Se evalua si se ingreso el registro
            if (recibeUsuariosRegistrados > 0)
            {
                //En casos de registro , manda el msj almacenado
                MessageBox.Show("!Usuario Registrado Con Exito¡", "Almacenado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //limpiar cajas de texto
                txt_nombre.Text = "";
                txt_nick.Text = "";
                txt_password.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Inicio regreso = new Inicio();
            regreso.Show();
            this.Hide();
        }

        private void txt_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_nick.Focus();
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
                btn_Registrar.Focus();
            }
        }




    }
}

