using Foundation;
using UIKit;

namespace AppleMapPolygonTest
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds) {RootViewController = new MapViewController()};
            Window.MakeKeyAndVisible();

            return true;
        }
    }
}