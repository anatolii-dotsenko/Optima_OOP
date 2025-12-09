namespace GameModel.Infrastructure.IO
{
    // handles downloading and caching of character images to local storage
    public class ImageCacheService
    {
        private readonly string _cacheDir;
        private readonly HttpClient _httpClient;

        public ImageCacheService()
        {
            // use a hidden folder in the current directory for cache
            _cacheDir = Path.Combine(Directory.GetCurrentDirectory(), ".cache", "images");
            _httpClient = new HttpClient();

            if (!Directory.Exists(_cacheDir))
            {
                Directory.CreateDirectory(_cacheDir);
            }
        }

        // downloads image if not exists and returns local path
        public async Task<string> GetCachedImagePathAsync(string characterName)
        {
            string fileName = $"{characterName.ToLower()}.png";
            string localPath = Path.Combine(_cacheDir, fileName);

            if (File.Exists(localPath))
            {
                return localPath;
            }

            try
            {
                // constructing url for genshin api icon
                string url = $"https://genshin.jmp.blue/characters/{characterName.ToLower()}/icon-big";
                var bytes = await _httpClient.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(localPath, bytes);
                return localPath;
            }
            catch
            {
                return "image not found";
            }
        }
    }
}