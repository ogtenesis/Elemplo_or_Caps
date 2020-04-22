using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDeDatos
{
    public class CD_Productos
    {
		private CD_conexion conexion = new CD_conexion();

		SqlDataReader leer; //para leer consultas
		DataTable tabla = new DataTable(); // donde almacenar el resultado
		SqlCommand comando = new SqlCommand(); // para ejecutar comandos

		public DataTable Mostrar()
		{
			//// opciones mediante transact sql 
            ///
			//comando.Connection = conexion.AbrirConexion();
			//comando.CommandText = "select * from productos";
   //         leer = comando.ExecuteReader();
			//tabla.Load(leer);
			//conexion.CerrarConexion();
			//return tabla;

            //// opción meiante procedimiento alamcenado

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "MostrarProductos"; // se cambia la consulta por  el procedimiento (sin el execute)
            comando.CommandType = CommandType.StoredProcedure;// se le indica al comando que es de tipo procedimiento
            leer = comando.ExecuteReader(); // este comando ejecuta y retorna lineas 
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }
        // método para insertar registros: los parametros seran del mismo tipo que las columnas de la tabla
        public void Insertar(string Nom,string Des, double Precio, int stk)
        {

           /* Detalles
           * ////// opciones mediante transact sql
            /////
            //comando.Connection = conexion.AbrirConexion();
            //comando.CommandText = "insert into productos values ('" + Nom + "','" + Des + "'," + Precio + "," + stk + ")";

            //comando.CommandType = CommandType.Text;//esto es porque arriba(linea 34)está de tipo procedimeinto y lo que sigue no es un precedimiento
            //                                       // y generaría un error.
            //comando.ExecuteNonQuery(); //este coamdo solo ejecuta instrucciones no retorna lineas
            //conexion.CerrarConexion();


            //// opciones mediante procediminto alamacenado
            /// primer modo lo use  y funcionó
            //comando.Connection = conexion.AbrirConexion();
            //comando.CommandText = "InsertarProductos '" + Nom + "','" + Des + "'," + Precio + "," + stk + ""; // una forma
            //comando.ExecuteNonQuery(); //este coamdo solo ejecuta instrucciones no retorna lineas
            //conexion.CerrarConexion();

            /// segundo  modo  el recomendado en los tutoriales 
            ///
            */
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "InsertarProductos";
            comando.CommandType = CommandType.StoredProcedure; // ojo en el modo uno este coamdo no va
            comando.Parameters.AddWithValue("@Nom", Nom); //@Nom y demas deben quedar igualitos a como está creado en el procedimiento
            comando.Parameters.AddWithValue("@Des", Des);
            comando.Parameters.AddWithValue("@Pre", Precio);
            comando.Parameters.AddWithValue("@Stk", stk);
            
            comando.ExecuteNonQuery(); //este coamdo solo ejecuta instrucciones no retorna lineas
            comando.Parameters.Clear(); //limpia el registro  
            conexion.CerrarConexion();
        }

        public void Editar(string Nom, string Des, double Precio, int stk,int id)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "EditarProductos";
            comando.CommandType = CommandType.StoredProcedure; // ojo en el modo uno este coamdo no va
            comando.Parameters.AddWithValue("@Nom", Nom); //@Nom y demas deben quedar igualitos a como está creado en el procedimiento
            comando.Parameters.AddWithValue("@Des", Des);
            comando.Parameters.AddWithValue("@Pre", Precio);
            comando.Parameters.AddWithValue("@Stk", stk);
            comando.Parameters.AddWithValue("@Id", id);

            comando.ExecuteNonQuery(); //este coamdo solo ejecuta instrucciones no retorna lineas
            comando.Parameters.Clear(); //limpia el registro
            conexion.CerrarConexion();
            //si no se limpiara el registo al mometo de usrlo en una inserta y luego editar 
            // los registrs continarian estando y se acumularian y daría un error por demaciados parámetros
        }

        public void Elimiar(int id)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "Delete productos where id ="+id;
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();
            

        }
        
    }

   
}
/*
 * Programacion por capas 
 * para este caso se tienen tres capas
 * a. CapaDeDatos: se encarga de realizar la conexión y tramitar las consultas
 *      para el ejemplo tiene dos clases
 *      CD_conexion == se ecarga estrictamente de hacer la conexión
 *      CD_Productos== se encarga de las consultas relacionadas con productos. 
 *      Si ubiese mas entidades, entonces provablemente tendria otras claes para  las consultas de estas.
 *      
 *  b.CapaDeNegocio: se encarga de realizar todas la converciones es decir recibe la infrmación
 *          de la capa de presentación la organiza y se la envia a la capa de  datos. Para este
 *          caso solo cuenta con una clase, dado que solo hay una entidad;
 *          CM_Productos== tiene los métodos que seran llamados por la capa de presentación.
 *          
 *  c.CapaDe presentación : se encarga de capturar y mostrar la información al usuario. Básicamente
 *          contendrá los formularios, botones campos y demas objetos de interación. Desde esta,
 *          se invocaran los métodos  de la capa de negocio enviando la informacion capturada o
 *          mostrando la informacion que  estos les retornan.
 *          
 *  Inicialmente se crea una solución en blanco. Luego se le va agregando un nuevo proyecto de tipo
 *   Biblioteca de clases (NET.FrameWork) y se van nombrando segun la capa a crear.
 *   luego de estar creadas todas las capas se conectan secuencialmente (referencian)  asi: 
 *   la capa de negocio refencia a la de dados, y la capa de presentacion referencia a la de negocio.
 *   ¿Cómo se hace? (clic derecho sobre la capa, agregar, referencia y se activa a la que se requiere)
 *   
 *  en las capas de datos y negocio simpre se deben agregar las librerias: Data y Data.sqlClien. Además
 *  se debe agregar junto a las librerias la capa que instanciará: Es decir si se está en la capa de 
 *  presentación de deberá agragar en el campo de las liberias using CapaDeCegocio. Si se está en la capa
 *  de negocio entones using CapaDeDatos;
 *  
 *   Por ultimo, para poder instanciar  una clase de una capa a otra ;  esta clase deverá  
 *   quedar publica, es decir public class..
 * */
