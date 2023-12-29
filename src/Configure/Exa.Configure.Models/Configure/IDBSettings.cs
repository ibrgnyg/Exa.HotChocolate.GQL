using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa.Configure.Models.Configure
{
    public interface IDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } 
    }
}
