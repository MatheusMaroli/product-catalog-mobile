using Xamarin.Forms;

namespace Mobile.Layout
{
    public static class Colors
    {
        public static Color ColorPrimary { get; private set; } = Color.FromHex("#285baf");
        public static Color ColorSuccess { get; set; } = Color.FromHex("#5cb85c");
        public static Color ColorDanger { get; set; } = Color.FromHex("#d9534f");
        public static Color ColorWarning { get; set; } = Color.FromHex("#FFA047");
        public static Color ColorInfo { get; set; } = Color.FromHex("#5bc0de");

        public static Color ColorBackground { get; set; } = Color.FromHex("#e6e6e6");
        public static Color ColorModalBackgroud { get; set; } = Color.FromRgba(255, 255, 255, .7);
        public static Color ColorText { get; set; } = Color.FromHex("#202020");

        public static Color Purple { get; private set; } = Color.FromHex("#eb34d5");
        public static Color Yellow { get; set; } = Color.FromHex("#fcfc1e");

    }
}
