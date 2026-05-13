using Microsoft.JSInterop;
using System.Text.Json;

namespace MusicRecognitionApp.Blazor.Services.Auth
{
    public class BrowserStorageService : IBrowserStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public BrowserStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
        {
            PropertyNameCaseInsensitive = true
        };

        public async ValueTask SetAsync<T>(string key, T value)
        {
            if (value is null)
            {
                await RemoveAsync(key);
                return;
            }

            var json = JsonSerializer.Serialize(value, JsonOptions);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }

        public async ValueTask<T?> GetAsync<T>(string key)
        {
            var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);

            if (string.IsNullOrWhiteSpace(json))
                return default;

            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }

        public ValueTask RemoveAsync(string key) =>
            _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);

        public ValueTask ClearAsync() =>
            _jsRuntime.InvokeVoidAsync("localStorage.clear");

        public ValueTask SetCookieAsync(string name, string value, int days = 365)
        {
            var date = DateTime.UtcNow.AddDays(days);
            var expires = $"{date:R}";
            var cookieString = $"{name}={value}; expires={expires}; path=/";

            return _jsRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{cookieString}\"");
        }
        
        public async ValueTask<string?> GetCookieAsync(string name)
        {
            var allCookies = await _jsRuntime.InvokeAsync<string>("eval", "document.cookie");
            if (string.IsNullOrEmpty(allCookies)) 
                return null;

            var cookies = allCookies.Split(';');
            foreach (var cookie in cookies)
            {
                var parts = cookie.Trim().Split('=');
                if (parts.Length == 2 && parts[0] == name)
                    return parts[1];
            }
            return null;
        }

        public ValueTask RemoveCookieAsync(string name)
        {
            return _jsRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;\"");
        }
    }
}


