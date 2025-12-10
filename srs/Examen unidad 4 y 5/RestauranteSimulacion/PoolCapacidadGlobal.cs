using System;

namespace RestauranteSimulacion
{
    public static class PoolCapacidadGlobal
    {
        public static int CapacidadMaximaGlobal { get; set; } = 10;
        public static int TotalObjetos { get; private set; } = 0;

        public static bool PuedeAgregar()
        {
            return TotalObjetos < CapacidadMaximaGlobal;
        }

        public static void RegistrarAgregado()
        {
            if (TotalObjetos >= CapacidadMaximaGlobal)
            {
                throw new InvalidOperationException($"Capacidad global {CapacidadMaximaGlobal} alcanzada");
            }
            TotalObjetos++;
        }

        public static void RegistrarEliminado()
        {
            if (TotalObjetos > 0) TotalObjetos--;
        }

        public static void ReiniciarConteo()
        {
            TotalObjetos = 0;
        }
    }
}
