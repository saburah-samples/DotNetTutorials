using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentManager.WPF.Insurance
{
    /// <summary>
    /// Страховщик (страховая компания)
    /// </summary>
    public class InsurerPerson : Person
    {
        public InsurerPerson()
        {
            Name = "Страховая компания";
            Address.Name = "Адрес центрального офиса";

            this.InsuranceContracts = new List<InsuranceContract>();
            this.ServiceContracts = new List<ServiceContract>();
        }

        public ICollection<InsuranceContract> InsuranceContracts { get; private set; }

        public ICollection<ServiceContract> ServiceContracts { get; private set; }
    }
}
