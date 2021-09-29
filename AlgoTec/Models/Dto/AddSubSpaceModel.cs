using System;

namespace AlgoTec.Models.Dto
{
    public class AddSubSpaceModel
    {
        public long SpaceId { get; set; }

        public Guid SubSpaceIdToUpdate { get; set; }

        public SubSpace SubSpace { get; set; }
    }
}