using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Asteroids : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 VPHalfSize;

        Effect normalMappedSprite;

        Player player;
        
        public Asteroids()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player = new Player();

            Mouse.SetCursor(MouseCursor.Crosshair);
            IsMouseVisible = true;

            VPHalfSize = GraphicsDevice.Viewport.Bounds.Size.ToVector2() / 2f;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player.Sprite = Content.Load<Texture2D>("Entities/ship_diffuse");
            player.SpriteNormal = Content.Load<Texture2D>("Entities/ship_normal");

            normalMappedSprite = Content.Load<Effect>("Effects/NormalMappedSprite");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            
            Vector2 mousepos = Mouse.GetState().Position.ToVector2() - VPHalfSize;
            Vector2 drawpos = player.Position + VPHalfSize;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, normalMappedSprite);
            normalMappedSprite.Parameters["NormalTexture"]?.SetValue(player.SpriteNormal);
            normalMappedSprite.Parameters["WorldPosition"]?.SetValue(player.Position);
            normalMappedSprite.Parameters["LightRadius"]?.SetValue(10f);
            normalMappedSprite.Parameters["LightColor"]?.SetValue(new Vector3(1f, 0.4f, 0.2f));
            normalMappedSprite.Parameters["LightPosition"]?.SetValue(new Vector3(mousepos, 10f));
            spriteBatch.Draw(player.Sprite, drawpos, null, Color.White, player.Rotation, player.Sprite.Bounds.Size.ToVector2() / 2f, 1f, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
