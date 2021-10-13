﻿using System.Threading;
using System.Threading.Tasks;

namespace WebStore.Data.Mocks
{
    public interface IMockAsync
    {
        public ValueTask<bool> InitAsync(CancellationToken cancellationToken = default);
    }
}
