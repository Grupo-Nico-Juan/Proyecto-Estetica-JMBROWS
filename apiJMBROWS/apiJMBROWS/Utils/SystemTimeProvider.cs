namespace apiJMBROWS.Utils
{
    /// <summary>
    /// Implementación de <see cref="ITimeProvider"/> que utiliza
    /// <see cref="DateTimeOffset.UtcNow"/>.
    /// </summary>
    public class SystemTimeProvider : ITimeProvider
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
