using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Dominio
{
    public interface IPersistente//NO SE USA BORRARRRR
    {
        /// <summary>
        /// Metodo que devuelve un objeto de conexion a la base de datos.
        /// </summary>
        /// <returns></returns>
        SqlConnection ObtenerConexion();

        /// <summary>
        /// Metodo que retorna un data reader de una consulta.
        /// </summary>
        /// <param name="conexion"></param>
        /// <param name="comandoTexto"></param>
        /// <param name="comandoTipo"></param>
        /// <param name="comandoParametros"></param>
        /// <returns></returns>
        SqlDataReader EjecutarQuery(SqlConnection con, string comandoString,
            CommandType comandoTipo, List<SqlParameter> comandoParametros);

        /// <summary>
        /// Metodo que ejecuta un comando (no consulta) y devuelve la cantidad de filas afectadas
        /// </summary>
        /// <param name="conexion"></param>
        /// <param name="comandoTexto"></param>
        /// <param name="comandoTipo"></param>
        /// <param name="comandoParametros"></param>
        /// <param name="transaccion"></param>
        /// <returns></returns>
        int EjecutarNoQuery(SqlConnection con, string text,
           CommandType tipo, List<SqlParameter> parametros);

        int EjecutarNoQuery(SqlConnection con, string comandoString, CommandType comandoTipo, List<SqlParameter> comandoParametros, SqlTransaction transaccion);

    }
}
