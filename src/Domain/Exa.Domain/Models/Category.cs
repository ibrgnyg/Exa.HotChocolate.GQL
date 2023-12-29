using Exa.Configure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa.Domain.Models
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = string.Empty;
    }
}
