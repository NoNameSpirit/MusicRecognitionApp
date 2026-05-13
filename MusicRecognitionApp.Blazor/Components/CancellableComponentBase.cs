using Microsoft.AspNetCore.Components;

namespace MusicRecognitionApp.Blazor.Components
{
    public abstract class CancellableComponentBase : ComponentBase, IDisposable
    {
        private CancellationTokenSource? _cts;

        protected CancellationToken Ct => (_cts ??= new CancellationTokenSource()).Token;

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}
