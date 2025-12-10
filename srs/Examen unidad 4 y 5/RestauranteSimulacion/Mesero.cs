using System;

namespace RestauranteSimulacion
{
    public class Mesero
    {
        public int Id { get; }
        public string Nombre { get; private set; }
        private IFormateadorMesero _formateador = new FormateadorMeseroBase();

        public Mesero(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public void AsignarCliente(string cliente)
        {
            System.Console.Write("  [MESERO] ");
            EscribirNombre();
            System.Console.WriteLine($" → ATENDIENDO a {cliente}");
        }

        public void Reiniciar()
        {
            System.Console.Write("  [MESERO] ");
            EscribirNombre();
            System.Console.WriteLine(" → DISPONIBLE y listo para reusar");
        }

        public void ConfigurarFormateador(IFormateadorMesero formateador)
        {
            _formateador = formateador ?? new FormateadorMeseroBase();
        }

        internal void EscribirNombre()
        {
            _formateador.EscribirNombre(this);
        }
    }
}
