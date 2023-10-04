using Application.Common.Mappings;
using AutoMapper;
using Domain.Common;
using Domain.Entities;


namespace Application.Common.Entities
{
    public class PlayerDto : BaseAuditableEntity, IMapFrom<Player>
    {       
        public int? ShirtNo { get; set; }
        public string? Name { get; set; }
        public int? PositionId { get; set; }
        public int? Appearances { get; set; }
        public int? Goals { get; set; }
        public virtual Position? Position { get; set; }
    }
}
