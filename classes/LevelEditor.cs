using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    class LevelEditor
    {
        static MouseState mouseState = Game1.mouseState;

        static List<List<List<int>>> UsedTileMaps;
        static List<List<int>> TileMapSize;
        static bool Drawn = false;
        static double Zoom = 1;
        static Texture2D SelectTexture;
        static AutomatedDraw LevelGraphics = new AutomatedDraw(Game1.ScreenBounds, Color.White, Drawn, Zoom);
        static Vector TileSize = new Vector(96, 96);
        List<List<int>> TileMap;
        int ChangeTo;
        int LayerSerialNum;

        public LevelEditor(List<List<int>> tileMap)
        { this.TileMap = tileMap; }
        public LevelEditor() : this(TileMapSize) { }

        public void Update()
        {
            Rectangle mouselocation = LevelGraphics.CalculationRectangle(new Rectangle(mouseState.X, mouseState.Y, 0, 0));
            Vector selectedTile = new Vector((int)(mouselocation.X / TileSize.X), (int)(mouselocation.Y / TileSize.Y));
            Rectangle currentTile = new Rectangle(selectedTile.X * TileSize.X, selectedTile.Y * TileSize.Y, TileSize.X, TileSize.Y);

            Button Tile = new Button(LevelGraphics, Drawn);
            Tile.ButtonUpdate(currentTile, SelectTexture);
            if (Tile.Pressed)
            {
                TileMap[selectedTile.X][selectedTile.Y] = ChangeTo;
            }
        }
    }
}
