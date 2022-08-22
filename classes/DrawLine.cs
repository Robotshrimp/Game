using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace GameJom
{
    class LineClass
    {
        static SpriteBatch spriteBatch = Game1.spriteBatch;
        public Vector start;
        public Vector end;
        int thiccness;
        public LineClass(Vector Start, Vector End, int Thiccness = 1)
        {
            this.thiccness = Thiccness;
            this.start = Start;
            this.end = End;
        }
        public void DrawLine(int OffsetX = 0, int OffsetY = 0)
        {
            Vector adjStart = new Vector(start.X + OffsetX, start.Y + OffsetY);
            Vector adjEnd = new Vector(end.X + OffsetX, end.Y + OffsetY);
            Vector RelativePostition = new Vector(adjEnd.X - adjStart.X, adjEnd.Y - adjStart.Y); 
            int length = (int)TrigFun.pythag_hypotenus(RelativePostition);// pythagorean theorem hypotenus moment

            // angle finder, might be useful to put in another class
            float rotation = (float)Math.Asin((double)RelativePostition.Y / length);
            if (RelativePostition.X < 0)
            {
                rotation = (float)((Math.PI) - rotation);
            }
            // might be useful to have specific function for draw
            spriteBatch.Begin();
            spriteBatch.Draw(Game1.BasicTexture, null, new Rectangle(adjStart.X, adjStart.Y, length, thiccness), null, new Vector2(0, 0), rotation, null, Color.White);
            spriteBatch.End();
        }
    }
}
