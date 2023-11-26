using Facturacion.Models;
using Facturacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Controllers
{
    public class FacturaController : Controller
    {
        private readonly FacturaService _facturaService;

        public FacturaController(FacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        public IActionResult Crear()
        {
            var viewModel = new FacturaViewModel
            {
                Clientes = _facturaService.ObtenerClientes(),
                Productos = _facturaService.ObtenerProductos()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Crear(FacturaViewModel facturaViewModel)
        {
            if (ModelState.IsValid)
            {
                _facturaService.GuardarFactura(facturaViewModel);
                return RedirectToAction("Crear");
            }

            facturaViewModel.Clientes = _facturaService.ObtenerClientes();
            facturaViewModel.Productos = _facturaService.ObtenerProductos();

            return View(facturaViewModel);
        }

        public IActionResult Mostrar(int id)
        {
            
            var factura = _facturaService.ObtenerFacturaPorId(id);

            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }
    }
}
