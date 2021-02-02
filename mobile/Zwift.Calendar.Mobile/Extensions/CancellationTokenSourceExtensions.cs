using System;
using System.Threading;

namespace Zwift.Calendar.Mobile.Extensions
{
    public static class CancellationTokenSourceExtensions
    {
        public static void CancelAndDispose(this CancellationTokenSource cancellationTokenSource)
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }
    }
}
