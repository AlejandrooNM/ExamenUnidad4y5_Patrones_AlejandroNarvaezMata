using System;
using System.Threading;
using RestauranteSimulacion.Infrastructure;
using RestauranteSimulacion.Application;

namespace RestauranteSimulacion
{
    class Program
    {
        static ObjectPool<Mesa> poolMesas = null!;
        static ObjectPool<Mesero> poolMeseros = null!;
        static ObjectPool<Pedido> poolPedidos = null!;

        static void Main(string[] args)
        {
            EstiloConsola.Titulo("Simulación de Restaurante");
            var gerente = GerenteRestaurante.ObtenerInstancia();
            gerente.AbrirRestaurante();

            InicializarPools();

            DemostrarTopePools();
            PoolCapacidadGlobal.ReiniciarConteo();
            InicializarPools();

            var logger = new ConsoleLogger();
            var pedidoRepo = new PedidoRepositoryMemoria();
            var atencion = new AtencionService(logger, pedidoRepo);

            atencion.AtenderCliente("Frenkie de Jong", poolMesas, poolMeseros, poolPedidos, new[] { ("Hamburguesa", 150m), (" Bacardi con coca ", 30m) }, SeleccionarColor);

            EstiloConsola.Seccion("Tiempo de Espera");
            Console.WriteLine("[TIEMPO] Esperando 30 segundos para el siguiente cliente...");
            Thread.Sleep(30000);

            atencion.AtenderCliente("Aitana Bombati", poolMesas, poolMeseros, poolPedidos, new[] { ("Pizza", 200m) }, SeleccionarColor);

            poolMesas.MostrarEstadisticas();
            poolMeseros.MostrarEstadisticas();
            poolPedidos.MostrarEstadisticas();

            gerente.CerrarRestaurante();
            Console.ReadKey();
        }

        static void InicializarPools()
        {
            poolMesas = new ObjectPool<Mesa>("Mesas", capacidadMaxima: 3);
            poolMesas.AgregarObjeto(new Mesa(1));
            poolMesas.AgregarObjeto(new Mesa(2));
            poolMesas.AgregarObjeto(new Mesa(3));
            poolMesas.AgregarObjeto(new Mesa(99));

            poolMeseros = new ObjectPool<Mesero>("Meseros", capacidadMaxima: 2);
            poolMeseros.AgregarObjeto(new Mesero(1, "Gilberto"));
            poolMeseros.AgregarObjeto(new Mesero(2, "Marcela"));
            poolMeseros.AgregarObjeto(new Mesero(99, "Extra"));

            poolPedidos = new ObjectPool<Pedido>("Pedidos", capacidadMaxima: 2);
            poolPedidos.AgregarObjeto(new Pedido(1));
            poolPedidos.AgregarObjeto(new Pedido(2));
            poolPedidos.AgregarObjeto(new Pedido(99));
        }


        static ConsoleColor SeleccionarColor(int meseroId)
        {
            var colores = new[] { ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Blue };
            int idx = (meseroId - 1) % colores.Length;
            return colores[idx];
        }

        static void DemostrarTopePools()
        {
            EstiloConsola.Seccion("Demostración de Tope de Capacidad");
            for (int i = 4; i <= 10; i++)
            {
                poolMesas.AgregarObjeto(new Mesa(i));
            }
            poolMesas.AgregarObjeto(new Mesa(100));
            poolMesas.AgregarObjeto(new Mesa(101));

            for (int i = 3; i <= 10; i++)
            {
                poolMeseros.AgregarObjeto(new Mesero(i, $"Mesero #{i}"));
            }
            poolMeseros.AgregarObjeto(new Mesero(100, "Mesero extra 1"));
            poolMeseros.AgregarObjeto(new Mesero(101, "Mesero extra 2"));

            for (int i = 3; i <= 10; i++)
            {
                poolPedidos.AgregarObjeto(new Pedido(i));
            }
            poolPedidos.AgregarObjeto(new Pedido(100));
            poolPedidos.AgregarObjeto(new Pedido(101));

            EstiloConsola.Seccion("Prueba de extracción hasta vaciar");
            for (int i = 0; i < 12; i++)
            {
                var m = poolMesas.Obtener();
                if (m == null) break;
            }
            for (int i = 0; i < 12; i++)
            {
                var me = poolMeseros.Obtener();
                if (me == null) break;
            }
            for (int i = 0; i < 12; i++)
            {
                var p = poolPedidos.Obtener();
                if (p == null) break;
            }

            poolMesas.MostrarEstadisticas();
            poolMeseros.MostrarEstadisticas();
            poolPedidos.MostrarEstadisticas();

            EstiloConsola.Seccion("Recuperación tras prueba");
            poolMesas.Devolver(new Mesa(-1));
        }
    }
}
