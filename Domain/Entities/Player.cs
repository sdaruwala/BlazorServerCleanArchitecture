using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Player : BaseAuditableEntity
    {       
        public int? ShirtNo { get; set; }
        public string? Name { get; set; }
        public int? PositionId { get; set; }
        public int? Appearances { get; set; }
        public int? Goals { get; set; }
        public virtual Position? Position { get; set; }
    }
}
