﻿using Microsoft.Extensions.Logging;
using System;

namespace Zwift.Calendar.Mobile.Logging
{
    /// <summary>
    /// Scope provider that does nothing
    /// </summary>
    internal class NullExternalScopeProvider : IExternalScopeProvider
    {

        private NullExternalScopeProvider() { }


        /// <summary>
        /// Returns a cached instance of <see cref="NullExternalScopeProvider"/>.
        /// </summary>
        public static IExternalScopeProvider Instance { get; } = new NullExternalScopeProvider();


        /// <inheritdoc />
        void IExternalScopeProvider.ForEachScope<TState>(Action<object, TState> callback, TState state) { }

        /// <inheritdoc />
        IDisposable IExternalScopeProvider.Push(object state)
        {
            return NullScope.Instance;
        }
    }
}
