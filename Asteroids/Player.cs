using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids {
    public class Player : Entity {
        public string Name { get; set; }

        public float ReloadTime { get; set; }
        public float ReloadDelay { get; set; }
        public float Damage { get; set; }
        public float Health { get; set; }
        public float HealthMax { get; set; }

        public Player() {
            
        }
    }
}
