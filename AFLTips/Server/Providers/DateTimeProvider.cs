using System;
using AFLTips.Server.Providers.Interfaces;

namespace AFLTips.Server.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime DateTimeNow => DateTime.Now;
    }
}
