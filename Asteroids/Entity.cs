using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids {
    public enum CollisionType {
        None,
        AABB,
        OBB,
        Precise
    }

    public class Entity {
        public Texture Sprite { get; set; }
        public Vector2 Position { get; set; } = new Vector2();
        public Vector2 Velocity { get; set; } = new Vector2();
        public float RotSpeed { get; set; } = 0f;
        public float Mass { get; set; } = 0f;

        public CollisionType Collision { get; set; } = CollisionType.None;

        public Entity() {}
        public Entity(Texture sprite, CollisionType collision = CollisionType.None) {
            Sprite = sprite;
            Collision = collision;
        }
        public Entity(Texture sprite, Vector2 pos, Vector2 vel, CollisionType collision = CollisionType.None) {
            Sprite = sprite;
            Position = pos;
            Velocity = vel;
            Collision = collision;
        }
        public Entity(Texture sprite, Vector2 pos, Vector2 vel, float rotspeed, float mass, CollisionType collision = CollisionType.None) {
            Sprite = sprite;
            Position = pos;
            Velocity = vel;
            RotSpeed = rotspeed;
            Mass = mass;
            Collision = collision;
        }
    }
}
