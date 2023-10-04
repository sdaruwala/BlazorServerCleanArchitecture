using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Position : BaseAuditableEntity
    {
        public Position()
        {
            Players = new HashSet<Player>();
        }

        public string? Name { get; set; }
        public int? DisplayOrder { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
