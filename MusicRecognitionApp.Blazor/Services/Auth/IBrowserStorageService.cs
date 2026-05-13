namespace MusicRecognitionApp.Blazor.Services.Auth
{
    public interface IBrowserStorageService
    {
        ValueTask SetAsync<T>(string key, T value);
        ValueTask<T?> GetAsync<T>(string key);
        ValueTask RemoveAsync(string key);
        ValueTask ClearAsync();

        ValueTask SetCookieAsync(string name, string value, int days = 365);
        ValueTask<string?> GetCookieAsync(string name);
        ValueTask RemoveCookieAsync(string name);

    }
}
