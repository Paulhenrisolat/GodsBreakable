using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class Music
    {
        public string Name { get; set; }
        public Song Audio {  get; set; }

        public Music(string name, Song audio)
        {
            Name = name;
            Audio = audio;
        }
    }
}
