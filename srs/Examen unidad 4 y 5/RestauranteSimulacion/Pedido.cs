using System;
using System.Collections.Generic;

namespace RestauranteSimulacion
{
    public class Pedido
    {
        public int NumeroPedido { get; private set; }
        public decimal Total { get; private set; }

        public Pedido(int numeroPedido)
        {
            NumeroPedido = numeroPedido;
            Total = 0;
        }

        public void IniciarPedido(string cliente)
        {
            Console.WriteLine($"  [PEDIDO] Pedido {NumeroPedido} → INICIADO para {cliente}");
        }

        public void AgregarItem(string item, decimal precio)
        {
            Total += precio;
            Console.WriteLine($"    + {item} (${precio})");
        }

        public void FinalizarPedido()
        {
            Console.WriteLine($"  [PEDIDO] → FINALIZADO - Total: ${Total}");
        }

        public void Reiniciar()
        {
            Total = 0;
            Console.WriteLine($"  [PEDIDO] Pedido {NumeroPedido} → LIMPIADO y listo para reusar");
        }
    }
}
