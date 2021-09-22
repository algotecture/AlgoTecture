using System;
using System.Collections.Generic;

namespace AlgoTecMvc.Models
{
    public class SubSpace
    {
        public SubSpace()
        {
            _subSpaces = new List<SubSpace>();
        }

        public Guid SubSpaceId { get; set; }

        public long SpaceId { get; set; }

        private List<SubSpace> _subSpaces;
        
        public double Area { get; set; }

        public int TypeOfSpaceId { get; set; }

        public long OwnerId { get; set; }

        public List<SubSpace> Subspaces
        {
            get { return _subSpaces; }
            set { _subSpaces = value; }
        }
    }
}