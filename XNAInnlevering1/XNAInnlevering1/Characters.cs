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
using System.Diagnostics;

namespace XNAInnlevering1
{
    class Characters : GameObject
    {
        private Texture2D _charBoy, _charCatGirl, _charHornGirl,
            _charPinkgirl, _charPrincessGirl;

        private MouseState _previousMouseState, _currentMouseState;

        private Vector2 _charPosition;

        internal bool collision { get; set; }

        private Rectangle _mouseRect;
        private Rectangle _characterHitBox;

        private Random rand;

        private Stopwatch _gameTime;

        private int _charStartPositionX, _charStartPositionY, _windowWidth,_movementSpeed, 
            _randomCharacter, _timeSinceLastCharacter, _timeBetweenCharacters, _timeToIncreaseSpeed, 
            _timeBetweenSpeedIncrease;


        public Characters(SpriteBatch spriteBatch, ContentManager contentMananger)
            : base(spriteBatch, contentMananger)
        {
            _charBoy = content.Load<Texture2D>("Character Boy");
            _charCatGirl = content.Load<Texture2D>("Character Cat Girl");
            _charHornGirl = content.Load<Texture2D>("Character Horn girl");
            _charPinkgirl = content.Load<Texture2D>("Character Pink Girl");
            _charPrincessGirl = content.Load<Texture2D>("Character Princess Girl");
            
            _charStartPositionX = -1000;
            _charStartPositionY = 480 - _charBoy.Height;
            _windowWidth = 800 - _charBoy.Width;
            _movementSpeed = 3;
            _randomCharacter = 0;
            _timeSinceLastCharacter = 0;
            _timeBetweenCharacters = 2000;
            _timeBetweenSpeedIncrease = 10000;

            _gameTime = new Stopwatch();
            _gameTime.Start();
            rand = new Random();
            _charPosition = new Vector2(_charStartPositionX, _charStartPositionY);
        }

        internal override void Update()
        {
            collision = false;
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            _mouseRect = new Rectangle(_currentMouseState.X, _currentMouseState.Y, 10, 10);

            _characterHitBox = new Rectangle((int)_charPosition.X + 17, (int)_charPosition.Y + 61, 67, 78);

            _charPosition.X += _movementSpeed;

            if (_mouseRect.Intersects(_characterHitBox) && IsMousePressed())
                _charPosition.X = _charStartPositionX;

            if (_charPosition.X > _windowWidth)
            {
                _charPosition.X = _charStartPositionX;
                collision = true;
            }

            /* 
             * Øker farten hvis karakteren er utenfor skjermen. Tiden er 
             * lagt til for at karakteren ikke skal bytte utseende for 
             * hver frame den er utenfor skjermen. 
             */ 
            if (_timeToIncreaseSpeed > _timeBetweenSpeedIncrease && 
                _charPosition.X < -101) 
            {
                _timeToIncreaseSpeed = 0;
                _movementSpeed += rand.Next(1, 3);
            }
            _timeToIncreaseSpeed += (int)_gameTime.ElapsedMilliseconds;
            _timeSinceLastCharacter += (int)_gameTime.ElapsedMilliseconds;
            _gameTime.Restart();
            if (_timeSinceLastCharacter > _timeBetweenCharacters &&
                _charPosition.X < 20)
            {
                _randomCharacter = rand.Next(0, 5);
                _timeSinceLastCharacter = 0;
            }
        }

        internal void DrawCharacters (int random)
        {
            switch (random)
            {
                case 0:
                    spriteBatch.Draw(_charBoy, _charPosition, Color.White);
                    break;
                case 1:
                    spriteBatch.Draw(_charCatGirl, _charPosition, Color.White);
                    break;
                case 2:
                    spriteBatch.Draw(_charHornGirl, _charPosition, Color.White);
                    break;
                case 3:
                    spriteBatch.Draw(_charPinkgirl, _charPosition, Color.White);
                    break;
                case 4:
                    spriteBatch.Draw(_charPrincessGirl, _charPosition, Color.White);
                    break;
                default:
                    spriteBatch.Draw(_charBoy, _charPosition, Color.White);
                    break;
            }
        }

        internal override void Draw()
        {
            DrawCharacters(_randomCharacter);
        }

        internal bool IsMousePressed()
        {
            if (_currentMouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }  
    }
}