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

namespace XNAInnlevering1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private CharacterHandler characterHandler;
        private Background drawBackground;
        private EnemyBugs enemyBug;
        private Characters characters;
        private Texture2D _soMuchWin;
        private int _timeSinceFirstWinScreen, _timeWithWinScreens;

        public Game1()
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
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _soMuchWin = Content.Load<Texture2D>("WinScreen");

            spriteBatch = new SpriteBatch(GraphicsDevice);

            characterHandler = new CharacterHandler(spriteBatch, Content);

            drawBackground = new Background(spriteBatch, Content, Window.ClientBounds.Height);

            enemyBug = new EnemyBugs(spriteBatch, Content, Window.ClientBounds.Width);

            characters = new Characters(spriteBatch, Content);

            _timeWithWinScreens = 3000;
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            Window.Title = "Capture the Cutes";

            if (!enemyBug.IsGameWon())
            {
                enemyBug.Update();

                characterHandler.Update();
                if (characterHandler.IsGameLost())
                    Exit();
            }

            if (enemyBug.IsGameWon()) 
            {
                _timeSinceFirstWinScreen += gameTime.ElapsedGameTime.Milliseconds;
                if (_timeSinceFirstWinScreen > _timeWithWinScreens)
                    Exit();
            }

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            drawBackground.Draw();

            enemyBug.Draw();

            characterHandler.Draw();

            characters.Draw();

            if (enemyBug.IsGameWon())
                spriteBatch.Draw(_soMuchWin, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}