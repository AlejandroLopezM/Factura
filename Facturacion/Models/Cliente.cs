namespace Facturacion.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Contacto { get; set; }
        public TipoCliente TipoCliente { get; set; }
    }
}