﻿using Domain.Common;

namespace Domain.Entities
{
    public class Stadium : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string City { get; set; }
        public int? Capacity { get; set; }
        public int? BuiltYear { get; set; }
        public int? PitchLength { get; set; }
        public int? PitchWidth { get; set; }

    }
}
