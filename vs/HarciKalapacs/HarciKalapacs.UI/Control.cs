namespace HarciKalapacs.UI
{
    using HarciKalapacs.Model;
    using HarciKalapacs.Repository;
    using HarciKalapacs.Renderer;
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    class Control : ContentControl
    {
        readonly IRepository repository;
        readonly IModel model;

        public Control()
        {
            this.repository = App.Current.Services.GetService<IRepository>();
            this.model = App.Current.Services.GetService<IModel>();

            this.Loaded += this.WindowLoaded;
            this.SizeChanged += this.WindowSizeChanged;
        }

        private Canvas PreviousCanvas { get; set; }

        private Window Window { get; set; }

        private void WindowSizeChanged(object sender, RoutedEventArgs e)
        {
            this.Window = this.Parent as Window;
            if (this.Window.Title == "Harci kalapács")
            {
                Renderer.Config.MainMenuConfig.WindowWidth = this.ActualWidth;
                Renderer.Config.MainMenuConfig.WindowHeight = this.ActualHeight;
                Renderer.Config.MainMenuConfig.TopBtXPos = this.ActualWidth / 2;
                Renderer.Config.MainMenuConfig.TopBtYPos = this.ActualHeight;
                this.ValidateFrame();
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            BitmapImage iconImage = new BitmapImage();
            iconImage.BeginInit();
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("HarciKalapacs.UI.Assets.Images.hammer.ico");
            iconImage.StreamSource = stream;
            iconImage.EndInit();
            (this.Parent as Window).Icon = iconImage;

            Window window = Window.GetWindow(this);
        }


        private void ValidateFrame()
        {
            if (this.PreviousCanvas != null)
            {
                this.RemoveLogicalChild(this.PreviousCanvas);
            }

            this.Content = MenuRenderer.MainMenu();
        }
    }
}
