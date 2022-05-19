using DisasterTracker.BL.Dtos;

namespace DisasterTracker.BL.Extensions
{
    public static class DisasterExtensions
    {
        public static bool ShouldNotifyUserAboutDisaster(this DisasterDto disaster, UserLocationDto userLocation)
        {
            var distance = CalculateDistanceBetweenADisasterAndUserLocation(disaster, userLocation);
            return distance <= userLocation.Distance;
        }

        private static double CalculateDistanceBetweenADisasterAndUserLocation(DisasterDto disaster, UserLocationDto userLocation)
        {
            var latitude1 = ToRadians(disaster.Latitude);
            var longitude1 = ToRadians(disaster.Longitude);

            var latitude2 = ToRadians(userLocation.Latitude);
            var longitude2 = ToRadians(userLocation.Longitude);

            // Haversine formula
            double dlon = longitude2 - longitude1;
            double dlat = latitude2 - latitude1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(latitude1) * Math.Cos(latitude2) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            double r = 6371;

            return (c * r);
        }


        private static double ToRadians(decimal angleIn10thOfADegree)
        {
            return ((double)angleIn10thOfADegree * Math.PI) / 180;
        }
    }
}
