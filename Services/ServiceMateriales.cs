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
    public class ServiceMateriales
    {
        private Uri Uriapi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceMateriales(String url)
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

        public async Task<List<Material>> GetMaterialAsync()
        {
            String request = "api/material";
            List<Material> materiales =
                await this.CallApi<List<Material>>(request);
            return materiales;
        }

        public async Task<Material> BuscarMaterialIdAsync(int id)
        {
            String request = "api/material/buscarmaterial/"+id;
            Material m = await this.CallApi<Material>(request);
            return m;
        }

        public async Task InsertarMaterialAsync(string nombre
            , string categoria, int precio, string descripcion, string imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/material/nuevomaterial";
                client.BaseAddress = this.Uriapi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Material m = new Material();
                m.Nombre = nombre;
                m.Categoria = categoria;
                m.Precio = precio;
                m.Descripcion = descripcion;
                m.Imagen = imagen;
                String json = JsonConvert.SerializeObject(m);
                StringContent content = new StringContent(json
                    , Encoding.UTF8, "application/json");
                await client.PostAsync(request, content);
            }
        }

        public async Task ModificarMaterialAsync(int id, string nombre,
            string categoria, int precio, string descripcion, string imagen) 
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/material/modificarmaterial";
                client.BaseAddress = this.Uriapi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Material m = await this.BuscarMaterialIdAsync(id);
                m.Nombre = nombre;
                m.Categoria = categoria;
                m.Precio = precio;
                m.Descripcion = descripcion;
                m.Imagen = imagen;
                String json = JsonConvert.SerializeObject(m);
                StringContent content = new StringContent(json
                    , Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }

        public async Task EliminarMaterialAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/material/eliminarmaterial=?id"+id;
                client.BaseAddress = this.Uriapi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                await client.DeleteAsync(request);
            }
        }

                
    }
}
