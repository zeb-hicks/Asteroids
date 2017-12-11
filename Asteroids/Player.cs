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

        public void Draw(SpriteBatch sb, Matrix mCamera) {
            Vector2 pos = Vector2.Transform(Position, mCamera);
            //Rectangle dst = new Rectangle(new Point((int)(pos.X + Sprite.Bounds.Width / 2f), (int)(pos.Y + Sprite.Bounds.Height / 2f)), Sprite.Bounds.Size);
            Rectangle dst = Sprite.Bounds;
            dst.Offset(pos);
            Rectangle src = Sprite.Bounds;
            Vector2 origin = Sprite.Bounds.Size.ToVector2() / 2;
            float rotation = Rotation + mCamera.Rotation.Z;
            Color color = Color.White;

            Effect ef = GameEffects.Effects["NormalMappedSprite"];

            ef.Parameters["NormalTexture"]?.SetValue(SpriteNormal);

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, ef, null);
            sb.Draw(Sprite, dst, src, color, rotation, origin, SpriteEffects.None, 0f);
            sb.End();
        }
    }
}
