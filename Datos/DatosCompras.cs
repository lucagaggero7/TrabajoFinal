using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using Entidades;
using System.Security.Cryptography;

namespace Datos
{
    public class DatosCompras : DatosConexionBD
    {
       
        public int Compra(string accion, int idUsuario , string listaProductos)
        {

            int realizada;

            string orden = string.Empty;

            if (accion == "Comprar")
                orden = "INSERT INTO Compras (Carrito, UsuarioId) " +
                         "VALUES (@Productos, @idUsuario)";


            OleDbCommand cmd = new OleDbCommand(orden, conexion);
            cmd.Parameters.AddWithValue("@Productos", listaProductos);
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

            try
            {
                Abrirconexion();
                realizada = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Errror al tratar de guardar,borrar o modificar de Compras", e);
            }
            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }
            return realizada;
        }

    }
}
  

