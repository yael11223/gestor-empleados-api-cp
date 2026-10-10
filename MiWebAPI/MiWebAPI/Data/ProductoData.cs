using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class ProductoData
    {

        private readonly string conexion;

        public ProductoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<ProductoDetalle>> Lista()
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.PRODUCTO_DETALLE_OBTENER", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new ProductoDetalle
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            NombreProducto = reader["NombreProducto"].ToString(),
                            precio = Convert.ToDecimal(reader["Precio"]),
                            Cantidad = Convert.ToInt32(reader["Cantidad"]),
                            Total = Convert.ToDecimal(reader["Total"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            FechaModificacion = reader["FechaModificacion"].ToString()
                        });
                    }
                }
            }
            return lista;
        }
        public async Task<bool> Crear(ProductoDetalle objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_CREAR_PRODUCTO_DETALLE", con);
                cmd.Parameters.AddWithValue("@nombreProducto", objeto.NombreProducto);
                cmd.Parameters.AddWithValue("@precio", objeto.precio);
                cmd.Parameters.AddWithValue("@cantidad", objeto.Cantidad);
                cmd.Parameters.AddWithValue("@total", objeto.Total);
                cmd.Parameters.AddWithValue("@descripcion", objeto.Descripcion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;


        }
        public async Task<bool> Editar(ProductoDetalle objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_EDITAR_PRODUCTO_DETALLE", con);
                cmd.Parameters.AddWithValue("@Id", objeto.Id);
                cmd.Parameters.AddWithValue("@nombreProducto", objeto.NombreProducto);
                cmd.Parameters.AddWithValue("@precio", objeto.precio);
                cmd.Parameters.AddWithValue("@cantidad", objeto.Cantidad);
                cmd.Parameters.AddWithValue("@total", objeto.Total);
                cmd.Parameters.AddWithValue("@descripcion", objeto.Descripcion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<ProductoDetalle> Obtener(int Id)
        {
            ProductoDetalle objeto = new ProductoDetalle();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_OBTENER_PRODUCTO_DETALLE", con);
                cmd.Parameters.AddWithValue("@IdProducto", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new ProductoDetalle
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            NombreProducto = reader["NombreProducto"].ToString(),
                            precio = Convert.ToDecimal(reader["Precio"]),
                            Cantidad = Convert.ToInt32(reader["Cantidad"]),
                            Total = Convert.ToDecimal(reader["Total"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            FechaModificacion = reader["FechaModificacion"].ToString()
                        };
                    }
                }
            }
            return objeto;
        }
        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_ELIMINAR_PRODUCTO_DETALLE", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;

        }

    }
}

        
