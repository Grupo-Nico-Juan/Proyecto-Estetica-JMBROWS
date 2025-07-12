namespace apiJMBROWS.Utils
{
    /// <summary>
    /// Provee la hora actual en UTC para centralizar el manejo de tiempo.
    /// </summary>
    public interface ITimeProvider
    {
        /// <summary>
        /// Obtiene la fecha y hora actuales en UTC.
        /// </summary>
        DateTimeOffset UtcNow { get; }
    }
}
