using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoDatos;
using Configuracion;
using System.Data;

namespace Logica
{
    public class LogicaCliente
    {

        public int guardarCliente(Cliente pvo_cliente)
        {

            int idCliente = -1;
            ClsConfiguracion vlo_config = new ClsConfiguracion(); // instanciando el objeto que tiene la cadena de conexion
            AD_Cliente aD_Cliente = new AD_Cliente(vlo_config.getConectioString); // instanciando la clase que accede a los datos y pasandole a esta la cadena de conexion

            try
            {
                idCliente = aD_Cliente.guardarCliente(pvo_cliente);
            }
            catch (Exception)
            {

                throw;
            }

            return idCliente;

        }

        public string eliminarCliente(Cliente pvo_Cliente)
        {
            string mensaje = null;
            ClsConfiguracion vlo_config = new ClsConfiguracion(); // instanciando el objeto que tiene la cadena de conexion
            AD_Cliente aD_Cliente = new AD_Cliente(vlo_config.getConectioString); // instanciando la clase que accede a los datos y pasandole a esta la cadena de conexion

            try
            {
                // eliminando el cliente especificado
                mensaje = aD_Cliente.eliminar(pvo_Cliente);
            }
            catch (Exception)
            {

                throw;
            }

            return mensaje;
        }


        public Cliente obtenerRegistro(string pvc_condicion)
        {
            Cliente vlo_Cliente;
            AD_Cliente aD_Clientes;
            ClsConfiguracion vlo_config = new ClsConfiguracion();

            try
            {

                aD_Clientes = new AD_Cliente(vlo_config.getConectioString);
                vlo_Cliente = aD_Clientes.obtenerRegistro(pvc_condicion);

            }
            catch (Exception ex)
            {

                throw;

            }

            return vlo_Cliente;
        }

        public DataSet listarRegistros(string pvc_condicion)
        {
            DataSet vlo_dataSet;
            AD_Cliente vlo_AD_Clientes;
            ClsConfiguracion vlo_config = new ClsConfiguracion();

            try
            {

                vlo_AD_Clientes = new AD_Cliente(vlo_config.getConectioString);
                vlo_dataSet = vlo_AD_Clientes.listarRegistros(pvc_condicion);

            }
            catch (Exception ex)
            {

                throw;

            }

            return vlo_dataSet;
        }

    }
}
