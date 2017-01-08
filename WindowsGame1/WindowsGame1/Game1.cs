using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont lives;
        int livesNum;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1000;

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
            livesNum = 3;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        Texture2D sunglasses;
        Texture2D character;
        private Rectangle? sourceRectangle;
        private float rotation;
        private SpriteEffects effects;
        private float layerDepth;

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sunglasses = Content.Load<Texture2D>("dealwithit");
            character = Content.Load<Texture2D>("megamansprite");
            lives = Content.Load<SpriteFont>("SpriteFont1");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        Random startX = new Random();
        Vector2 positionSunglasses = new Vector2(400, 0);
        Vector2 positionCharacter = new Vector2(300, 400);

        Vector2 livesCounter = new Vector2(800, 0);

        float yMovement;
        float xMovement= 400;

        Vector2 charMovement = new Vector2(1, 0);

       
      

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Q) || Keyboard.GetState().IsKeyDown(Keys.Left))
                xMovement -= 10;
            else if (Keyboard.GetState().IsKeyDown(Keys.E) || Keyboard.GetState().IsKeyDown(Keys.Right))
                xMovement += 10;
            else if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Down))
                yMovement += 10;

            yMovement += (float)gameTime.ElapsedGameTime.TotalSeconds * 100;

            int MaxX = graphics.GraphicsDevice.Viewport.Width - character.Width;
            int MinX = 0;
            int MaxY = graphics.GraphicsDevice.Viewport.Height - 1;
            int MinY = 0;

            int start = startX.Next(0, 800);

            positionCharacter += charMovement * (float)gameTime.ElapsedGameTime.TotalSeconds * 500;

            if (positionCharacter.X > MaxX)
            {
                charMovement.X *= -1;
                positionCharacter.X = MaxX;
            }
            else if (positionCharacter.X < MinX)
            {
                charMovement.X *= -1;
                positionCharacter.X = MinX;
            }

            if (yMovement > MaxY)
            {
                yMovement = MinY;
                livesNum--;
            }
            double sunHeight = sunglasses.Height * .1;
            double sunWidth = sunglasses.Width * .1;

            Rectangle charHitbox = new Rectangle((int)positionCharacter.X + 100, 554, 5, 30);
            Rectangle sunHitbox = new Rectangle((int)xMovement, (int)yMovement, 5, 30);

            if (sunHitbox.Intersects(charHitbox) || livesNum < 0)
            {
                this.Exit();
            }


            positionSunglasses = new Vector2(xMovement, yMovement);
       

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            float scaleSun = .1f;
            
           

            spriteBatch.Begin();
            spriteBatch.DrawString(lives, "Lives:" + livesNum.ToString(), livesCounter, Color.Black);
            spriteBatch.Draw(character, positionCharacter, Color.White);
            spriteBatch.Draw(sunglasses, positionSunglasses, sourceRectangle, Color.Wheat, rotation, positionSunglasses, scaleSun, effects, layerDepth);         
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}