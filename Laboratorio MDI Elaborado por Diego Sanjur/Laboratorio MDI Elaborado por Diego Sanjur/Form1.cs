using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorio_MDI_Elaborado_por_Diego_Sanjur
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tsbActivarBoton_Click(object sender, EventArgs e)
        {
            frmVentanaTexto ventanaTexto = Application.OpenForms.OfType<frmVentanaTexto>().FirstOrDefault();

            if (ventanaTexto != null)
            {
                ventanaTexto.BringToFront();
                ventanaTexto.Focus();
                // Si ya existía, volvemos a habilitar el botón de inmediato
            }

            else
            {
                //si no hay ventana abierta
                ventanaTexto = new frmVentanaTexto();
                ventanaTexto.MdiParent = this;
                ventanaTexto.Show();
            }
        }
    }
}
