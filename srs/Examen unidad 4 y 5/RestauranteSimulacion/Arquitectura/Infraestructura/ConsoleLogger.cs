using System;

namespace RestauranteSimulacion.Infrastructure
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string mensaje) => Console.WriteLine(mensaje);
        public void Warn(string mensaje) => Console.WriteLine(mensaje);
        public void Error(string mensaje) => Console.WriteLine(mensaje);
    }
}
