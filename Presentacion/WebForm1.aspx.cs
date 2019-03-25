using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Logica;

namespace Presentacion
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listarDatos();
                txt_id.Text = "-1";// colocando por defecto el -1 si no se ha seleccionado algun registro
            }

            
        }

        private void listarDatos()
        {
            LogicaCliente vlo_logicaCliente = new LogicaCliente();
            grd_listaClientes.DataSource = vlo_logicaCliente.listarRegistros(null); // obteniendo los registros de la base de datos
            grd_listaClientes.DataBind(); // necesario para que se muestren los registros
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int idCliente = -1;
            Cliente cliente = new Cliente();
            LogicaCliente logicaCliente = new LogicaCliente();
            cliente.Id = int.Parse(txt_id.Text);
            cliente.Nombre = txt_nombre.Text;
            cliente.Telefono = txt_telefono.Text;

            try
            {
               idCliente = logicaCliente.guardarCliente(cliente);

                listarDatos(); //cargando los registros en el grid
            }
            catch (Exception)
            {

                // ocurrio un error
            }

            

        }

        protected void LinkButton1_Command(object sender, CommandEventArgs e)
        {
            // obteniendo el id del registro
            int idCliente = int.Parse(e.CommandArgument.ToString());

            // cargando los datos del cliente en los textbox
            cargarDatos(idCliente);
        }

        private void cargarDatos(int idCliente)
        {

            Cliente vlo_cliente = new Cliente();
            LogicaCliente vlo_logicaCliente = new LogicaCliente();

            try
            {
                vlo_cliente = vlo_logicaCliente.obtenerRegistro(string.Format("ID = {0}", idCliente));
                txt_id.Text = vlo_cliente.Id.ToString();
                txt_nombre.Text = vlo_cliente.Nombre;
                txt_telefono.Text = vlo_cliente.Telefono;
            }
            catch (Exception)
            {

                // no se pudo obtener el cliente
            }

        }

        protected void lnk_eliminar_Command(object sender, CommandEventArgs e)
        {
            // obteniendo el id del registro
            int idCliente = int.Parse(e.CommandArgument.ToString());

            // eliminando el registro
            eliminarRegistro(idCliente);
        }

        public void eliminarRegistro(int pvn_idCliente)
        {
            Cliente vlo_cliente = new Cliente();
            LogicaCliente vlo_logicaCliente = new LogicaCliente();
            vlo_cliente.Id = pvn_idCliente;
            string vlc_mensaje = null;

            try
            {
                vlc_mensaje = vlo_logicaCliente.eliminarCliente(vlo_cliente);
                listarDatos(); //cargando los registros en el grid
            }
            catch (Exception)
            {

                // error al intentar eliminar el cliente
            }

        }
    }
}