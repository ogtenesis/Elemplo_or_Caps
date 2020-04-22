using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDeNegocio;

namespace CapaDePresentacion
{
    
    public partial class Form1 : Form
    {
        CN_Productos objetoCN = new CN_Productos();
        private string idProducto = null;
        private bool Editar = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarProdutos();
        }


        private void MostrarProdutos()
        {
            //para mostrar es recomendable usar otro objeto distinto al de la inserciones
            //par evitar que se muestre duplicadala información

            CN_Productos objetoCN1 = new CN_Productos();
            dataGridView1.DataSource = objetoCN1.MostrarProd();
    }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(Editar == false)
            {

           
            try
            {
                //se envian los tectos tal cual se capturan de tipo string en el formulario no se hace 
                //ninguna converción
                objetoCN.InsertarProd(txtNombre.Text, textDescrip.Text, textPrecio.Text, txtStock.Text);
                MessageBox.Show("Se insertó correctamente");
                    
                MostrarProdutos();
                 limpiar();
            }
            catch(Exception er)
            {
                MessageBox.Show("Ha ocurrrido un error al insetar los datos " + er);
            }
            }
            if(Editar)
            {
                try
                {
                    //se envian los textos tal cual se capturan de tipo string en el formulario no se hace 
                    //ninguna converción
                    objetoCN.EditarProd(txtNombre.Text, textDescrip.Text, textPrecio.Text, txtStock.Text,idProducto);
                    MessageBox.Show("Se Editó correctamente");
                    Editar = false;
                    MostrarProdutos();
                    limpiar();
                }
                catch (Exception er)
                {
                    MessageBox.Show("Ha ocurrrido un error al editar los datos " + er);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
        if(dataGridView1.SelectedRows.Count  > 0)
            {
                Editar = true;
                txtNombre.Text = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                textDescrip .Text = dataGridView1.CurrentRow.Cells["Descripcion"].Value.ToString();
                textPrecio.Text = dataGridView1.CurrentRow.Cells["Precio"].Value.ToString();
                txtStock.Text = dataGridView1.CurrentRow.Cells["Stock"].Value.ToString();
                idProducto = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
              // el data grid  toma los titulos tal y como estpan creados en la base de datos  
            }
            else
            {
                MessageBox.Show("Selecione la fila a editar");
            }
        }
        private void limpiar()
        {
            textDescrip.Clear(); // dos maneras distintas para limpiar un texto
            txtNombre.Clear();
            textPrecio.Text = "";
            txtStock.Text = ""; 
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                idProducto=  dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                objetoCN.EliminarProd(idProducto);
                MessageBox.Show("Producto Eliminado");
                MostrarProdutos();
            }
            else
            {
                MessageBox.Show("Elija una fiala para eliminar");
            }
        }
    }
}
