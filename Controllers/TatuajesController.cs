using ClienteTienda.Models;
using ClienteTienda.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteTienda.Controllers
{
    public class TatuajesController : Controller
    {
        ServiceTatuaje service;

        public TatuajesController(ServiceTatuaje service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Tatuaje> tjs = await this.service.GetTatuajesAsync();
            return View(tjs);
        }

        public async Task<IActionResult> NuevoTatuaje()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevoTatuaje(string nombre, string autor
            , string tamaño, string color, int precio, string imagen)
        {
            await this.service.NuevoTatuaje(nombre, autor
                , tamaño, color, precio, imagen);
            return RedirectToAction("ListaTatuajes", "Tatuajes");
        }

       
        public async Task<IActionResult> ListaTatuajes()
        {
            List<Tatuaje> tatuajes = await this.service.GetTatuajesAsync();
            return View(tatuajes);
        }

        public async Task<IActionResult> DetallesTatuaje(int id)
        {
            List<int> precios = await this.service.ListaPrecios();
            Tatuaje t = await this.service.BuscarTatuaje(id);
            ViewData["PRECIOS"] = precios;
            return View(t);
        }

        [HttpPost]
        public async Task<IActionResult> DetallesTatuaje(int id, int idtatu
            , string nombretatus, int precios)
        {
            ViewData["PRECIOS"] = precios;
            Tatuaje t = await this.service.BuscarTatuaje(id);
            if (idtatu > 0)
            {
                return RedirectToAction("DetallesTatuaje", "Tatuajes", new { id = idtatu });
            }
            if(nombretatus != null)
            {
                List<Tatuaje> tatuajes = await this.service.BuscarNombre(nombretatus);
                ViewData["FILTRONOMBRE"] = tatuajes;
                return View(t);
            }
            return View(t);
        }
    }
}
