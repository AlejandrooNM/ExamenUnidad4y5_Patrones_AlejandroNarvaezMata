using System;

namespace RestauranteSimulacion
{
    public sealed class GerenteRestaurante
    {
        private static GerenteRestaurante _instancia = null!;
        private static readonly object _lock = new object();
        
        public int ClientesAtendidos { get; private set; }

        private GerenteRestaurante()
        {
            ClientesAtendidos = 0;
            Console.WriteLine("\n[SINGLETON]  INSTANCIA ÚNICA CREADA - Gerente del restaurante");
        }

        public static GerenteRestaurante ObtenerInstancia()
        {
            if (_instancia == null)
            {
                lock (_lock)
                {
                    if (_instancia == null)
                    {
                        _instancia = new GerenteRestaurante();
                    }
                }
            }
            else
            {
                Console.WriteLine("  [SINGLETON] → Reutilizando instancia única existente");
            }
            return _instancia;
        }

        public void AbrirRestaurante()
        {
            Console.WriteLine("\n[RESTAURANTE] El gerente ha abierto el restaurante\n");
        }

        public void CerrarRestaurante()
        {
            Console.WriteLine($"\n[RESTAURANTE] El gerente ha cerrado el restaurante - Clientes atendidos: {ClientesAtendidos}\n");
        }

        public void RecibirCliente(string nombreCliente)
        {
            Console.WriteLine($"  [SINGLETON-HOST] Gerente recibe a {nombreCliente} en la entrada");
        }

        public void AsignarMesa(string nombreCliente, int numeroMesa)
        {
            Console.WriteLine($"  [SINGLETON-HOST] Gerente asigna Mesa {numeroMesa} a {nombreCliente}");
        }

        public void RegistrarClienteAtendido()
        {
            ClientesAtendidos++;
            Console.WriteLine($"  [SINGLETON-HOST] Gerente registra cliente #{ClientesAtendidos} atendido");
        }

        public void DespedirCliente(string nombreCliente)
        {
            Console.WriteLine($"  [SINGLETON-HOST] Gerente despide a {nombreCliente}");
        }
    }
}
