using Application.Common.Mappings;
using Domain.Entities;


namespace Application.Common.Entities
{
    public class PositionDto : IMapFrom<Position>
    {
        public PositionDto()
        {
            Players = new HashSet<Player>();
        }

        public int Id { get; init; }
        public string? Name { get; set; }
        public int? DisplayOrder { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
