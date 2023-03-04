using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AgendaElectronica
{
    public partial class Form1 : Form
    {
        private List<Contacto> contactoList=new List<Contacto>();
        private int indice = -1;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader lector = new StreamReader("AgendaElectronica.txt");
                string linea;
                while ((linea = lector.ReadLine()) != null)
                {
                    int posicion;
                    Contacto persona = new Contacto();
                    posicion = linea.IndexOf("|");
                    persona.Nombres = linea.Substring(0, posicion);
                    linea = linea.Substring(++posicion);
                    posicion = linea.IndexOf("|");
                    persona.Apellidos = linea.Substring(0, posicion);
                    linea = linea.Substring(++posicion);
                    posicion = linea.IndexOf("|");
                    persona.Telefono = linea.Substring(0, posicion);
                    linea = linea.Substring(++posicion);
                    posicion = linea.IndexOf("|");
                    persona.Email = linea.Substring(0, posicion);
                    contactoList.Add(persona);
                }
                lector.Close();
                actualizaVista();
            }
             catch (Exception ex)
            {
                Console.WriteLine("Exception: "+ ex.Message);
               
            }
            finally
            {
                Console.WriteLine("Lista Cargada");
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Contacto persona = new Contacto();
            persona.Nombres=tbNombres.Text;
            persona.Apellidos=tbApellidos.Text;
            persona.Telefono=tbTelefono.Text;
            persona.Email=tbEmail.Text;
            if (indice>-1)
            {
                contactoList[indice] = persona;
                indice = -1;

            }
            else
            {
                contactoList.Add(persona);
            }
            actualizaVista();
            limpiarcampos();

        }
        private void actualizaVista()
        {
            dgvLista.DataSource = null;
            dgvLista.DataSource = contactoList;
            dgvLista.ClearSelection();

        }

        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewRow renglon = dgvLista.SelectedRows[0];
            indice = dgvLista.Rows.IndexOf(renglon);
            Contacto persona = contactoList[indice];
            tbNombres.Text = persona.Nombres;
            tbApellidos.Text = persona.Apellidos;
            tbTelefono.Text = persona.Telefono;
            tbEmail.Text = persona.Email;
        }
        private void limpiarcampos()
        {
            tbNombres.Text = "";
            tbApellidos.Text = "";
            tbTelefono.Text = "";
            tbEmail.Text = "";
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (indice>-1)
            {
                contactoList.RemoveAt(indice);
                actualizaVista();
                limpiarcampos();
                indice = -1;
            }
        }

        private void btnGuardarArchivo_Click(object sender, EventArgs e)
        {
            TextWriter Escribir = new StreamWriter("AgendaElectronica.txt");
            foreach (Contacto persona in contactoList)
            {
                Escribir.WriteLine(persona.Nombres+"|"+ persona.Apellidos + "|" + persona.Telefono + "|" + persona.Email + "|");
            }
            Escribir.Close();
            MessageBox.Show("Agenda Creada");
        }
    }
}
