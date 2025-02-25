﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

using ClassEntidadesGP;
using LogicaNegociosGP;
using System.Configuration;

namespace WebFormsGP
{
    public partial class Registro : System.Web.UI.Page
    {
        LogicaFachada logicaFachada = new LogicaFachada();
        protected void Page_Load(object sender, EventArgs e)
        {            

            List<EntidadMarca> listaMarca = null;
            string h = "";
            if(!Page.IsPostBack)
            {
                listaMarca = logicaFachada.informacionMarcas(ref h);
                ddlMarca.Items.Clear();

                for (int q = 0; q < listaMarca.Count; q++)
                {
                    ddlMarca.Items.Add(new ListItem(listaMarca[q].Nombre, listaMarca[q].idMarca.ToString()));
                }
            }

            List<EntidadPuesto> listaPuesto = null;
            if(!Page.IsPostBack)
            {
                listaPuesto = logicaFachada.informacionPuesto(ref h);
                ddlPuesto.Items.Clear();

                for (int i = 0; i < listaPuesto.Count; i++)
                {
                    ddlPuesto.Items.Add(new ListItem(listaPuesto[i].Nombre, listaPuesto[i].idPuesto.ToString()));
                }
            }
        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsr.Text) || string.IsNullOrEmpty(txtPaswr.Text) || string.IsNullOrEmpty(txtClave.Text))
            {
                MessageBox.Show("Ingresa todos los datos que se piden");
            }
            else
            {
                string mensaje = txtPaswr.Text;
                int clave = Convert.ToInt32(txtClave.Text);
                string incrip = incriptarMensaje(mensaje, clave);

                EntidadUsuario nuevoU
                    = new EntidadUsuario()
                    {
                        NombreUsr = txtUsr.Text,
                        Contraseña = txtPaswr.Text,
                        ContraseñaIncrip = incrip,
                        idMarca = Convert.ToInt16(ddlMarca.SelectedValue),
                        idPuesto = Convert.ToInt16(ddlPuesto.SelectedValue),
                    };

                string cad = "";
                logicaFachada.InsertarUsuario(nuevoU, ref cad);
                MessageBox.Show(cad);
            }    
        }

        public string incriptarMensaje(string Mensaje, int key)
        {
            string letras = "abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            string code = "";
            string resp = "";

            for (int i = 0; i < Mensaje.Length; i++)
            {
                string letra = Mensaje.Substring(i, 1);
                int posicion = letras.IndexOf(letra);
                int encript = (posicion + key) % 27;

                if (encript < 0)
                {
                    encript = encript + 27;
                }

                if (Mensaje[i] == ' ')
                {
                    code += '/';
                }
                else
                {
                    code = code + letras.Substring(encript, 1);
                }
                resp = code;
            }
            resp = code;
            return code;
        }
    }
}