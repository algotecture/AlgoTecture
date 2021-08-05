using System.Collections.Generic;

namespace AlgoTecMvc.Models
{
    public class SubSpace
    {
        public SubSpace()
        {
            _subSpaces = new List<SubSpace>();
        }

        private List<SubSpace> _subSpaces;
        
        public double Area { get; set; }

        public long OwnerId { get; set; }

        public List<SubSpace> Subspaces
        {
            get { return _subSpaces; }
        }
    }
}