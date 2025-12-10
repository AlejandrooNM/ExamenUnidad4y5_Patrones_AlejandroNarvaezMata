using System;

namespace RestauranteSimulacion
{
    public static class EstiloConsola
    {
        private const int Ancho = 60;

        public static void Linea(char ch = '=')
        {
            Console.WriteLine(new string(ch, Ancho));
        }

        public static void Titulo(string texto)
        {
            Linea('=');
            Console.WriteLine(Centrar(texto, Ancho));
            Linea('=');
        }

        public static void Seccion(string texto)
        {
            Console.WriteLine();
            Console.WriteLine(Centrar($"{texto}", Ancho));
            Linea('-');
        }

        public static void Etiqueta(string etiqueta, string valor)
        {
            Console.WriteLine($"{etiqueta}: {valor}");
        }

        public static void ConColor(ConsoleColor color, Action accion)
        {
            var previo = Console.ForegroundColor;
            Console.ForegroundColor = color;
            try { accion(); }
            finally { Console.ForegroundColor = previo; }
        }

        private static string Centrar(string texto, int ancho)
        {
            if (string.IsNullOrEmpty(texto)) return string.Empty;
            if (texto.Length >= ancho) return texto;
            int padding = (ancho - texto.Length) / 2;
            return new string(' ', padding) + texto;
        }
    }
}
