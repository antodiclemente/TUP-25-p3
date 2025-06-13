using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace servidor.ModeloDatos;

public class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string ImagenUrl { get; set; } = string.Empty;

    public string Categoria { get; set; } = string.Empty;


    public List<ItemCompra> ItemsCompra { get; set; } = new List<ItemCompra>();
}    


