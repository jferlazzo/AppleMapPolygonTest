using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace XamarinMapsDrawPolygon.Core
{
    public class LocationSampleData
    {
        public List<GpsLocation> Locations { get; }

        public LocationSampleData()
        {
            Locations = new List<GpsLocation>();
            var sampledata = File.ReadAllLines("SampleData.csv");

            foreach (var line in sampledata)
            {
                var items             = line.Split(',');
                var assetId           = items[0];
                var pointLatitude     = items[1];
                var pointLongitude    = items[2];
                var boundarySequence  = items[3];
                var boundaryLatitude  = items[4];
                var boundaryLongitude = items[5];

                try
                {
                    var locationItem = Locations.FirstOrDefault(x => x.Title == assetId);

                    if (locationItem == null)
                    {
                        Locations.Add(new GpsLocation
                        {
                            Title        = assetId,
                            Latitude     = Convert.ToDouble(pointLatitude),
                            Longitude    = Convert.ToDouble(pointLongitude),
                            BoundaryData = new List<GpsBoundary>
                            {
                                new GpsBoundary
                                {
                                    Sequence  = Convert.ToInt32(boundarySequence),
                                    Latitude  = Convert.ToDouble(boundaryLatitude),
                                    Longitude = Convert.ToDouble(boundaryLongitude)
                                }
                            }
                        });
                    }

                    else
                    {
                        locationItem.BoundaryData.Add(new GpsBoundary
                        {
                            Sequence  = Convert.ToInt32(boundarySequence),
                            Latitude  = Convert.ToDouble(boundaryLatitude),
                            Longitude = Convert.ToDouble(boundaryLongitude)
                        });
                    }
                }

                catch (Exception)
                {
                    Debug.WriteLine(assetId);
                    Debug.WriteLine(pointLatitude);
                    Debug.WriteLine(pointLongitude);
                    Debug.WriteLine(boundarySequence);
                    Debug.WriteLine(boundaryLatitude);
                    Debug.WriteLine(boundaryLongitude);
                }
            }
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