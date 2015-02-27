using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentManager.WPF.Insurance
{
    /// <summary>
    /// Программа страхования
    /// </summary>
    public class InsuranceProgram
    {
        public int Id { get; set; }
        /// <summary>
        /// Код программы
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Наименование программы
        /// </summary>
        public string Name { get; set; }
    }
}
