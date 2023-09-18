namespace LinkShortenerAPI.Services
{
    /// <summary>
    /// Interface in order to use ShortURL by delight.im as a service with DI.
    /// </summary>
    public interface IShortUrl
    {
        public string Encode(long num);
        public int Decode(string num);
    }
}
