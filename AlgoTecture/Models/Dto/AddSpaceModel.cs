using System.Collections.Generic;

namespace AlgoTecture.Models.Dto
{
    public class AddSpaceModel
    {
        public string UserEmail { get; set; }

        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string DateStart { get; set; }

        public string DateStop { get; set; }
    }
}