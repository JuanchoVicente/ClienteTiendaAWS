using ClienteTienda.Models;
using ClienteTienda.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteTienda.Controllers
{
    public class MaterialesController : Controller
    {
        ServiceMateriales service;

        public MaterialesController(ServiceMateriales service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Material> aps = await this.service.GetMaterialAsync();
            return View(aps);
        }

        public async Task<IActionResult> ListaMateriales()
        {
            List<Material> aps = await this.service.GetMaterialAsync();
            return View(aps);
        }

        public async Task<IActionResult> DetallesMaterial(int id)
        {
            Material ap = await this.service.BuscarMaterialIdAsync(id);
            return View(ap);
        }

    }
}
