using System;

namespace AutomatedWorker.Tools
{
    public class NumberUtils
    {
        public static int GetUniqueNumber()
        {
            TimeSpan span = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return Convert.ToInt32(span.TotalSeconds);
        }
    }
}
