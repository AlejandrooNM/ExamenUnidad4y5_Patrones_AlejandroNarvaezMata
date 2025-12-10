using System;

namespace RestauranteSimulacion
{
    public enum ClienteEstado
    {
        Esperando,
        Atendido,
        Finalizado
    }

    public class Cliente
    {
        public string Nombre { get; }
        public ClienteEstado Estado { get; private set; }

        public Cliente(string nombre)
        {
            Nombre = nombre;
            Estado = ClienteEstado.Esperando;
        }

        public void CambiarEstado(ClienteEstado nuevo)
        {
            Estado = nuevo;
            Console.WriteLine($"  [CLIENTE-ESTADO] {Nombre} → {Estado}");
        }
    }
}
