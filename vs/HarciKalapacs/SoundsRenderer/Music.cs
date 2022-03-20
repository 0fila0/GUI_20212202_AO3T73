using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media;

namespace SoundsRenderer
{
    public class Music : IMusic
    {
        static MediaPlayer musicPlayer = new MediaPlayer();
        static MediaPlayer soundEffectPlayer = new MediaPlayer();

        public Music()
        {
            musicPlayer.MediaEnded += MusicPlayer_MediaEnded;
        }

        public void PlayMusic(MusicType musicType)
        {
            Uri musicPath = null;
            switch (musicType)
            {
                case MusicType.mainMenu:
                    musicPath = new Uri(Directory.GetCurrentDirectory() + @"\Sounds\Song\mainMenu.mp3");
                    break;
                case MusicType.desert:
                    musicPath = new Uri(Directory.GetCurrentDirectory() + @"\Sounds\Atmosphere\desert.mp3");
                    break;
            }

            musicPlayer.Open(musicPath);
            musicPlayer.Position = TimeSpan.Zero;
            musicPlayer.Play();
        }

        private void MusicPlayer_MediaEnded(object sender, EventArgs e)
        {
            musicPlayer.Position = TimeSpan.Zero;
            musicPlayer.Play();
        }

        public void PlaySoundEffect(SoundEffectType effectType)
        {
            Uri soundEffectPath = null;
            switch (effectType)
            {
                case SoundEffectType.selectHelicopter:
                    soundEffectPath = new Uri(Directory.GetCurrentDirectory() + @"\Sounds\SoundEffect\helicopterSelect.mp3");
                    break;
                case SoundEffectType.selectTank:
                    //soundEffectPath = new Uri(Directory.GetCurrentDirectory() + @"\Sounds\SoundEffect\helicopterSelect.mp3");
                    break;
                case SoundEffectType.selectInfantryman:
                    //soundEffectPath = new Uri(Directory.GetCurrentDirectory() + @"\Sounds\SoundEffect\helicopterSelect.mp3");
                    break;
                case SoundEffectType.selectTruck:
                    //soundEffectPath = new Uri(Directory.GetCurrentDirectory() + @"\Sounds\SoundEffect\helicopterSelect.mp3");
                    break;
                case SoundEffectType.helicopterFire:
                    //soundEffectPath = new Uri(Directory.GetCurrentDirectory() + @"\Sounds\SoundEffect\helicopterSelect.mp3");
                    break;
                case SoundEffectType.tankFire:
                    soundEffectPath = new Uri(Directory.GetCurrentDirectory() + @"\Sounds\SoundEffect\tankFire.mp3");
                    break;
                case SoundEffectType.infantrymanFire:
                    //soundEffectPath = new Uri(Directory.GetCurrentDirectory() + @"\Sounds\SoundEffect\helicopterSelect.mp3");
                    break;
                case SoundEffectType.truckFire:
                    //soundEffectPath = new Uri(Directory.GetCurrentDirectory() + @"\Sounds\SoundEffect\helicopterSelect.mp3");
                    break;
                case SoundEffectType.destroyedUnit:
                    //soundEffectPath = new Uri(Directory.GetCurrentDirectory() + @"\Sounds\SoundEffect\helicopterSelect.mp3");
                    break;
            }

            soundEffectPlayer.Open(soundEffectPath);
            soundEffectPlayer.Play();
        }
    }
}
