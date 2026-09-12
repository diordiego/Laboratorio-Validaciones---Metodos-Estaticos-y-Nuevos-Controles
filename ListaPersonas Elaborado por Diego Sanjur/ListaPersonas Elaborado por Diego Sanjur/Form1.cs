using System;
using System.Collections;
using System.Windows.Forms;

namespace ListaPersonas_Elaborado_por_Diego_Sanjur
{
    public partial class Form1 : Form
    {
        //ArrayList para almacenar los objetos Persona
        //ArrayList pertenece al espacio de nombres System.Collections

        ArrayList listaPersonas = new ArrayList();

        //Objeto Persona que se está editando. Si es null, no hay fila seleccionada
        private Persona personaEnEdicion = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Persona miColaborador1 = new Persona();

            miColaborador1.Id = 1;
            miColaborador1.Nombres = "Elena Carolina";
            miColaborador1.Apellidos = "Gonzalez Rodríguez";
            miColaborador1.Correo = "elena.gonzalez@ejemplo.com";
            miColaborador1.Salario = 1500.00m;
            miColaborador1.FechaNacimiento = new DateTime(1990, 5, 15);
            listaPersonas.Add(miColaborador1);
            RefrescarGrid();

            //Mostrar la fecha en formato corto dentro del grid
            dgvDatos.Columns["FechaNacimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        //Valida todos los campos del formulario y entrega el ID y el salario ya convertidos.
        //Se extrajo a un método aparte para que los botones Nuevo y Editar la compartan
        private bool ValidarCampos(out int id, out decimal salario)
        {
            id = 0;
            salario = 0;

            if (!int.TryParse(txtIdEmpleado.Text, out id))
            {
                errorProvider1.SetError(txtIdEmpleado, "Ingrese un ID válido");
                txtIdEmpleado.Focus();
                return false; // <-- Interrumpe y finaliza la ejecución del método actual
            }
            else
            {
                errorProvider1.SetError(txtIdEmpleado, "");
            }

            if (txtNombres.Text == "")
            {
                errorProvider1.SetError(txtNombres, "Ingrese los nombres del Colaborador");
                txtNombres.Focus();
                return false;
            }
            else
            {
                errorProvider1.SetError(txtNombres, "");
            }

            if (txtApellidos.Text == "")
            {
                errorProvider1.SetError(txtApellidos, "Ingrese los apellidos del Colaborador");
                txtApellidos.Focus();
                return false;
            }
            else
            {
                errorProvider1.SetError(txtApellidos, "");
            }

            if (Utilidades.EsCorreoValido(txtEmail.Text) == false)
            {
                errorProvider1.SetError(txtEmail, "Ingrese un correo válido");
                txtEmail.Focus();
                return false;
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }

            if (!decimal.TryParse(txtSalario.Text, out salario))
            {
                errorProvider1.SetError(txtSalario, "Ingrese un salario válido");
                txtSalario.Focus();
                return false;
            }
            else
            {
                errorProvider1.SetError(txtSalario, "");
            }

            return true;
        }

        //Vuelve a enlazar el grid. El ArrayList no notifica sus cambios al DataGridView
        private void RefrescarGrid()
        {
            dgvDatos.DataSource = null; // Limpiar el DataSource antes de asignar la nueva lista
            dgvDatos.DataSource = listaPersonas;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            int id;
            decimal salario;

            if (!ValidarCampos(out id, out salario))
            {
                return;
            }

            Persona colaborador1 = new Persona();
            colaborador1.Id = id;
            colaborador1.Nombres = txtNombres.Text;
            colaborador1.Apellidos = txtApellidos.Text;
            colaborador1.Correo = txtEmail.Text;
            colaborador1.Salario = salario;
            colaborador1.FechaNacimiento = dtpFechaNacimiento.Value.Date;
            listaPersonas.Add(colaborador1);
            RefrescarGrid();

            personaEnEdicion = null;
        }

        //Carga en el formulario los datos de la fila que se seleccione en el grid
        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Si se hizo clic en el encabezado de columna, e.RowIndex vale -1
            if (e.RowIndex < 0)
            {
                return;
            }

            //DataBoundItem devuelve el objeto Persona de esa fila. Se usa en lugar de
            //listaPersonas[e.RowIndex] para que siga funcionando aunque se ordene el grid
            personaEnEdicion = (Persona)dgvDatos.Rows[e.RowIndex].DataBoundItem;

            txtIdEmpleado.Text = personaEnEdicion.Id.ToString();
            txtNombres.Text = personaEnEdicion.Nombres;
            txtApellidos.Text = personaEnEdicion.Apellidos;
            txtEmail.Text = personaEnEdicion.Correo;
            txtSalario.Text = personaEnEdicion.Salario.ToString();
            dtpFechaNacimiento.Value = personaEnEdicion.FechaNacimiento;
        }

        //Aplica los cambios al registro seleccionado
        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (personaEnEdicion == null)
            {
                MessageBox.Show("Seleccione primero un registro del grid.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            int id;
            decimal salario;

            if (!ValidarCampos(out id, out salario))
            {
                return;
            }

            //El ArrayList guarda referencias, no copias: al modificar las propiedades
            //del objeto se modifica directamente el elemento de la lista
            personaEnEdicion.Id = id;
            personaEnEdicion.Nombres = txtNombres.Text;
            personaEnEdicion.Apellidos = txtApellidos.Text;
            personaEnEdicion.Correo = txtEmail.Text;
            personaEnEdicion.Salario = salario;
            personaEnEdicion.FechaNacimiento = dtpFechaNacimiento.Value.Date;

            RefrescarGrid();
            LimpiarFormulario();
        }

        private void tsbLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        //Se separó del evento para poder llamarlo también desde el botón Editar
        private void LimpiarFormulario()
        {
            // Limpiar las cajas de texto
            txtIdEmpleado.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtEmail.Clear();
            txtSalario.Clear();

            // Restablecer la fecha al día de hoy
            dtpFechaNacimiento.Value = DateTime.Today;

            // Quitar los iconos de error que hayan quedado
            errorProvider1.SetError(txtIdEmpleado, "");
            errorProvider1.SetError(txtNombres, "");
            errorProvider1.SetError(txtApellidos, "");
            errorProvider1.SetError(txtEmail, "");
            errorProvider1.SetError(txtSalario, "");

            //Cancelar la edición en curso
            personaEnEdicion = null;

            // Devolver el foco al primer campo
            txtIdEmpleado.Focus();
        }
    }
}