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
			// opciones mediante transac sql 
			comando.Connection = conexion.AbrirConexion();
			comando.CommandText = "select * from productos";
            leer = comando.ExecuteReader();
			tabla.Load(leer);
			conexion.CerrarConexion();
			return tabla;
			// opcion meiante procedimiento alamcenado
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
