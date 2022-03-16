using System.Windows.Media;

namespace HarciKalapacs.Renderer.Config
{
    public static class SelectMapConfig
    {
        public const string BackgroundImageName = "HarciKalapacs.Renderer.Assets.mapMenu.jpg";

        public const string BtImage = "HarciKalapacs.Renderer.Assets.button.png";
        public const string BtSelectImage = "HarciKalapacs.Renderer.Assets.selectedButton.png";
        public const double BtWidth = 150;
        public const double BtHeight = 50;
        public const double SpaceBetweenButtons = 500;
        public const int BtFontSize = 15;

        public static double TopBtXPos;
        public static double TopBtYPos;

        public static FontFamily BtFontFamily = new FontFamily("Arial");
        public static Brush FontColor = Brushes.White;
    }
}
