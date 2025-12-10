namespace RestauranteSimulacion
{
    public class FormateadorMeseroBase : IFormateadorMesero
    {
        public string FormatearNombre(Mesero mesero) => mesero.Nombre;
        public void EscribirNombre(Mesero mesero)
        {
            System.Console.Write(FormatearNombre(mesero));
        }
    }
}
