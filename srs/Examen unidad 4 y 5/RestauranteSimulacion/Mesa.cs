using System;

namespace RestauranteSimulacion
{
    public class Mesa
    {
        public int NumeroMesa { get; private set; }
        public bool EstaOcupada { get; private set; }

        public Mesa(int numeroMesa)
        {
            NumeroMesa = numeroMesa;
            EstaOcupada = false;
        }

        public void Ocupar(string cliente)
        {
            EstaOcupada = true;
            Console.WriteLine($"  [MESA] Mesa {NumeroMesa} → OCUPADA por {cliente}");
        }

        public void Reiniciar()
        {
            EstaOcupada = false;
            Console.WriteLine($"  [MESA] Mesa {NumeroMesa} → LIMPIADA y lista para reusar");
        }
    }
}
