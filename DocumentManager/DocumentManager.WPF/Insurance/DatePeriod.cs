using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.WPF.Insurance
{
    /// <summary>
    /// Временной период
    /// </summary>
    public class DatePeriod
    {
        /// <summary>
        /// Дата открытия периода
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// Дата закрытия период
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
