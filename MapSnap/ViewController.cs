using System;

using UIKit;
using CoreLocation;
using MapKit;
using Foundation;
using System.Threading.Tasks;
using MessageUI;
using System.Drawing;
using System.IO;
using CoreAnimation;

namespace MapSnap
{
	public partial class ViewController : UIViewController
	{
		CLLocationManager locMan = new CLLocationManager();

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			locMan.RequestWhenInUseAuthorization ();

			map.Camera.CenterCoordinate = new CLLocationCoordinate2D (49.26741, -123.161861);
			map.Camera.Altitude = 750;
			map.ShowsScale = true;
			map.ShowsTraffic = true;
			map.ShowsCompass = true;
		
			map.MapType = MKMapType.Standard;
		}

		MKMapSnapshotOptions GetSnapshotOptions ()
		{
			MKMapSnapshotOptions options = new MKMapSnapshotOptions();
			options.ShowsBuildings = true;
			options.ShowsPointsOfInterest = true;
			options.MapType = MKMapType.Hybrid;
			options.Camera = map.Camera;
			options.Size = map.Frame.Size;//defaults to 256x256

			return options;
		}

		async partial void BtnCapture_TouchUpInside (UIButton sender)
		{
			imgCapture.Image = (await GetSnapshotAsync(GetSnapshotOptions())).Image;
		}

		Task<MKMapSnapshot> GetSnapshotAsync (MKMapSnapshotOptions options)
		{
			MKMapSnapshotter shotter = new MKMapSnapshotter(options);

			var tcs = new TaskCompletionSource<MKMapSnapshot>();

			shotter.Start ((snapshot, error) => {
				tcs.SetResult(snapshot);
			});

			return tcs.Task;
		}

		MFMailComposeViewController mailVC = new MFMailComposeViewController();
		partial void BtnEmail_TouchUpInside (UIButton sender)
		{
			var data = imgCapture.Image.AsJPEG();

			if (MFMailComposeViewController.CanSendMail == false)
				return;

			mailVC = new MFMailComposeViewController();

			mailVC.AddAttachmentData(data, "image/jpeg", "map.jpg");
			mailVC.SetSubject("I am here");
			mailVC.SetMessageBody ("my location", false);

			this.PresentViewController(mailVC, true, null);

			mailVC.Finished += (s, e) => {
				e.Controller.DismissViewController(true, null);
				mailVC.Dispose();
				mailVC = null;
			};
		}
	}
}

