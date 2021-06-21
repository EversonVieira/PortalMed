using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public virtual ICollection<Product> Products { get; set; }
      
        public bool IsValid()
        {
            bool isValid = true;

            if (UserId is <= 0)
                isValid = false;

            return isValid;
        }
    }
}
