using System;

namespace IngenuityNow.Common
{
    /// <summary>
    /// ICalendar should be used in place of DateTime to assist with unit testing.
    /// </summary>
    /// <remarks>
    /// Implementation details... Calendar : ICalendar.
    /// </remarks>
    public interface ICalendar
    {
        /// <summary>
        /// This property can be used just like DateTime.Now but can also be mocked out during testing
        /// </summary>
        /// <value>A dateTime representing "now"</value>
        DateTime Now { get; }
    }

    /// <summary>
    /// Calendar Class is a wrapper for DateTime to assist with unit testing.
    /// </summary>
    public class Calendar : ICalendar
    {
        /// <summary>
        /// The property can be used just like DateTime.Now (that is what it returns).
        /// </summary>
        /// <value>DateTime.Now</value>
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
