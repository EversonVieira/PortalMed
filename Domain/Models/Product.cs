using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public ICollection<Order> Orders { get; set; }

        public void ThreatProduct()
        {
            this.Name = this.Name.Trim();
            this.Description = this.Description.Trim();
        }
        public bool IsValid()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(this.Name) ||
                string.IsNullOrEmpty(this.Description))
                isValid = false;

            return isValid;
        }

        public bool ThreatAndValidate()
        {
            this.ThreatProduct();
            return this.IsValid();
        }
    }
}
