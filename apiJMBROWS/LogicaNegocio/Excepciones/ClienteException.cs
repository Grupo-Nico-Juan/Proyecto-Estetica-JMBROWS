namespace LogicaNegocio.Excepciones
{
    public class ClienteException : ExcepcionReglaDeNegocio
    {
        public ClienteException() { }

        public ClienteException(string mensaje) : base(mensaje) { }

        public ClienteException(string mensaje, Exception inner) : base(mensaje, inner) { }
    }
}