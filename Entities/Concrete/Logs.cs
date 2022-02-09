using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
   public class Logs:IEntity
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public string Audit { get; set; }
        public DateTime Date { get; set; }
    }
}
