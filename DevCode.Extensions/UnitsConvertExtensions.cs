namespace DevCode.Extensions
{
    public static class UnitsConvertExtensions
    {
        /// <summary>
        /// Simplest way to get a number of Kilobytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int KB(this int value)
        {
            return value * 1024;
        }

        /// <summary>
        /// Simplest way to get a number of Megabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int MB(this int value)
        {
            return value.KB() * 1024;
        }

        /// <summary>
        /// Simplest way to get a number of Gigabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GB(this int value)
        {
            return value.MB() * 1024;
        }

        /// <summary>
        /// Simplest way to get a number of Terabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long TB(this int value)
        {
            return (long)value.GB() * (long)1024;
        }
    }
}