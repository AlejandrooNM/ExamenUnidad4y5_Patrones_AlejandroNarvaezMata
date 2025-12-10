using System;
using System.Collections.Generic;

namespace RestauranteSimulacion
{
    public class ObjectPool<T> where T : class
    {
        private readonly List<T> _disponibles;
        private readonly List<T> _enUso;
        public int CapacidadMaxima { get; private set; }
        public string NombrePool { get; private set; }

        public ObjectPool(string nombrePool)
        {
            NombrePool = nombrePool;
            _disponibles = new List<T>();
            _enUso = new List<T>();
            CapacidadMaxima = int.MaxValue;
            Console.WriteLine($"[POOL] Pool '{nombrePool}' creado");
        }

        public ObjectPool(string nombrePool, int capacidadMaxima)
        {
            NombrePool = nombrePool;
            CapacidadMaxima = capacidadMaxima > 0 ? capacidadMaxima : throw new ArgumentOutOfRangeException(nameof(capacidadMaxima), "La capacidad debe ser mayor a cero.");
            _disponibles = new List<T>();
            _enUso = new List<T>();
            Console.WriteLine($"[POOL] Pool '{nombrePool}' creado (Tope local: {CapacidadMaxima})");
        }

        public void AgregarObjeto(T objeto)
        {
            int totalLocal = _disponibles.Count + _enUso.Count;
            if (totalLocal >= CapacidadMaxima)
            {
                Console.WriteLine($"[POOL-LIMITE-LOCAL] ✗ No se puede agregar al pool '{NombrePool}': tope local {CapacidadMaxima} alcanzado (Total local: {totalLocal})");
                return;
            }
            if (!PoolCapacidadGlobal.PuedeAgregar())
            {
                Console.WriteLine($"[POOL-LIMITE-GLOBAL] ✗ No se puede agregar: capacidad global {PoolCapacidadGlobal.CapacidadMaximaGlobal} alcanzada (Total global: {PoolCapacidadGlobal.TotalObjetos})");
                return;
            }
            PoolCapacidadGlobal.RegistrarAgregado();
            _disponibles.Add(objeto);
            string topeLocalTxt = CapacidadMaxima == int.MaxValue ? "sin tope" : CapacidadMaxima.ToString();
            Console.WriteLine($"[POOL-PUSH] ✓ Objeto agregado al pool '{NombrePool}' (Disponibles: {_disponibles.Count} | En uso: {_enUso.Count} | Tope local: {topeLocalTxt} | Global: {PoolCapacidadGlobal.TotalObjetos}/{PoolCapacidadGlobal.CapacidadMaximaGlobal})");
        }

        public T Obtener()
        {
            if (_disponibles.Count > 0)
            {
                T objeto = _disponibles[0];
                _disponibles.RemoveAt(0);
                _enUso.Add(objeto);
                string topeLocalTxt = CapacidadMaxima == int.MaxValue ? "sin tope" : CapacidadMaxima.ToString();
                Console.WriteLine($"[POOL-POP] ← Objeto SACADO del pool '{NombrePool}' (Disponibles: {_disponibles.Count} | En uso: {_enUso.Count} | Tope local: {topeLocalTxt} | Global: {PoolCapacidadGlobal.TotalObjetos}/{PoolCapacidadGlobal.CapacidadMaximaGlobal})");
                return objeto;
            }
            Console.WriteLine($"[POOL-ERROR] ✗ Pool '{NombrePool}' vacío - No hay objetos disponibles");
            return null!;
        }

        public void Devolver(T objeto)
        {
            if (objeto == null) return;
            
            if (_enUso.Remove(objeto))
            {
                ReiniciarObjeto(objeto);
                _disponibles.Add(objeto);
                string topeLocalTxt = CapacidadMaxima == int.MaxValue ? "sin tope" : CapacidadMaxima.ToString();
                Console.WriteLine($"[POOL-RETURN] → Objeto DEVUELTO al pool '{NombrePool}' (Disponibles: {_disponibles.Count} | En uso: {_enUso.Count} | Tope local: {topeLocalTxt} | Global: {PoolCapacidadGlobal.TotalObjetos}/{PoolCapacidadGlobal.CapacidadMaximaGlobal})");
            }
        }

        private void ReiniciarObjeto(T objeto)
        {
            if (objeto is Mesa mesa) mesa.Reiniciar();
            else if (objeto is Mesero mesero) mesero.Reiniciar();
            else if (objeto is Pedido pedido) pedido.Reiniciar();
        }

        public void MostrarEstadisticas()
        {
            string topeLocalTxtFinal = CapacidadMaxima == int.MaxValue ? "sin tope" : CapacidadMaxima.ToString();
            Console.WriteLine($"\n[{NombrePool}] Disponibles: {_disponibles.Count} | En uso: {_enUso.Count} | Tope local: {topeLocalTxtFinal} | Global: {PoolCapacidadGlobal.TotalObjetos}/{PoolCapacidadGlobal.CapacidadMaximaGlobal}");
        }
    }
}
