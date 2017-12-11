using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
//using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Asteroids {
    public static class MusicController {
        public static bool Percussion = false;

        static Dictionary<string, Song> Tracks = new Dictionary<string, Song>();

        public static void LoadMusic(ContentManager Content) {
            Tracks["level1"] = Content.Load<Song>("Music/level1");
        }

        public static void PlayMusic() {
            MediaPlayer.Volume = 0.5f;

            MediaPlayer.Play(Tracks["level1"]);
        }
    }
}
