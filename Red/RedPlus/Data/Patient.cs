using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedPlus.Data
{
    public enum BloodType
    {
        A = 0,
        B = 1,
        AB = 2,
        O = 3
    }
    class Patient
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public BloodType BloodType { get; set; }

        public string RHFactor { get; set; }
        
    }
}
