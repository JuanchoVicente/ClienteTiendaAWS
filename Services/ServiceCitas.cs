using ClienteTienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace ClienteTienda.Services
{
    public class ServiceCitas
    {
        private Uri Uriapi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceCitas(String url)
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

        public async Task<List<Cita>> GetCitasAsync()
        {
            String request = "api/citas";
            List<Cita> ap = await this.CallApi<List<Cita>>
                (request);
            return ap;
        }

        public async Task<Cita> GetBuscarCitaAsync(int id)
        {
            String request = "api/citas/buscarcita/"+id;
            Cita ap = await this.CallApi<Cita>(request);
            return ap;
        }

        public async Task NuevaCitaAsync(string tatuaje, int idtatuador
            , string tatuador, string fecha, string comentario)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/citas";
                client.BaseAddress = this.Uriapi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Cita c = new Cita();
                c.Tatuaje = tatuaje;
                c.IdUsuario = idtatuador;
                c.Tatuador = tatuador;
                c.Fecha = fecha;
                c.Comentarios = comentario;
                String json = JsonConvert.SerializeObject(c);
                StringContent content = new StringContent(json
                    , Encoding.UTF8, "application/json");
                await client.PostAsync(request, content);
            }
        }

        
    }
}
