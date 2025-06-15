using cliente.Models;

namespace cliente.Services
{
    public class CarritoService
    {
        public List<ItemCarrito> Items { get; private set; } = new();

    
    // Agrega un producto al carrito o aumenta la cantidad si ya está, sin pasar el stock disponible
        public void AgregarProducto(Producto producto)
        {
            var item = Items.FirstOrDefault(i => i.Producto.Id == producto.Id);
            if (item != null)
            {
                if (item.Cantidad < producto.Stock)
                    item.Cantidad++;
            }
            else
            {
                Items.Add(new ItemCarrito { Producto = producto, Cantidad = 1 });
            }
        }
        public void QuitarProducto(Producto producto)
        {
            var item = Items.FirstOrDefault(i => i.Producto.Id == producto.Id);
            if (item != null)
            {
                Items.Remove(item);
            }
        }


        public void Vaciar() => Items.Clear();
        public int CantidadTotal() => Items.Sum(i => i.Cantidad);
        public decimal Total() => Items.Sum(i => i.Cantidad * i.Producto.Precio);
        public int TotalItems => Items.Sum(p => p.Cantidad);


    }

    public class ItemCarrito
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }

}
