using Microsoft.AspNetCore.Mvc;
using servidor.ModeloDatos;

namespace servidor.Controladores
{
    [ApiController]
    [Route("api/productos")]
    public class ControladorProductos : ControllerBase
    {
        private readonly TiendaContexto _contexto;

        public ControladorProductos(TiendaContexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Producto>> ObtenerTodos()
        {
            return Ok(_contexto.Productos.ToList());
        }

        [HttpGet("{id}")]


        public ActionResult<Producto> ObtenerPorId(int id)
        {
            var producto = _contexto.Productos.Find(id);

            if (producto == null)
                return NotFound(new { mensaje = "No se encontro el producto" });

            return Ok(producto);
        }
        [HttpPost]
        public ActionResult<Producto> CrearProducto([FromBody] Producto nuevoProducto)
        {
            _contexto.Productos.Add(nuevoProducto);
            _contexto.SaveChanges();

            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoProducto.Id }, nuevoProducto);
        }
        [HttpPut("{id}")]
public IActionResult ActualizarProducto(int id, [FromBody] Producto productoActualizado)
{
    var productoExistente = _contexto.Productos.Find(id);

    if (productoExistente == null)
        return NotFound(new { mensaje = "Producto no encontrado" });

    productoExistente.Nombre = productoActualizado.Nombre;
    productoExistente.Precio = productoActualizado.Precio;
    productoExistente.Stock = productoActualizado.Stock;
    productoExistente.Descripcion = productoActualizado.Descripcion;
    productoExistente.Categoria = productoActualizado.Categoria;

    _contexto.SaveChanges();

    return NoContent();
}

[HttpDelete("{id}")]
public IActionResult EliminarProducto(int id)
{
    var producto = _contexto.Productos.Find(id);

    if (producto == null)
        return NotFound(new { mensaje = "Producto no encontrado" });

    _contexto.Productos.Remove(producto);
    _contexto.SaveChanges();

    return NoContent();
}

}
}
