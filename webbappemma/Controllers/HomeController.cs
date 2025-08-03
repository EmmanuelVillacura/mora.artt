using Microsoft.AspNetCore.Mvc;
using webbappemma.Models;
using System.Collections.Generic;
using System.Linq;

namespace webbappemma.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productos = _context.Producto.Take(12).ToList(); // Obtén hasta 12 productos
            return View(productos);
        }

        // Acción para mostrar el formulario
        [HttpGet]
        public IActionResult CrearProducto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EliminarProducto(int id)
        {
            var producto = _context.Producto.Find(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Acción para procesar el formulario
        [HttpPost]
        public IActionResult CrearProducto(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Producto.Add(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }
    }
}