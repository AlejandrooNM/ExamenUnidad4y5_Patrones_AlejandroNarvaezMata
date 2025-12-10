using System.Collections.Generic;

namespace RestauranteSimulacion.Infrastructure
{
    public interface IPedidoRepository
    {
        void Guardar(Pedido pedido);
        IEnumerable<Pedido> ObtenerTodos();
        void Limpiar();
    }
}
