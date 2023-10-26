
using InsertadordeFilasaBD.Models;
using InsertadordeFilasaBD.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Security.Principal;

namespace InsertadordeFilasaBD.Controllers
{
    public class ProtocoloController : Controller
    {
        private readonly BdprotocoloContext _context;

        public ProtocoloController(BdprotocoloContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> ListadoProtocolos()
        {
            var protocolos = _context.Protocolos.Include(m => m.IdproductoNavigation);
            return View(await protocolos.ToListAsync());
        }


        public IActionResult crear()
        {
            ViewData["Productos"] = new SelectList(_context.Productos,"Idproducto", "Nameproduct");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> create(IFormFile file, ProtocoloViewModel protocolo)
        {
            Console.WriteLine("archivo", file);
            Console.WriteLine("Protocolo",protocolo);
            if(ModelState.IsValid)
            {
                using (var memorystrean = new MemoryStream())
                {
                    file.CopyTo(memorystrean);
                    var fileBytes = memorystrean.ToArray();
                    var filedata = Convert.ToBase64String(fileBytes);
                    var protocols = new Protocolo()
                    {
                        Namedocument = file.FileName,
                        Document = filedata,
                        Lote = protocolo.Lote,
                        Idproducto = protocolo.Idproducto
                    };
                    _context.Add(protocols);
                    await _context.SaveChangesAsync();
                   return RedirectToAction("ListadoProtocolos");
                }

            }
            ViewData["Productos"] = new SelectList(_context.Productos, "Idproducto", "Nameproduct");
            Console.WriteLine("No se pudo crear");
            return RedirectToAction("ListadoProtocolos");
        }

       public Protocolo obtenerPDF(int id)
        {
            Protocolo protocolo = _context.Protocolos.Find(id);
            if(protocolo != null)
            {
                var protocolos = new Protocolo();
                protocolos.Idprotocolo = id;
                protocolos.Namedocument = protocolo.Namedocument;
                protocolos.Document = protocolo.Document;
                protocolos.Lote = protocolo.Lote;
                protocolos.Idprotocolo = protocolo.Idproducto;
                return protocolos;
            }
            else
            {
                return null;
            }

        }

        [HttpGet]
        public IActionResult Descargar(int id)
        {
            Protocolo protocolo = obtenerPDF(id);
            if (protocolo != null && protocolo.Document != null)
            {
                var fileBytes = Convert.FromBase64String(protocolo.Document);
                var meemorystream = new MemoryStream(fileBytes);
                return File(meemorystream,"application/pdf", protocolo.Namedocument);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
