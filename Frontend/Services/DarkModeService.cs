using Microsoft.JSInterop;

namespace Frontend.Services
{
    public class DarkModeService
    {
        private readonly IJSRuntime _js;
        private bool _isDarkMode = false;
        public event Action? OnDarkModeChanged;

        public bool IsDarkMode => _isDarkMode;

        public DarkModeService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task InitializeDarkMode()
        {
            var stored = await _js.InvokeAsync<string?>("localStorage.getItem", "dark-mode");
            _isDarkMode = stored == "true";
            await ApplyDarkMode(_isDarkMode);
        }

        public async Task ToggleDarkMode()
        {
            _isDarkMode = !_isDarkMode;
            await _js.InvokeVoidAsync("localStorage.setItem", "dark-mode", _isDarkMode.ToString().ToLower());
            await ApplyDarkMode(_isDarkMode);
            OnDarkModeChanged?.Invoke();
        }

        private async Task ApplyDarkMode(bool isDark)
        {
            if (isDark)
            {
                await _js.InvokeVoidAsync("eval", "document.documentElement.classList.add('dark-mode')");
            }
            else
            {
                await _js.InvokeVoidAsync("eval", "document.documentElement.classList.remove('dark-mode')");
            }
        }
    }
}
