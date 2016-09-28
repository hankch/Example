using System;

using Foundation;
using UIKit;
using NotificationCenter;
using WebKit;

namespace ExtensionsDemo {
	public partial class ExtensionsDemoViewController : UIViewController ,IWKScriptMessageHandler{
		WKWebView webView;

		public ExtensionsDemoViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			var controller = NCWidgetController.GetWidgetController ();
			controller.SetHasContent (true, "com.xamarin.ExtensionsDemo.EvolveCountdownWidget");
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			webView = new WKWebView (View.Frame, new WKWebViewConfiguration ());
			View.AddSubview (webView);


			var userController = new WKUserContentController();

			// add JS function to the document so we can call it from C#
			userController.AddUserScript(
				new WKUserScript(
					new NSString("function sendData(){window.webkit.messageHandlers.native.postMessage(JSON.stringify(window.location));};"),
					WKUserScriptInjectionTime.AtDocumentEnd, false));

			// register messageHandler 'native' that can be called with window.webkit.messageHandlers.native
			userController.AddScriptMessageHandler(this, "native");

			var config = new WKWebViewConfiguration
			{
				UserContentController = userController
			};

			this.webView = new WKWebView(this.View.Frame, config)
			{
				WeakNavigationDelegate = this
			};

			this.View.AddSubview(webView);

			this.webView.LoadRequest(new NSUrlRequest(new NSUrl("http://164.99.26.68:8080/zfe?name=dallas")));

		}

		public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
		{
			System.Diagnostics.Debug.WriteLine(message.Body.ToString());
		}

		[Export("webView:didFinishNavigation:")]
		public void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
		{
			this.webView.EvaluateJavaScriptAsync("sendData();").ContinueWith(t =>
			{
				if (t.Status != System.Threading.Tasks.TaskStatus.RanToCompletion)
				{
					System.Diagnostics.Debug.WriteLine(t.Exception.InnerException.Message);
				}
			});
		}
	}
}

