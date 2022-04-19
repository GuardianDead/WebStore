using System;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
    public class TrackNumberService : ITrackNumberService
    {
        public string GenerateTrackNumber() => Guid.NewGuid().ToString();
    }
}
