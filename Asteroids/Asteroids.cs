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

        Texture2D particles_d;
        Texture2D particles_n;

        RenderTargetBinding[] ZBuffer;
        RenderTarget2D[] ZBufferTargets;

        RenderTarget2D OutputTarget;

        float vw, vh;

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

            player.Position = new Vector2(GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 4);

            particles_d = Content.Load<Texture2D>("Particles/particles_diffuse");
            particles_n = Content.Load<Texture2D>("Particles/particles_normal");

            GameEffects.Effects.Add("NormalMappedSprite", Content.Load<Effect>("Effects/NormalMappedSprite"));

            ZBuffer = new RenderTargetBinding[2];
            ZBufferTargets = new RenderTarget2D[2];

            ZBufferTargets[0] = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2); // Diffuse
            ZBufferTargets[1] = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2); // Normal
            ZBuffer[0] = new RenderTargetBinding(ZBufferTargets[0]); // Diffuse
            ZBuffer[1] = new RenderTargetBinding(ZBufferTargets[1]); // Normal

            OutputTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
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

            GraphicsDevice.SetRenderTargets(ZBuffer);

            player.Draw(spriteBatch, Matrix.Identity);
            
            DrawQuad(ZBufferTargets[0], 0.0f, 0.0f, 0.5f, 0.5f, OutputTarget);
            DrawQuad(ZBufferTargets[1], 0.5f, 0.0f, 0.5f, 0.5f, OutputTarget);

            DrawQuad(OutputTarget);

            base.Draw(gameTime);
        }

        void DrawQuad(Texture2D tex, float x = 0f, float y = 0f, float w = 1f, float h = 1f, RenderTarget2D target = null) {
            int rx = (int)(x * vw);
            int ry = (int)(y * vh);
            int rw = (int)(w * vw);
            int rh = (int)(h * vh);
            Rectangle rect = new Rectangle(rx, ry, rw, rh);

            GraphicsDevice.SetRenderTarget(target);

            spriteBatch.Begin();
            spriteBatch.Draw(tex, rect, Color.White);
            spriteBatch.End();
        }
    }
}
