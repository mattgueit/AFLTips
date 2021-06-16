using System;

namespace AFLTips.Server.Providers.Interfaces
{
    public interface IDateTimeProvider
    {
        public DateTime DateTimeNow { get; }
    }
}
