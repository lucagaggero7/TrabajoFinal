using System;
using System.Data;
using System.Data.OleDb;
using Entidades;

namespace Datos
{
    public class DatosUsuarios : DatosConexionBD
    {
        public int Compra(string accion, Usuario objUsuario, string listaProductos)
        {
            int usuarioId = 0;
            string orden = string.Empty;

            // Define el comando INSERT para agregar el usuario
            if (accion == "Comprar")
            {
                orden = "INSERT INTO Usuarios (Nombre, Direccion, Provincia, Localidad, CodigoPostal, Telefono, Dni) " +
                        "VALUES (@Nombre, @Direccion, @Provincia, @Localidad, @CodPostal, @Telefono, @Dni)";
            }

            // Crear el comando OleDb
            OleDbCommand cmd = new OleDbCommand(orden, conexion);
            cmd.Parameters.AddWithValue("@Nombre", objUsuario.Nombre);
            cmd.Parameters.AddWithValue("@Direccion", objUsuario.Direccion);
            cmd.Parameters.AddWithValue("@Provincia", objUsuario.Provincia);
            cmd.Parameters.AddWithValue("@Localidad", objUsuario.Localidad);
            cmd.Parameters.AddWithValue("@CodPostal", objUsuario.CodPostal);
            cmd.Parameters.AddWithValue("@Telefono", objUsuario.Telefono);
            cmd.Parameters.AddWithValue("@Dni", objUsuario.Dni);

            try
            {
                // Abre la conexión y ejecuta el INSERT
                Abrirconexion();
                cmd.ExecuteNonQuery();

                // Ejecutar una segunda consulta para obtener el ID generado
                cmd.CommandText = "SELECT @@IDENTITY";
                usuarioId = (int)cmd.ExecuteScalar(); // Obtiene el ID autonumérico generado
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de guardar el usuario.", e);
            }
            finally
            {
                // Cierra la conexión y libera recursos
                Cerrarconexion();
                cmd.Dispose();
            }

            // Retorna el ID del usuario recién creado para usarlo en otras operaciones
            return usuarioId;
        }
    }
}
