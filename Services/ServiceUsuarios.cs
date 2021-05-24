using ClienteTienda.Helpers;
using ClienteTienda.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClienteTienda.Services
{
    public class ServiceUsuarios
    {
        private Uri Uriapi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceUsuarios(String url)
        {
            this.Uriapi = new Uri(url);
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApi<T>(String request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.Uriapi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }

            }
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            String request = "api/usuarios";
            List<Usuario> usuarios =
                await this.CallApi<List<Usuario>>(request);
            return usuarios;
        }

        public async Task<Usuario> BuscarUsuarioIdAsync(int id)
        {
            String request = "api/usuarios/buscarusuario/" + id;
            Usuario u = await this.CallApi<Usuario>(request);
            return u;
        }

        public async Task InsertarUsuarioAsync(string nombreuser, string passuser
            , int edad, string salud, string favham, string favape
            , string favvin, int cesta, int telefono
            , string correo, string rrss, string rol, string hora)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/usuarios/nuevousuario";
                client.BaseAddress = this.Uriapi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Usuario usuario = new Usuario();
                usuario.Nombre = nombreuser;
                String salt = CypherService.GetSalt();
                usuario.Salt = salt;
                byte[] respuesta =
                CypherService.CifrarContenido(passuser, salt);
                usuario.Pass = respuesta;
                usuario.Edad = edad;
                usuario.Salud = salud;
                usuario.FavsHamburguesas = favham;
                usuario.FavsAperitivos = favape;
                usuario.FavsVinos = favvin;
                usuario.Cesta = cesta;
                usuario.Telefono = telefono;
                usuario.Correo = correo;
                usuario.RedesSociales = rrss;
                usuario.Rol = rol;
                String json = JsonConvert.SerializeObject(usuario);
                StringContent content = new StringContent(json
                    , Encoding.UTF8, "application/json");
                await client.PostAsync(request, content);
            }
        }

        public async Task ModificarUsuarioAsync(int id, string nombreuser, string passuser
            , int edad, string salud, string favham, string favape
            , string favvin, int cesta, int telefono
            , string correo, string rrss, string rol, string hora)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/usuarios/modificarusuario";
                client.BaseAddress = this.Uriapi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Usuario usuario = await this.BuscarUsuarioIdAsync(id);
                usuario.Nombre = nombreuser;
                String salt = CypherService.GetSalt();
                usuario.Salt = salt;
                byte[] respuesta =
                CypherService.CifrarContenido(passuser, salt);
                usuario.Pass = respuesta;
                usuario.Edad = edad;
                usuario.Salud = salud;
                usuario.FavsHamburguesas = favham;
                usuario.FavsAperitivos = favape;
                usuario.FavsVinos = favvin;
                usuario.Cesta = cesta;
                usuario.Telefono = telefono;
                usuario.Correo = correo;
                usuario.RedesSociales = rrss;
                usuario.Rol = rol;
                String json = JsonConvert.SerializeObject(usuario);
                StringContent content = new StringContent(json
                    , Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }

        public async Task EliminarUsuarioAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/usuarios/eliminarusuario=?id"+id;
                client.BaseAddress = this.Uriapi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                await client.DeleteAsync(request);
            }
        }

        public async Task<Boolean> ExisteUsuarioAsync(string nombre, int id)
        {
            String request = "/api/Usuarios/UsuarioEnBDD/"+nombre+"/"+ id;
            Boolean esta = await this.CallApi<bool>(request);
            return esta;
        }

    }
}
