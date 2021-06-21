using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }

        public void ThreatUser()
        {
            this.DisplayName = this.DisplayName.Trim();
            this.UserName = this.UserName.Trim();
            this.Password = this.Password.Trim();
            this.Email = this.Email.Trim();
        }

        public bool IsValid()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(this.DisplayName) || 
                string.IsNullOrEmpty(this.UserName) || 
                string.IsNullOrEmpty(this.Password) || 
                string.IsNullOrEmpty(this.Email))
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
