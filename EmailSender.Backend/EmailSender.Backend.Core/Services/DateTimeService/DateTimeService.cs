namespace EmailSender.Backend.Core.Services.DateTimeService
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class DateTimeService : IDateTimeService
    {
        /// <summary>
        /// Returns current DateTime in UTC.
        /// </summary>
        public DateTime Now => DateTime.UtcNow;

        /// <summary>
        /// Returns current DateTime relative to UTC (as DateTimeOffset).
        /// </summary>
        public DateTimeOffset RelativeNow => DateTime.UtcNow;

        /// <summary>
        /// Returns today's date and set time at midnight (00:00:00).
        /// </summary>
        public DateTime TodayStartOfDay => DateTime.Today;
        
        /// <summary>
        /// Returns today's date and set time at second before midnight (23:59:59). 
        /// </summary>
        public DateTime TodayEndOfDay => DateTime.Today.AddDays(1).AddTicks(-1);
        
        /// <summary>
        /// Returns date component (time set to 00:00:00) from given DateTime. 
        /// </summary>
        /// <param name="value">Date and Time.</param>
        /// <returns>Extracted date component from supplied value.</returns>
        public DateTime GetStartOfDay(DateTime value) => value.Date;
        
        /// <summary>
        /// Returns date component (time set to 23:59:59) from given DateTime. 
        /// </summary>
        /// <param name="value">Date and Time.</param>
        /// <returns>Extracted date component from supplied value.</returns>
        public DateTime GetEndOfDay(DateTime value) => value.Date.AddDays(1).AddTicks(-1);
        
        /// <summary>
        /// Returns first day of the month from given Date and Time.
        /// </summary>
        /// <param name="value">Date and Time.</param>
        /// <returns>Date and time being first day of the month.</returns>
        public DateTime GetFirstDayOfMonth(DateTime value) => new (value.Year, value.Month, 1);
    }
}