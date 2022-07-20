using System;

namespace IngenuityNow.Common
{
    /// <summary>
    /// An interface used to indicate that a class supports Effective date with EffectiveDate and EndDate properties.
    /// </summary>
    public interface IEffectiveDate
    {
        /// <summary>
        /// The starting effective Date.
        /// </summary>
        DateTime EffectiveDate { get; set; }

        /// <summary>
        /// The optional end date.
        /// </summary>
        DateTime? EndDate { get; set; }
    }
}
