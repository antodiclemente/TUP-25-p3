using System.Net.Http.Json;
using System.Net.Http;
using cliente.Models;

namespace cliente.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /*public async Task AgregarAlCarritoAsync(Producto producto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/carrito", producto);

            if (!response.IsSuccessStatusCode)
            {
                var errorTexto = await response.Content.ReadAsStringAsync();
                throw new Exception($"No se pudo agregar el producto al carrito: {errorTexto}");
            }
        }
*/

        public async Task<DatosRespuesta> ObtenerDatosAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<DatosRespuesta>("/api/datos");
                return response ?? new DatosRespuesta { Mensaje = "No se recibieron datos del servidor", Fecha = DateTime.Now };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos: {ex.Message}");
                return new DatosRespuesta { Mensaje = $"Error: {ex.Message}", Fecha = DateTime.Now };
            }
        }

        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Producto>>("api/productos");
        }

        public async Task<Producto> ObtenerProductoPorIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Producto>($"api/productos/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CrearProductoAsync(Producto producto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/productos", producto);

            if (!response.IsSuccessStatusCode)
            {
                var errorTexto = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error del servidor: " + errorTexto);
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarProductoAsync(Producto producto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/productos/{producto.Id}", producto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarProductoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/productos/{id}");
            return response.IsSuccessStatusCode;
        }
    }

    public class DatosRespuesta
    {
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
    }
}
