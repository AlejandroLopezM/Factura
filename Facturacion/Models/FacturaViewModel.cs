using System.ComponentModel.DataAnnotations;

namespace Facturacion.Models
{
    public class FacturaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Cliente es obligatorio.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El campo Número de Factura es obligatorio.")]
        public string NumeroFactura { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }

        public List<DetalleFacturaViewModel> DetalleFacturaViewModel { get; set; }

        public List<Producto> ProductosDisponibles { get; set; }

        public List<Cliente> Clientes { get; set; }

        public List<Producto> Productos { get; set; }

        public TipoCliente TipoCliente { get; set; }

    }
}

