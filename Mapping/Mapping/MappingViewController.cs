using System;
using System.Drawing;

using Foundation;
using UIKit;
using MapKit;
using CoreLocation;
using System.Diagnostics;

namespace Mapping
{
	public partial class MappingViewController : UIViewController
	{
		private CLLocationCoordinate2D locVancouver = new CLLocationCoordinate2D(49.2827, -123.1207);

		private CLLocationCoordinate2D locBoston = new CLLocationCoordinate2D (42.374172, -71.120639);

		public MappingViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			map.Delegate = new MyMapDelegate ();

			AddPins ();
			
			btnPointToPoint.TouchUpInside += (sender, e) => {
				CalcAndDrawPointToPoint ();
			};

			btnRoute.TouchUpInside += (sender, e) => {
				CalcAndDrawRoute ();
			};
		}

		void AddPins ()
		{
			var pointAnnotation = new MKPointAnnotation ();
			pointAnnotation.SetCoordinate (locVancouver);
			map.AddAnnotation (pointAnnotation);

			pointAnnotation = new MKPointAnnotation ();
			pointAnnotation.SetCoordinate (locBoston);
			map.AddAnnotation (pointAnnotation);
		}

		void CalcAndDrawPointToPoint ()
		{
			var polyline = MKPolyline.FromCoordinates(new CLLocationCoordinate2D[]
			{
				locVancouver,
				locBoston,
			});

			map.AddOverlay (polyline);
		}

		void CalcAndDrawRoute ()
		{
			MKPlacemarkAddress nullmark = null;

			var orignPlaceMark = new MKPlacemark (locVancouver, nullmark);
			var sourceItem = new MKMapItem(orignPlaceMark);

			var destPlaceMark = new MKPlacemark(locBoston, nullmark);
			var destItem = new MKMapItem(destPlaceMark);

			MKDirectionsRequest request = new MKDirectionsRequest ();
			request.Source = sourceItem;
			request.Destination = destItem;
			request.RequestsAlternateRoutes = true;


			var directions = new MKDirections (request);

			directions.CalculateDirections ((response, error) => 
			{
				if(error != null)
				{
					Console.WriteLine(error.LocalizedDescription);
				}
				else
				{
					foreach (var route in response.Routes)
					{
						map.AddOverlay(route.Polyline);

						Console.WriteLine(route.Name + " " + route.Distance/1000 + "km " + route.ExpectedTravelTime/3600 + "hr");
					}
				}
			});
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}

	class MyMapDelegate : MKMapViewDelegate
	{
		public override MKOverlayRenderer OverlayRenderer(MKMapView mapView, IMKOverlay overlay)
		{
			if (overlay is MKPolyline) 
			{
				var polylineRenderer = new MKPolylineRenderer(overlay as MKPolyline);

				int rand = new Random().Next(5);

				UIColor lineColor = UIColor.White;

				switch (rand) 
				{
				case 0:
					lineColor = UIColor.Blue;
					break;
				case 1:
					lineColor = UIColor.Green;
					break;
				case 2:
					lineColor = UIColor.Red;
					break;
				case 3:
					lineColor = UIColor.Cyan;
					break;
				case 4:
					lineColor = UIColor.Orange;
					break;
				case 5:
					lineColor = UIColor.Yellow;
					break;
				}

				polylineRenderer.StrokeColor = lineColor;

				return polylineRenderer;
			} 
			else 
			{
				Debug.WriteLine("OverlayRenderer() - Unknown overlay type!");
				return null;
			}

		}            
	}
}

