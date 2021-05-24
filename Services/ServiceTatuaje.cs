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
    public class ServiceTatuaje
    {
        private Uri Uriapi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceTatuaje(String url)
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


        public async Task<List<Tatuaje>> GetTatuajesAsync()
        {
            String request = "api/tatuajes";
            List<Tatuaje> t = await this.CallApi<List<Tatuaje>>
                (request);
            return t;
        }

        public async Task<Tatuaje> BuscarTatuaje(int id)
        {
            String request = "/api/tatuajes/" + id;
            Tatuaje t = await this.CallApi<Tatuaje>(request);
            return t;
        }

        public async Task<List<int>> ListaPrecios()
        {
            String request = "/api/Tatuajes/ListaPrecios";
            List<int> precios = await this.CallApi<List<int>>(request);
            return precios;
        }

        public async Task<List<Tatuaje>> BuscarNombre(string nombre)
        {
            String request = "/api/Tatuajes/BuscarTatuajeNombre/" + nombre;
            List<Tatuaje> tatuajes = await this.CallApi<List<Tatuaje>>(request);
            return tatuajes;
        }

        public async Task NuevoTatuaje(string nombre, string autor
            , string size, string color, int precio, string imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/tatuajes";
                client.BaseAddress = this.Uriapi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Tatuaje t = new Tatuaje();
                t.Nombre = nombre;
                t.Autor = autor;
                t.Tamaño = size;
                t.Color = color;
                t.Precio = precio;
                t.Imagen = imagen;
                String json = JsonConvert.SerializeObject(t);
                StringContent content = new StringContent(json
                    , Encoding.UTF8, "application/json");
                await client.PostAsync(request, content);
            }
        }

        public async Task EliminarTatuajeAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/tatuajes?id=" + id;
                client.BaseAddress = this.Uriapi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                await client.DeleteAsync(request);
            }
        }
    }
}