using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.WPF.Insurance
{
    /// <summary>
    /// Договор страхования
    /// </summary>
    public class InsuranceContract
    {
        public InsuranceContract()
        {
            this.Insurant = new Person();
        }

        /// <summary>
        /// Страхователь (клиент)
        /// </summary>
        public Person Insurant { get; private set; }
    }
}
