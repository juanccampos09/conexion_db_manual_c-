using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class AD_Cliente
    {

        string vgc_cadenaConexion; 

        public AD_Cliente(string pvc_cadenaConexion)
        {
            vgc_cadenaConexion = pvc_cadenaConexion;
        }

        public int guardarCliente(Cliente pvo_Cliente)
        {
            SqlParameter parametroDevuelto; // objeto para obtener el valor que devuelve el parametro de tipo OUT
            SqlConnection vlo_sqlConexion = new SqlConnection(vgc_cadenaConexion);
            SqlCommand vlo_sqlCommand = new SqlCommand("SP_GUARDAR_CLIENTE", vlo_sqlConexion); // indicando cual procedimiento almacenado se va a utilizar
            int idCliente = -1;

            parametroDevuelto = vlo_sqlCommand.Parameters.AddWithValue("@ID", pvo_Cliente.Id); // agregando un paramentro a la variable command y asignandolo dentro de una variable de tipo sqpParameter para agregarle mas datos
            parametroDevuelto.Direction = ParameterDirection.InputOutput; // agregando el tipo de salida que tiene el paramentro
            vlo_sqlCommand.Parameters.AddWithValue("@NOMBRE", pvo_Cliente.Nombre); // agregando paramentros con valores
            vlo_sqlCommand.Parameters.AddWithValue("@TELEFONO", pvo_Cliente.Telefono);

            vlo_sqlCommand.CommandType = CommandType.StoredProcedure; // indicando que se trata de un procedimiento almacenado
            
            try
            {
                vlo_sqlConexion.Open();
                vlo_sqlCommand.ExecuteNonQuery();

                idCliente = int.Parse(vlo_sqlCommand.Parameters["@ID"].Value.ToString()); // obteniendo el valor del parametro de tipo OUT indicado anteriormente 
                vlo_sqlConexion.Close();

            }
            catch (Exception)
            {

                throw new Exception("Error al intentar guardar el cliente, por favor intentelo de nuevo!");
            }
            finally
            {

                vlo_sqlConexion.Dispose();
                vlo_sqlCommand.Dispose();

            }

            return idCliente;
        }


        public string eliminar(Cliente pvo_Cliente)
        {

            SqlParameter parametroDevuelto; // objeto para obtener el valor que devuelve el parametro de tipo OUT
            SqlConnection vlo_sqlConexion = new SqlConnection(vgc_cadenaConexion);
            SqlCommand vlo_sqlCommand = new SqlCommand("SP_ELIMINAR_CLIENTE", vlo_sqlConexion); // indicando cual procedimiento almacenado se va a utilizar
            String mensaje = null;

            vlo_sqlCommand.Parameters.AddWithValue("@ID", pvo_Cliente.Id); // agregando paramentros con valores
            parametroDevuelto = vlo_sqlCommand.Parameters.AddWithValue("@MSJ", ""); // agregando un paramentro al command y asignandolo dentro de una variable de tipo sqlParameter para agregarle mas datos.
            parametroDevuelto.Size = 100; // agregando el tamaño del varchar para que devuelva el mensaje completo. Sino se especifica, este devuelve solo el primer caracter
            parametroDevuelto.Direction = ParameterDirection.InputOutput; // agregando el tipo de salida que tiene el paramentro
           
            vlo_sqlCommand.CommandType = CommandType.StoredProcedure; // indicando que se trata de un procedimiento almacenado

            try
            {
                vlo_sqlConexion.Open();
                vlo_sqlCommand.ExecuteNonQuery();

                mensaje = vlo_sqlCommand.Parameters["@MSJ"].Value.ToString(); // obteniendo el valor del parametro de tipo OUT indicado anteriormente 
                vlo_sqlConexion.Close();

            }
            catch (Exception)
            {

                throw new Exception("Error al intentar eliminar el cliente, por favor intentelo de nuevo!");
            }
            finally
            {

                vlo_sqlConexion.Dispose();
                vlo_sqlCommand.Dispose();

            }

            return mensaje;

        }


        public Cliente obtenerRegistro(string pvc_Condicion)
        {
            Cliente vlo_cliente = new Cliente();
            SqlConnection vlo_Conexion = new SqlConnection(vgc_cadenaConexion);
            SqlCommand vlo_Command = new SqlCommand();
            SqlDataReader vlo_dataReader; // objeto que obtiene los datos de la consulta
            string vlc_sentencia = null;

            vlo_Command.Connection = vlo_Conexion;
            vlc_sentencia = "SELECT ID, NOMBRE, TELEFONO FROM CLIENTE";

            // verificando si se ha especificado una condicion
            if (!string.IsNullOrEmpty(pvc_Condicion))
            {
                // agregando la condicion a la sentencia
                vlc_sentencia = string.Format("{0} WHERE {1}", vlc_sentencia, pvc_Condicion);

            }

            try
            {

                vlo_Command.CommandText = vlc_sentencia; // indicandole al objeto comando la consulta a realizar
                vlo_Conexion.Open();
                vlo_dataReader = vlo_Command.ExecuteReader(); // ejecutando la consulta y guardando el resultado en el dataReader

                // verificando si la consulta devolvio algun resultado
                if (vlo_dataReader.HasRows)
                {

                    vlo_dataReader.Read(); // leyendo el primer valor devuelto por la consulta
                    // leyendo el primer campo devuelto por la consulta el cual siempre inicia en 0
                    vlo_cliente.Id = vlo_dataReader.GetInt32(0);
                    vlo_cliente.Nombre = vlo_dataReader.GetString(1);
                    vlo_cliente.Telefono = vlo_dataReader.GetString(2);

                }

                vlo_Conexion.Close();

            }
            catch (Exception ex)
            {

                throw;

            }
            finally
            {
                // eliminando los objetos de la memoria
                vlo_Command.Dispose();
                vlo_Conexion.Dispose();

            }


            return vlo_cliente;



        }


        public DataSet listarRegistros(string pvc_Condicion)
        {

            DataSet vlo_DataSet = new DataSet(); // objeto que guarda todos los registros que devuelve la consulta
            SqlConnection vlo_Conexion = new SqlConnection(vgc_cadenaConexion);
            SqlDataAdapter vlo_dataAdapter;
            string vlc_Sentencia = null;

            try
            {

                vlc_Sentencia = "SELECT ID, NOMBRE, TELEFONO FROM CLIENTE";

                // verificando si se especifico alguna condicion
                if (!string.IsNullOrEmpty(pvc_Condicion))
                {
                    // agregando la condicion a la sentencia
                    vlc_Sentencia = string.Format("{0} WHERE {1}", vlc_Sentencia, pvc_Condicion);

                }

                // instanciando el objeto que se encarga de realizar la consulta con los paramentros de la sentencia y de la conexion a la base de datos
                vlo_dataAdapter = new SqlDataAdapter(vlc_Sentencia, vlo_Conexion); 
                // llenando el dataset con los registros obtenidos de la consulta
                vlo_dataAdapter.Fill(vlo_DataSet, "Clientes");

            }
            catch (Exception ex)
            {

                throw;

            }
            finally
            {

                vlo_Conexion.Dispose();

            }


            return vlo_DataSet;

        }

    }
}
