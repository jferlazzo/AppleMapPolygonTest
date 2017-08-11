﻿using System.Linq;
using CoreLocation;
using MapKit;
using UIKit;
using XamarinMapsDrawPolygon.Portable;

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
}