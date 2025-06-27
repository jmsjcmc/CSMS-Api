namespace CSMapi.Helpers
{
    public static class TimeHelper
    {
        // Method to get the current time in Philippine Standard Time (PST)
        public static DateTime GetPhilippineStandardTime()
        {
            // Get time zone info object for the Philippine Standard Time
            var phpTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
            // Get current UTC time
            DateTime utcNow = DateTime.UtcNow;
            // Convert UTC time to Philippine Standard Time
            DateTime phpTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, phpTimeZone);
            // Return converted time 
            return phpTime;
        }
    }
}
