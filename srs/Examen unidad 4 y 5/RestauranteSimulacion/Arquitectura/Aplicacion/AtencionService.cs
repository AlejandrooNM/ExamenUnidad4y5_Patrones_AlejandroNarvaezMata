using System;
using System.Threading;

namespace RestauranteSimulacion.Application
{
    using RestauranteSimulacion.Infrastructure;

    public class AtencionService
    {
        private readonly ILogger _logger;
        private readonly IPedidoRepository _pedidoRepo;

        public AtencionService(ILogger logger, IPedidoRepository pedidoRepo)
        {
            _logger = logger;
            _pedidoRepo = pedidoRepo;
        }

        public void AtenderCliente(
            string nombreCliente,
            ObjectPool<Mesa> poolMesas,
            ObjectPool<Mesero> poolMeseros,
            ObjectPool<Pedido> poolPedidos,
            (string item, decimal precio)[] items,
            Func<int, ConsoleColor> seleccionarColor)
        {
            var gerente = GerenteRestaurante.ObtenerInstancia();
            var cliente = new Cliente(nombreCliente);

            EstiloConsola.Seccion($"Cliente: {nombreCliente}");
            cliente.CambiarEstado(ClienteEstado.Esperando);
            Thread.Sleep(TimeSpan.FromSeconds(15));

            var mesa = poolMesas.Obtener();
            var mesero = poolMeseros.Obtener();
            var pedido = poolPedidos.Obtener();

            gerente.RecibirCliente(nombreCliente);
            gerente.AsignarMesa(nombreCliente, mesa.NumeroMesa);

            var color = seleccionarColor(mesero.Id);
            var formateador = new ColorFormateador(color, new FormateadorMeseroBase());
            mesero.ConfigurarFormateador(formateador);
            mesero.AsignarCliente(nombreCliente);

            pedido.IniciarPedido(nombreCliente);
            foreach (var (item, precio) in items)
            {
                pedido.AgregarItem(item, precio);
            }

            cliente.CambiarEstado(ClienteEstado.Atendido);
            Thread.Sleep(TimeSpan.FromSeconds(15));

            pedido.FinalizarPedido();
            _logger.Info($"Pago total: ${pedido.Total}");
            _pedidoRepo.Guardar(pedido);

            cliente.CambiarEstado(ClienteEstado.Finalizado);
            gerente.RegistrarClienteAtendido();
            gerente.DespedirCliente(nombreCliente);

            poolMesas.Devolver(mesa);
            poolMeseros.Devolver(mesero);
            poolPedidos.Devolver(pedido);
        }
    }
}
