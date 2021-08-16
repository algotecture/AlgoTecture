using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlgoTecMvc.Models.ViewModels
{
    public class ParkingListModel
    {
        public List<SelectListItem> ParkingCoordinates { get; set; }
        public string SelectedSpaceProperty {get;set;}
        public string SelectedParkingCoordinatesText { get; set; }
    }
}