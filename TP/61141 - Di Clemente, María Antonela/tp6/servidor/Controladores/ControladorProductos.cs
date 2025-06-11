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
    }
}
