
namespace LogicaAccesoDatos.Repositorios
{
    [Serializable]
    internal class HabilidadException : Exception
    {
        public HabilidadException()
        {
        }

        public HabilidadException(string? message) : base(message)
        {
        }

        public HabilidadException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}