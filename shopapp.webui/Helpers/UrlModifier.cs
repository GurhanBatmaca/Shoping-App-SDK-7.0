namespace shopapp.webui.Helpers
{
    public static class UrlModifier
    {
        public static string Modifie(string url)
        {
            string modifieUrl;

            modifieUrl = url.Trim()
                            .ToLower()
                            .Replace(" ", "-")
                            .Replace(".", "-")
                            .Replace("ş", "s")
                            .Replace("ç", "c")
                            .Replace("ü", "u")
                            .Replace("ğ", "g")
                            .Replace("ı", "i")
                            .Replace("ö", "o");

            return modifieUrl;
        }
    }
}