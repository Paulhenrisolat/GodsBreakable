using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class Sound
    {
        public string Name { get; set; }
        public SoundEffect Audio { get; set; }

        public Sound(string name, SoundEffect audio)
        {
            Name = name;
            Audio = audio;
        }
    }
}
