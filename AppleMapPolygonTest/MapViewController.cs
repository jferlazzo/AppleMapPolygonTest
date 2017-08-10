using System.Collections.Generic;
using System.Linq;
using CoreLocation;
using MapKit;
using UIKit;

namespace AppleMapPolygonTest
{
    public class MapViewController : UIViewController
    {
        private MKMapView Map { get; set; }

        private LocationSampleData SampleData { get; }

        public MapViewController()
        {
            SampleData = new LocationSampleData();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Map = new MKMapView(View.Bounds)
            {
                RotateEnabled = true,
                ScrollEnabled = true,
                ZoomEnabled   = true,
                Delegate      = new MapDelegate(),
                MapType       = MKMapType.Standard
            };

            View.AddSubview(Map);

            foreach (var location in SampleData.Locations)
            {
                Map.AddOverlay(MKPolygon.FromCoordinates(location.BoundaryData
                    .Select(boundaryLocation => new CLLocationCoordinate2D(boundaryLocation.Latitude, boundaryLocation.Longitude))
                    .ToArray()));

                Map.AddAnnotation(new MKPointAnnotation
                {
                    Title = location.Title,
                    Coordinate = new CLLocationCoordinate2D(location.Latitude, location.Longitude)
                });
            }

            Map.ShowAnnotations(Map.Annotations, true);
        }
    }

    public class MapDelegate : MKMapViewDelegate
    {
        private const string MapAnnotationId = "Annotation";

        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            return mapView.DequeueReusableAnnotation(MapAnnotationId) ?? new MKPinAnnotationView(annotation, MapAnnotationId)
            {
                CanShowCallout = true
            };
        }
        
        public override MKOverlayView GetViewForOverlay(MKMapView mapView, IMKOverlay overlay)
        {
            var polygon = overlay as MKPolygon;
            var polygonView = new MKPolygonView(polygon)
            {
                FillColor   = UIColor.LightGray,
                StrokeColor = UIColor.DarkGray,
                LineWidth   = 3f
            };

            return polygonView;
        }
    }

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