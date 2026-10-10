namespace MiWebAPI.Models
{
    public class ProductoDetalle
    {
        public long Id{ get; set; }
        public string? NombreProducto { get; set; }
        public decimal precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public string? Descripcion { get; set; }

        public string? FechaModificacion { get; set; }
    }
}
