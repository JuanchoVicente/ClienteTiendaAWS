using ClienteTienda.Models;
using ClienteTienda.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteTienda.Controllers
{
    public class CitasController : Controller
    {
        ServiceCitas service;

        public CitasController(ServiceCitas service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListaDeCitas()
        {
            List<Cita> cts = await this.service.GetCitasAsync(); 
            return View(cts);
        }
                
        public IActionResult NuevaCita()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevaCita(string tatuaje, int idtatuador
            , string tatuador, string fecha, string comentarios)
        {
            await this.service.NuevaCitaAsync(tatuaje, idtatuador, tatuador, fecha, comentarios);
            return RedirectToAction("Index","Home");
        }
                    
    }
}
