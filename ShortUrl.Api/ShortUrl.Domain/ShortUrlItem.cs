namespace ShortUrl.Domain
{
    public class ShortUrlItem
    {
        public const int ShortcutMaxLength = 10;
        public const int UrlMaxLength = 2000;

        public ShortUrlItem(string shortcut, string url)
        {
            Shortcut = shortcut;
            Url = url;
        }

        public string Shortcut { get; }
        public string Url { get; }
    }
}