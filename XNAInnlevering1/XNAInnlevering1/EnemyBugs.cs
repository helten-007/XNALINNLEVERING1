using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class EnemyBugs : GameObject
    {
        private Texture2D _enemyBug, _gemBlue, _gemOrange, _gemGreen;
        private Rectangle _rectGemBlue, _rectGemOrange, _rectGemGreen,
            _mouseRectangle, _normalBugRect, _lowerBugRect, _bugHitBox;
        private int _posX, _posY, _numberOfGems, _timeBetweenRandoms,
            _position, _timeWithRandoms, _mouseCounter, _timeWithoutRandoms,
            _timeSinceLastRandom;

        private Random rand;
        private MouseState _currentMouseState, _previousMouseState;

        private Stopwatch _gameTime;

        public EnemyBugs(SpriteBatch spriteBatch, ContentManager content, int windowWidth)
            : base(spriteBatch, content)
        {
            _enemyBug = content.Load<Texture2D>("Enemy Bug");
            _gemBlue = content.Load<Texture2D>("Gem Blue");
            _gemOrange = content.Load<Texture2D>("Gem Orange");
            _gemGreen = content.Load<Texture2D>("Gem Green");

            _posX = 101;
            _posY = -40;
            _timeBetweenRandoms = 5000;
            _timeWithRandoms = _timeBetweenRandoms - 600;
            _numberOfGems = 0;
            _mouseCounter = 0;

            _rectGemBlue = new Rectangle(windowWidth - 60, -20, 60, 60);
            _rectGemOrange = new Rectangle(windowWidth - _rectGemBlue.Width * 2, -20, 60, 60);
            _rectGemGreen = new Rectangle(windowWidth - _rectGemBlue.Width * 3, -20, 60, 60);

            _gameTime = new Stopwatch();
            _gameTime.Start();
            rand = new Random();
        }

        internal override void Update()
        {

            _bugHitBox = new Rectangle();
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
            _mouseRectangle = new Rectangle(_currentMouseState.X, _currentMouseState.Y, 10, 10);
            _timeWithoutRandoms += (int)_gameTime.ElapsedMilliseconds;
            _timeSinceLastRandom += (int)_gameTime.ElapsedMilliseconds;
            _gameTime.Restart();

            if (_timeSinceLastRandom > _timeBetweenRandoms)
            {
                
                Console.WriteLine(_timeSinceLastRandom - _timeWithoutRandoms);
                _timeSinceLastRandom = 0;
                _position = rand.Next(1, 6);
                _enemyBug = content.Load<Texture2D>("Enemy Bug");
                Console.WriteLine(_position);
            }
            if (_timeWithoutRandoms > _timeWithRandoms || _mouseCounter >= 21)
            {
                if (_mouseCounter >= 21)
                {
                    _timeSinceLastRandom = _timeWithRandoms;
                    _timeWithoutRandoms = 0;
                    Console.WriteLine("Timers reset");
                }

                Console.WriteLine("ballle");
                _mouseCounter = 0;
                _timeWithoutRandoms = (_timeWithRandoms - _timeBetweenRandoms);
                _position = 0;
                
            }
            if (IsMousePressed() && (_mouseRectangle.Intersects(_normalBugRect) ||
                _mouseRectangle.Intersects(_lowerBugRect)))
            {
                _mouseCounter++;
                Console.WriteLine(_mouseCounter);
                if (_mouseCounter == 20 && _numberOfGems == 0)
                    _enemyBug = content.Load<Texture2D>("Gem Blue");
                if (_mouseCounter == 20 && _numberOfGems == 1)
                    _enemyBug = content.Load<Texture2D>("Gem Orange");
                if (_mouseCounter == 20 && _numberOfGems == 2)
                    _enemyBug = content.Load<Texture2D>("Gem Green");
                if (_mouseCounter == 21)
                {
                    _numberOfGems++;
                }
            }

        }

        internal override void Draw()
        {
            drawGem(_numberOfGems);
            _normalBugRect = new Rectangle((_posX * _position) + 25, _posY + 100, 50, 100);
            _lowerBugRect = new Rectangle((_posX * _position) + 25, _posY + 140, 50, 100);

            switch (_position)
            {
                case 1:
                    spriteBatch.Draw(_enemyBug, _normalBugRect, Color.White);
                    break;

                case 2:
                    spriteBatch.Draw(_enemyBug, _normalBugRect, Color.White);
                    break;

                case 3:
                    spriteBatch.Draw(_enemyBug, _normalBugRect, Color.White);
                    break;

                case 4:
                    spriteBatch.Draw(_enemyBug, _normalBugRect, Color.White);
                    break;

                case 5:
                    spriteBatch.Draw(_enemyBug, _lowerBugRect, Color.White);
                    break;

                default:
                    break;
            }
        }

        internal void drawGem(int numberOfGems)
        {
            if (numberOfGems >= 1)
            {
                spriteBatch.Draw(_gemBlue, _rectGemBlue, Color.White);
            }
            if (numberOfGems >= 2)
            {
                spriteBatch.Draw(_gemOrange, _rectGemOrange, Color.White);
            }
            if (numberOfGems == 3)
            {
                spriteBatch.Draw(_gemGreen, _rectGemGreen, Color.White);
            }
        }

        internal bool IsMousePressed()
        {
            if (_currentMouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed)
                return true;
            
            return false;
        }

        public bool isGameWon()
        {
            return (_numberOfGems == 3);
        }
    }
}