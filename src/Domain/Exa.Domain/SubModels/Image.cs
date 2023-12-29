namespace Exa.Domain.SubModels
{
    public class Image
    {
        public string Url { get; set; } = string.Empty;
        public string FileType => GetFileExtension(Url);

        private string GetFileExtension(string url)
        {
            int lastDotIndex = url.LastIndexOf('.');
            if (lastDotIndex != -1)
            {
                return url.Substring(lastDotIndex + 1);
            }

            return string.Empty;
        }
    }
}
