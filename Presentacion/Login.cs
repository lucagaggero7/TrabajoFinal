﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace Presentacion
{
    public partial class Login : FrmBase
    {
        //variables que cuentan los clicks de los textbox 
        int nombredeusuarioclick = 0;
        int contraseñaclick = 0;

        public Login()
        {
            InitializeComponent();
        }

        public void Login_Load(object sender, EventArgs e)
        {
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            this.ActiveControl = PanelBarraTitulo;

            //verificamos que el nombre de usuario y la contraseña existan en un usuario mediante un if
            // para permitir el login y dar paso al siguiente formulario
            if (txtNombredeusuario.Text == "admin" && txtContraseña.Text == "admin" || 
                txtNombredeusuario.Text == "usuario" && txtContraseña.Text == "1234")
            {
            }
            else
            {
              MessageBox.Show("Error de Usuario y/o contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (txtNombredeusuario.Text == "admin" && txtContraseña.Text == "admin")
            {
                FormAdmProductos frm3 = new FormAdmProductos();
                this.AddOwnedForm(frm3);
                frm3.Show();
                this.Hide();

                if (chkRecordar.Checked == false)
                {
                    txtNombredeusuario.Text = "Nombre de usuario";
                    txtNombredeusuario.ForeColor = System.Drawing.Color.DarkGray;

                    txtContraseña.Text = "Contraseña";
                    txtContraseña.ForeColor = System.Drawing.Color.DarkGray;
                    txtContraseña.PasswordChar = '\0';
                }
            }
            if (txtNombredeusuario.Text == "usuario" && txtContraseña.Text == "1234")
            {
                FormUsuarioBasic frm4 = new FormUsuarioBasic();
                this.AddOwnedForm(frm4);
                frm4.Show();
                this.Hide();

                if (chkRecordar.Checked == false)
                {
                    txtNombredeusuario.Text = "Nombre de usuario";
                    txtNombredeusuario.ForeColor = System.Drawing.Color.DarkGray;

                    txtContraseña.Text = "Contraseña";
                    txtContraseña.ForeColor = System.Drawing.Color.DarkGray;
                    txtContraseña.PasswordChar = '\0';
                }
            }
        }

        private void txtNombredeusuario_Click(object sender, EventArgs e)
        {
            nombredeusuarioclick++;

            if (nombredeusuarioclick >= 1 && txtNombredeusuario.Text == "Nombre de usuario")
            {
                txtNombredeusuario.Text = "";
            }
            else
            {
                return;
            }
        }

        private void txtContraseña_Click(object sender, EventArgs e)
        {
            contraseñaclick++;

            if (contraseñaclick >= 1 && txtContraseña.Text == "Contraseña")
            {
                txtContraseña.Text = "";
                txtContraseña.PasswordChar = '*';
            }
            else
            {
                return;
            }
        }

        //Ocultar caracteres contraseña
        private void txtContraseña_DoubleClick(object sender, EventArgs e)
        {
            txtContraseña.PasswordChar = '*';
        }


        //Ocultar caracteres contraseña
        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {
            contraseñaclick++;
            txtContraseña.PasswordChar = '*';
            txtContraseña.ForeColor = Color.Black;
        }

        private void txtNombredeusuario_TextChanged(object sender, EventArgs e)
        {
            txtNombredeusuario.ForeColor = Color.Black;
        }

        private void txtNombredeusuario_KeyDown(object sender, KeyEventArgs e)
        {
            nombredeusuarioclick++;

            if (nombredeusuarioclick >= 1 && txtNombredeusuario.Text == "Nombre de usuario")
            {
                txtNombredeusuario.Text = "";
            }
            else
            {
                return;
            }
        }
        private void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            contraseñaclick++;

            if (contraseñaclick >= 1 && txtContraseña.Text == "Contraseña")
            {
                txtContraseña.Text = "";
            }
            else
            {
                return;
            }
        }
        private void txtNombredeusuario_Leave(object sender, EventArgs e)
        {
            if (txtNombredeusuario.Text == "")
            {
                txtNombredeusuario.Text = "Nombre de usuario";
                txtNombredeusuario.ForeColor = Color.DarkGray;
            }
        }
        private void txtContraseña_Leave(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "")
            {
                txtContraseña.Text = "Contraseña";
                txtContraseña.ForeColor = Color.DarkGray;
                txtContraseña.PasswordChar = '\0';
            }
        }
        private void Login_Click(object sender, EventArgs e)
        {
            //Ejecutamos los eventos leave para ahorrar repeticiones de codigo
            txtNombredeusuario_Leave(sender, e);
            txtContraseña_Leave(sender, e);
            //

            //Sacamos el foco de los textbox para no generar bugs del cursor
            labelBienvenido.Focus();
            //
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Owner.Show();
            this.Hide();
        }
    }
}