namespace RestauranteSimulacion
{
    public abstract class FormateadorMeseroDecorator : IFormateadorMesero
    {
        protected readonly IFormateadorMesero _inner;
        protected FormateadorMeseroDecorator(IFormateadorMesero inner)
        {
            _inner = inner;
        }

        public virtual string FormatearNombre(Mesero mesero) => _inner.FormatearNombre(mesero);
        public virtual void EscribirNombre(Mesero mesero) => _inner.EscribirNombre(mesero);
    }
}
