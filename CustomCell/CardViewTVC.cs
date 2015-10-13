using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace CustomCell
{
	partial class CardViewTVC : UITableViewController
	{
		public const string CELL_ID = "cell_id";

		public CardViewTVC (IntPtr handle) : base (handle)
		{
	
		}

		public override void ViewDidLoad ()
		{
			TableView.RegisterClassForCellReuse(typeof(CustomCell), CELL_ID);

			base.ViewDidLoad ();
			var titles = new string[] { "one", "two", "three", "four" };

			this.TableView.Source = new MyTVS (titles);
		}
	}

	public class MyTVS : UITableViewSource 
	{
		string[] data;

		public MyTVS (string[] data)
		{
			this.data = data;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return data.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = (CustomCell)tableView.DequeueReusableCell (CardViewTVC.CELL_ID);

			cell.SetTitle (data [indexPath.Row]);

			return cell;
		}

	}
}
