using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public void ThreatUser()
        {
            this.UserName = this.UserName.Trim();
            this.Password = this.Password.Trim();
        }

        public bool IsValid()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(this.UserName) ||
                string.IsNullOrEmpty(this.Password))
                isValid = false;

            return isValid;
        }

        public bool ThreatAndValidate()
        {
            this.ThreatUser();
            return this.IsValid();
        }
    }
}
