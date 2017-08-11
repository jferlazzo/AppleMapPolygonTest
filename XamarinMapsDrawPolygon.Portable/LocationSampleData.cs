using System.Collections.Generic;

namespace XamarinMapsDrawPolygon.Portable
{
    public class LocationSampleData
    {
        public List<GpsLocation> Locations { get; }

        public LocationSampleData()
        {
            Locations = new List<GpsLocation>
            {
                new GpsLocation
                {
                    Title        = "Civica Pty Ltd",
                    Latitude     = -37.816477823733315,
                    Longitude    = 144.95702102780342,
                    BoundaryData = new List<GpsBoundary>
                    {
                        new GpsBoundary {Sequence = 0, Latitude = -37.81609271483047,  Longitude = 144.95705422013998},
                        new GpsBoundary {Sequence = 1, Latitude = -37.8161976002603,   Longitude = 144.9566887691617},
                        new GpsBoundary {Sequence = 2, Latitude = -37.816781353818726, Longitude = 144.95701784268022},
                        new GpsBoundary {Sequence = 3, Latitude = -37.81670110122117,  Longitude = 144.95729142799973}
                    }
                }
            };
        }
    }

    public class GpsLocation
    {
        public string Title { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public List<GpsBoundary> BoundaryData { get; set; }
    }

    public class GpsBoundary
    {
        public int Sequence { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}