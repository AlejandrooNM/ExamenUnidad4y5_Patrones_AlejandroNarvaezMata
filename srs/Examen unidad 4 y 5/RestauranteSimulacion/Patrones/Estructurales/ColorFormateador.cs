using System;

namespace RestauranteSimulacion
{
    public class ColorFormateador : FormateadorMeseroDecorator
    {
        private readonly ConsoleColor _color;
        public ColorFormateador(ConsoleColor color, IFormateadorMesero inner) : base(inner)
        {
            _color = color;
        }

        public override string FormatearNombre(Mesero mesero)
        {
            return base.FormatearNombre(mesero);
        }

        public override void EscribirNombre(Mesero mesero)
        {
            var previo = Console.ForegroundColor;
            Console.ForegroundColor = _color;
            Console.Write(FormatearNombre(mesero));
            Console.ForegroundColor = previo;
        }
    }
}
