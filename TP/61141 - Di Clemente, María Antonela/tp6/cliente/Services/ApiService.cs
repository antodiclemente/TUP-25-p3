using System.Net.Http.Json;
using cliente.Models;


namespace cliente.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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
    public async Task<List<Producto>> ObtenerProductosAsync() {
        return await _httpClient.GetFromJsonAsync<List<Producto>>("api/productos");
    }

    public async Task<Producto> ObtenerProductoAsync(int id) {
        return await _httpClient.GetFromJsonAsync<Producto>($"api/productos/{id}");
    }

    public async Task<bool> CrearProductoAsync(Producto producto)
    {
    var response = await _httpClient.PostAsJsonAsync("api/productos", producto);

    if (!response.IsSuccessStatusCode)
    {
        var errorTexto = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Error del servidor: " + errorTexto);  // Esto te lo muestra en la consola del navegador (F12)
    }

    return response.IsSuccessStatusCode;
    }


    public async Task<bool> ActualizarProductoAsync(Producto producto) {
        var response = await _httpClient.PutAsJsonAsync($"api/productos/{producto.Id}", producto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> EliminarProductoAsync(int id) {
        var response = await _httpClient.DeleteAsync($"api/productos/{id}");
        return response.IsSuccessStatusCode;
    }
}

public class DatosRespuesta {
    public string Mensaje { get; set; }
    public DateTime Fecha { get; set; }
}
