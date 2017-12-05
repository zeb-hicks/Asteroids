using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Asteroids {
    public struct Frame {
        Rectangle SrcRect;
        int Duration;
        public Frame(Rectangle src, int dur) {
            SrcRect = src;
            Duration = dur;
        }
    }
    public class Particle : Entity {
        public List<Frame> Frames { get; set; } = new List<Frame>();
        public Texture2D Texture;
        private Point textureSize;

        public Particle(Texture2D tex, int start, int end) {
            
        }
        public Particle(Texture2D tex, List<Frame> frames) {
            Frames.AddRange(frames);
        }
    }
}
