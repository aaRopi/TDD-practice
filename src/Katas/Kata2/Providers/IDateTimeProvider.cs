using System;

namespace Katas.Kata2
{
    public interface IDateTimeProvider
    {
        DateTime DateTimeNow { get; }
    }
}