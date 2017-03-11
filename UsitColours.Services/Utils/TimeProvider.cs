using System;

namespace UsitColours.Services.Utils
{
    public abstract class TimeProvider
    {
        private static TimeProvider current;

        static TimeProvider()
        {
            TimeProvider.current =
                new DefaultTimeProvider();
        }

        public static TimeProvider Current
        {
            get { return TimeProvider.current; }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                TimeProvider.current = value;
            }
        }

        public abstract DateTime GetDate();

        public static void ResetToDefault()
        {
            TimeProvider.current =
                new DefaultTimeProvider();
        }
    }
}
