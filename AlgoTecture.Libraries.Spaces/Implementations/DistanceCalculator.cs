using AlgoTecture.Libraries.Spaces.Interfaces;
using AlgoTecture.Libraries.Spaces.Models;

namespace AlgoTecture.Libraries.Spaces.Implementations
{
    public class DistanceCalculator : IDistanceCalculator
    {
        public double GetDistanceInKilometers(double latitudePointA, double longitudePointA, double latitudePointB,
            double longitudePointB)
        {
            var coordinateA = new Coordinates(latitudePointA, longitudePointA);
            var coordinateB = new Coordinates(latitudePointB, longitudePointB);
            var distance = DistanceTo(coordinateA, coordinateB,
                    UnitOfLength.Kilometers);
            return distance;

        }
      
        private static double DistanceTo(Coordinates baseCoordinates, Coordinates targetCoordinates, UnitOfLength unitOfLength)
        {
            var baseRad = Math.PI * baseCoordinates.Latitude / 180;
            var targetRad = Math.PI * targetCoordinates.Latitude/ 180;
            var theta = baseCoordinates.Longitude - targetCoordinates.Longitude;
            var thetaRad = Math.PI * theta / 180;

            var dist =
                Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
                Math.Cos(targetRad) * Math.Cos(thetaRad);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return unitOfLength.ConvertFromMiles(dist);
        }

    }
}