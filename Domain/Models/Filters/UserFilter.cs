using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Filters
{
    public class UserFilter:User
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SortMethodEnum SortMethod { get; set; }
    }
}
