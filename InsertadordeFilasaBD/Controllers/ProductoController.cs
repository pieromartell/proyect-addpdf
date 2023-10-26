using InsertadordeFilasaBD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsertadordeFilasaBD.Controllers
{
    public class ProductoController : Controller
    {
        private readonly BdprotocoloContext _context;

        public ProductoController(BdprotocoloContext context)
        {
            _context = context;
        }

        public async Task< IActionResult> Index()
        {
            var productos = await _context.Productos.ToListAsync();
            return View(productos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                var productos = new Producto()
                {
                    Nameproduct = producto.Nameproduct,
                };
                _context.Productos.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        

    }
}
