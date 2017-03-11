using System;

namespace UsitColours.Services.Utils
{
    public class DefaultTimeProvider : TimeProvider
    {
        public override DateTime GetDate()
        {
            return DateTime.Now;
        }
    }
}
