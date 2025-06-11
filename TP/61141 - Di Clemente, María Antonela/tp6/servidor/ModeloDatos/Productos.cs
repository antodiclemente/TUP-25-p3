using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace servidor.Models;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string ImagenUrl { get; set; }

    public List<ItemCompra> ItemsCompra { get; set; } = new List<ItemCompra>();
}    


