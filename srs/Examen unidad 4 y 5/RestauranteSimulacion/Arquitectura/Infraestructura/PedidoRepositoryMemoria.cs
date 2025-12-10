using System.Collections.Generic;

namespace RestauranteSimulacion.Infrastructure
{
    public class PedidoRepositoryMemoria : IPedidoRepository
    {
        private readonly List<Pedido> _pedidos = new();

        public void Guardar(Pedido pedido)
        {
            _pedidos.Add(pedido);
        }

        public IEnumerable<Pedido> ObtenerTodos()
        {
            return _pedidos;
        }

        public void Limpiar()
        {
            _pedidos.Clear();
        }
    }
}
