using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;

namespace TestXamSimpleUITest.UITests
{
	[TestFixture]
	public class Tests
	{
		iOSApp app;

		[SetUp]
		public void BeforeEachTest()
		{
			app = ConfigureApp.iOS
			  .EnableLocalScreenshots()
			  .StartApp();
		}

		[Test]
		public void ClickAddButtonAddsOneItem()
		{
			app.Repl();
			//Func<AppQuery, AppQuery> addButton = c => c.Button("addButton");
			//Func<AppQuery, AppQuery> tableCells = c => c.Class("UITableViewCell");
			//Func<AppQuery, AppQuery> detailView = c => c.Marked("Detail view content goes here");
			//Func<AppQuery, AppQuery> masterButton = c => c.Button("Master");

			//AppResult[] results = app.Query(detailView);
			//if (results.Any())
			//{
			//	// Detail view is show first by iPad.
			//	app.Tap(masterButton);
			//}
			//app.Tap(addButton);
			//results = app.WaitForElement(tableCells);
			//app.Screenshot("New item added.");

			//Assert.AreEqual(1, results.Length);
		}
	}
}

