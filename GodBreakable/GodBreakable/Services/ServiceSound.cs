using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace GodBreakable
{
    public interface IServiceSound
    {
        void PlayMusic(string soundName);
        void StopMusic();
        string MusicPlaying();
        void PlaySound(string soundName);
    }

    public class ServiceSound : IServiceSound
    {
        public List<Music> musics;
        public List<Sound> sounds;
        protected Game game;
        private Music songPlaying;

        public ServiceSound(Game pGame)
        {
            game = pGame;
            //Musics
            musics = new List<Music>
            {
                new Music("OpenYourEyes-part1", game.Content.Load<Song>("music/OpenYourEyes-part1")),
                new Music("OpenYourEyes-part2", game.Content.Load<Song>("music/OpenYourEyes-part2"))
            };
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;

            //Sounds
            sounds = new List<Sound>
            {
                new Sound("bump", game.Content.Load<SoundEffect>("sound/bump")),
                new Sound("lightning", game.Content.Load<SoundEffect>("sound/lightning")),
                new Sound("brickxplode", game.Content.Load<SoundEffect>("sound/brickxplode")),
                new Sound("counter", game.Content.Load<SoundEffect>("sound/counter"))
            };
            ServiceLocator.RegisterService<IServiceSound>(this);
        }

        public void PlayMusic(string musicName)
        {
            StopMusic();

            for (int i = musics.Count - 1; i >= 0; i--)
            {
                Music music = musics[i];
                if (music.Name == musicName)
                {
                    songPlaying = music;
                    MediaPlayer.Play(songPlaying.Audio);
                }
            }
        }

        public void StopMusic()
        {
            MediaPlayer.Stop();
        }

        public string MusicPlaying()
        {
            if (songPlaying != null)
            {
                return songPlaying.Name;
            }
            else
            {
                return "None";
            }
            
        }

        public void PlaySound(string soundName)
        {
            for (int i = sounds.Count - 1; i >= 0; i--)
            {
                Sound sound = sounds[i];
                if (sound.Name == soundName)
                {
                    sound.Audio.Play();
                }
            }
        }
    }
}
