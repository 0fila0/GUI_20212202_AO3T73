using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media;

namespace SoundsRenderer
{
    public class Music : IMusic
    {
        MediaPlayer mediaPlayer;

        public MediaPlayer MediaPlayer { get => mediaPlayer; set => mediaPlayer = value; }

        public Music()
        {
            this.mediaPlayer = new MediaPlayer();
            this.mediaPlayer.Position = TimeSpan.Zero;
        }

        public void PlayMusic(MusicType musicType)
        {
            //
            //// Determine path
            //var assembly = Assembly.GetExecutingAssembly();
            //string resourcePath = @"SoundsRenderer\Musical\mainMenu.mp3";
            //// Format: "{Namespace}.{Folder}.{filename}.{Extension}"
            //    resourcePath = assembly.GetManifestResourceNames()
            //        .Single(str => str.EndsWith(@"mainMenu.mp3"));

            //Uri u = new Uri(resourcePath.ToString(), UriKind.Relative);
            //mediaPlayer.Open(u);

            //switch (musicType)
            //{
            //    case MusicType.mainMenu:
            //        this.mediaPlayer.Play();
            //        break;
            //}
        }
    }
}
