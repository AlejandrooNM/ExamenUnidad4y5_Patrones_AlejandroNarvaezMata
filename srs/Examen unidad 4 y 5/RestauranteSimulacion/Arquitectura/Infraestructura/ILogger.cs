namespace RestauranteSimulacion.Infrastructure
{
    public interface ILogger
    {
        void Info(string mensaje);
        void Warn(string mensaje);
        void Error(string mensaje);
    }
}
