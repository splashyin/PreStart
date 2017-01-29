using Color = Xamarin.Forms.Color;
#if NETFX_CORE || WINDOWS_UWP
using NativeColor = Windows.UI.Color;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#elif WINDOWS_PHONE
using System.Windows.Controls;
using System.Windows.Media;
using NativeColor = System.Windows.Media.Color;
#elif __IOS__
using UIKit;
using Xamarin.Forms.Platform.iOS;
using NativeColor = UIKit.UIColor;
#elif __ANDROID__
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using NativeColor = Android.Graphics.Color;
#endif

namespace SignaturePad.Forms.Platform
{
    public static class ColorExtensions
    {
#if WINDOWS_PHONE || NETFX_CORE || WINDOWS_UWP
        public static NativeColor ToWindows(this Color color)
        {
            return NativeColor.FromArgb(
                (byte)(color.A * 255),
                (byte)(color.R * 255),
                (byte)(color.G * 255),
                (byte)(color.B * 255));
        }
#endif

        public static NativeColor ToNative(this Color color)
        {
#if NETFX_CORE || WINDOWS_UWP
            return color.ToWindows();
#elif WINDOWS_PHONE
            return color.ToWindows();
#elif __IOS__
            return color.ToUIColor();
#elif __ANDROID__
            return color.ToAndroid();
#endif
        }

#if NETFX_CORE || WINDOWS_UWP
        public static void SetTextColor(this TextBlock textBlock, Color color)
        {
            textBlock.Foreground = new SolidColorBrush(color.ToNative());
        }
#elif WINDOWS_PHONE
        public static void SetTextColor(this TextBlock textBlock, Color color)
        {
            textBlock.Foreground = new SolidColorBrush(color.ToNative());
        }
#elif __IOS__
        public static void SetTextColor(this UILabel label, Color color)
        {
            label.TextColor = color.ToNative();
        }
        public static void SetTextColor(this UIButton button, Color color)
        {
            button.SetTitleColor(color.ToNative(), UIControlState.Normal);
        }
#elif __ANDROID__
        public static void SetTextColor(this TextView label, Color color)
        {
            label.SetTextColor(color.ToNative());
        }
#endif
    }
}
