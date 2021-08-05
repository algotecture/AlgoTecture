using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlgoTecMvc.Models.ViewModels
{
    public class ParkingListModel
    {
        public SelectList ParkingCoordinates { get; set; }
        public string SelectedParkingCoordinates {get;set;}
        public string SelectedParkingCoordinatesText { get; set; }
    }
}