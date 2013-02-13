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
    class Background : GameObject
    {
        private int _topRoof, _bottom, _roofBottom, _backgroundWidth;

        private Texture2D _roofNorthEast, _roofNorth, _roofEast, _roofSouthEast,
            _roofNorthWest, _roofWest, _roofSouthWest, _roofSouth, _window,
            _wallBlock, _stoneBlock, _door;

        public Background(SpriteBatch spriteBatch, ContentManager content, int windowHeight)
            : base(spriteBatch, content)
        {
            _roofNorthEast = content.Load<Texture2D>("Roof North East");
            _roofNorth = content.Load<Texture2D>("Roof North");
            _roofEast = content.Load<Texture2D>("Roof East");
            _roofSouthEast = content.Load<Texture2D>("Roof South East");
            _roofNorthWest = content.Load<Texture2D>("Roof North West");
            _roofWest = content.Load<Texture2D>("Roof West");
            _roofSouthWest = content.Load<Texture2D>("Roof South West");
            _roofSouth = content.Load<Texture2D>("Roof South");
            _window = content.Load<Texture2D>("Window Tall");
            _wallBlock = content.Load<Texture2D>("Wall Block Tall");
            _stoneBlock = content.Load<Texture2D>("Stone Block");
            _door = content.Load<Texture2D>("Door Tall Closed");

            _backgroundWidth = 7;
            _topRoof = -40;
            _bottom = windowHeight - _stoneBlock.Height;
            _roofBottom = 115;
        }

        internal override void Update()
        {

        }

        internal override void Draw()
        {
            DrawStreet();
            DrawWall();
            DrawRoof();
        }

        public void DrawRoof()
        {
            // Loop that draws the top row of the roof
            for (int i = 0; i < _backgroundWidth; i++)
            {
                if (i == 0)
                    spriteBatch.Draw(_roofNorthWest, new Vector2(_window.Width * i, _topRoof), Color.White);
                else if (i == 6)
                    spriteBatch.Draw(_roofNorthEast, new Vector2(_window.Width * i, _topRoof), Color.White);
                else
                    spriteBatch.Draw(_roofNorth, new Vector2(_window.Width * i, _topRoof), Color.White);
            }

            // Loop that draws the middle row of the roof
            for (int i = 0; i < _backgroundWidth; i++)
            {
                if (i == 0)
                    spriteBatch.Draw(_roofWest, new Vector2(_window.Width * i, _bottom - _roofSouthWest.Height - 100), Color.White);
                else if (i == 5)
                    spriteBatch.Draw(_roofNorth, new Vector2(_window.Width * i, 0), Color.White);
                else if (i == 6)
                    spriteBatch.Draw(_roofEast, new Vector2(_window.Width * i, _bottom - _roofSouthWest.Height - 100), Color.White);
                else
                    spriteBatch.Draw(_window, new Vector2(_window.Width * i, _bottom - _roofSouthWest.Height - 60), Color.White);
            }

            // Loop that draws the bottom row of the roof.
            for (int i = 0; i < _backgroundWidth; i++)
            {
                if (i == 0)
                    spriteBatch.Draw(_roofSouthWest, new Vector2(0, _roofBottom), Color.White);
                else if (i == 5)
                    spriteBatch.Draw(_window, new Vector2(_roofSouth.Width * i, _roofBottom), Color.White);
                else if (i == 6)
                    spriteBatch.Draw(_roofSouthEast, new Vector2(_roofSouth.Width * i, _roofBottom), Color.White);
                else
                    spriteBatch.Draw(_roofSouth, new Vector2(_roofSouth.Width * i, _roofBottom), Color.White);
            }
        }

        public void DrawWall()
        {
            for (int i = 0; i < _backgroundWidth; i++)
            {
                if (i == 5)
                    spriteBatch.Draw(_door, new Vector2(_wallBlock.Width * i, _bottom - _wallBlock.Height + 80), Color.White);
                else
                    spriteBatch.Draw(_wallBlock, new Vector2(_wallBlock.Width * i, _bottom - _wallBlock.Height + 60), Color.White);
            }
        }

        public void DrawStreet()
        {
            for (int i = 0; i < _backgroundWidth; i++)
            {
                spriteBatch.Draw(_stoneBlock, new Vector2(_stoneBlock.Width * i, _bottom), Color.White);
            }
        }
    }
}