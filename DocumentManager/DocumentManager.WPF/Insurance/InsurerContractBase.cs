using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.WPF.Insurance
{
    /// <summary>
    /// Базовый класс договора страховщика (страховой компании)
    /// </summary>
    public abstract class InsurerContractBase
    {
        public InsurerContractBase()
        {
            this.Period = new DatePeriod();
            this.Insurer = new InsurerPerson();
        }

        public int Id { get; set; }
        /// <summary>
        /// Номер договора
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Дата договора
        /// </summary>
        public DateTime? SignDate { get; set; }
        /// <summary>
        /// Срок действия договора
        /// </summary>
        public DatePeriod Period { get; private set; }
        /// <summary>
        /// Страховщик (страховая компания)
        /// </summary>
        public InsurerPerson Insurer { get; private set; }
    }
}
