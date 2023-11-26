using System.ComponentModel.DataAnnotations;

namespace Facturacion.Models
{
    public class DetalleFacturaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Producto es obligatorio.")]
        public int FacturaId { get; set; }
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El campo Cantidad es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        public int Cantidad { get; set; }
        public int TotalProducto { get; set; }
        public Producto Producto { get; set; }
    }
}