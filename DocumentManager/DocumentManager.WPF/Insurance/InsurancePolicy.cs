using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentManager.WPF.Insurance
{
    /// <summary>
    /// Страховой полис
    /// </summary>
    public class InsurancePolicy
    {
        public InsurancePolicy()
        {
            this.Period = new DatePeriod();
            this.Person = new Person();
            this.Contract = new InsuranceContract();
            this.Program = new InsuranceProgram();
        }

        public int Id { get; set; }
        /// <summary>
        /// Номер полиса
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Период страхования
        /// </summary>
        public DatePeriod Period { get; private set; }
        /// <summary>
        /// Застрахованный
        /// </summary>
        public Person Person { get; private set; }
        /// <summary>
        /// Договор страхования
        /// </summary>
        public InsuranceContract Contract { get; private set; }
        /// <summary>
        /// Программа страхования
        /// </summary>
        public InsuranceProgram Program { get; private set; }
    }
}
