using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace XNAInnlevering1
{
    class CharacterHandler : GameObject
    {

        private List<Character> _characterList;
        private Stopwatch _gameTime;
        private Random rand;
        private int _timeBetweenCharacters, _timeSinceLastRandom;

        public CharacterHandler(SpriteBatch spriteBatch, ContentManager content)
            : base(spriteBatch, content)
        {
            rand = new Random();
            _characterList = new List<Character>();
            _characterList.Add(new Character(spriteBatch, content));

            _gameTime = new Stopwatch();

            _timeBetweenCharacters = rand.Next(10000, 50000);
            _timeSinceLastRandom = 0;

            _gameTime.Start();
        }

        internal override void Update()
        {
            _timeSinceLastRandom += (int)_gameTime.ElapsedMilliseconds;
            _gameTime.Restart();
            if (_timeSinceLastRandom > _timeBetweenCharacters && _characterList.Count < 10)
            {
                _characterList.Add(new Character(spriteBatch, content));
                _timeSinceLastRandom = 0;
                _timeBetweenCharacters = rand.Next(1000, 5000);
            }

            foreach (Character character in _characterList)
            {
                character.Update();
            }
        }

        internal override void Draw()
        {
            foreach (Character c in _characterList)
                c.Draw();
        }
    }
}