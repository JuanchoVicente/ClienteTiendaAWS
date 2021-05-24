using ClienteTienda.Models;
using ClienteTienda.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteTienda.Controllers
{
    public class UsuariosController : Controller
    {
        ServiceUsuarios service;
        ServiceTatuaje servta;
        public UsuariosController(ServiceUsuarios service
            , ServiceTatuaje servta)
        {
            this.service = service;
            this.servta = servta;
        }

        public async Task<IActionResult> Index()
        {
            List<Usuario> us = await this.service.GetUsuariosAsync();
            return View(us);
        }

        public async Task<IActionResult> ListaUsuarios()
        {
            List<Usuario> us = await this.service.GetUsuariosAsync();
            return View(us);
        }

        public async Task<IActionResult> DetallesUsuario(int id)
        {
            ViewData["TATUAJES"] = await this.servta.GetTatuajesAsync();
            Usuario us = await this.service.BuscarUsuarioIdAsync(id);
            return View(us);
        }

        public IActionResult NuevoUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevoUsuario(string unombre, string upass, int uedad
            , string usalud, string ufhambur, string ufape, string ufvinos, int ucesta
            , int utelefono, string ucorre, string urrss, string urol)
        {
            await this.service.InsertarUsuarioAsync(unombre, upass, uedad, usalud, ufhambur
                , ufape, ufvinos, ucesta, utelefono, ucorre, urrss, urol, "");
            return RedirectToAction("Vitrinas","Home");
        }

        public async Task<IActionResult> ModificarUsuario(int id)
        {
            Usuario u = await this.service.BuscarUsuarioIdAsync(id);
            return View(u);
        }

        [HttpPost]
        public async Task<IActionResult> ModificarUsuario(int id, string unombre, string upass, int uedad
            , string usalud, string ufhambur, string ufape, string ufvinos, int ucesta
            , int utelefono, string ucorre, string urrss, string urol)
        {
            Usuario u = await this.service.BuscarUsuarioIdAsync(id);
            return View(u);
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string name, int id)
        {
            ViewData["ErrorLogin"] = "";
            if (await this.service.ExisteUsuarioAsync(name, id) == true)
            {
                return RedirectToAction("DetallesUsuario", "Usuarios", new { id = id });
            }
            else
            {
                ViewData["ErrorLogin"] = name + " no esta registrado o no existe. El identificador "
                    + id + " no esta registrado o no existe. ";
                return View(await this.service.BuscarUsuarioIdAsync(id));
            }
        }

        

    }
}
