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
    class CharacterHandler : GameObject
    {
        private List<Characters> _characterList;
        private Stopwatch _gameTime;
        private Random rand;
        private int _timeBetweenCharacters, _timeSinceLastRandom, _lives;
        private Texture2D _heart;

        public CharacterHandler(SpriteBatch spriteBatch, ContentManager content)
            : base(spriteBatch, content)
        {
            _heart = content.Load<Texture2D>("Heart");
            rand = new Random();
            _characterList = new List<Characters>();
            _characterList.Add(new Characters(spriteBatch, content));

            _gameTime = new Stopwatch();

            _timeBetweenCharacters = rand.Next(2000, 5000);
            _timeSinceLastRandom = 0;

            _lives = 5;

            _gameTime.Start();
        }

        internal override void Update()
        {
            _timeSinceLastRandom += (int)_gameTime.ElapsedMilliseconds;
            _gameTime.Restart();
            if (_timeSinceLastRandom > _timeBetweenCharacters && _characterList.Count < 10)
            {
                _characterList.Add(new Characters(spriteBatch, content));
                _timeSinceLastRandom = 0;
                _timeBetweenCharacters = rand.Next(2000, 5000);
            }

            foreach (Characters character in _characterList)
            {
                character.Update();
                if (character.collision == true)
                {
                    LoseLives();
                }
            }
        }

        internal override void Draw()
        {
            foreach (Characters c in _characterList)
                c.Draw();

            for (int i = 0; i < _lives; i++)
            {
                spriteBatch.Draw(_heart, new Rectangle(_heart.Width * i, 0, 60, 60), Color.White);
            }
        }
        public void LoseLives()
        {
            _lives -= 1;
        }

        public bool IsGameLost()
        {
            return (_lives == 0);
        }
    }
}