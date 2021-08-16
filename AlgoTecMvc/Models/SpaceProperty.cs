using System;
using System.Collections.Generic;

namespace AlgoTecMvc.Models
{
    public class SpaceProperty
    {
        public Guid SpacePropertyId { get; set; }

        public long SpaceId { get; set; }
        public int TypeOfSpace { get; set; }
        
        public double TotalArea { get; set; }
        
        public string Coordinates { get; set; }

        public long OwnerId { get; set; }

        public long ContractId { get; set; }

        public List<SubSpace> SubSpaces { get; set; }
    }
}   