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
    }

    public class ServiceSound : IServiceSound
    {
        public List<Sound> sounds;
        protected Game game;
        private Sound songPlaying;

        public ServiceSound(Game pGame)
        {
            game = pGame;
            //Sounds
            MediaPlayer.IsRepeating = true;
            ServiceLocator.RegisterService<IServiceSound>(this);
        }

        public void PlayMusic(string soundName)
        {
            StopMusic();

            sounds = new List<Sound>
            {
                new Sound("OpenYourEyes-part1", game.Content.Load<Song>("music/OpenYourEyes-part1")),
                new Sound("OpenYourEyes-part2", game.Content.Load<Song>("music/OpenYourEyes-part2"))
            };

            for (int i = sounds.Count - 1; i >= 0; i--)
            {
                Sound sound = sounds[i];
                if (sound.Name == soundName)
                {
                    songPlaying = sound;
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
    }
}
