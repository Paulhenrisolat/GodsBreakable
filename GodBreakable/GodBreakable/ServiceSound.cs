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
    public class ServiceSound
    {
        public List<Sound> sounds;
        protected Game game;
        public ServiceSound(Game pGame)
        {
            game = pGame;
            sounds = new List<Sound>();

            sounds.Add(new Sound("test", game.Content.Load<Song>("test")));
        }

        public void PlaySound(string soundName)
        {

        }
    }
}
