using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJom
{
    class AutomatedDraw
    {

        static SpriteBatch spriteBatch = Game1.spriteBatch;
        static GraphicsDeviceManager graphics = Game1.graphics;
        static double ScreenSizeAdjustment = Game1.ScreenSizeAdjustment;
        //constructor variables

        Vector Centering;
        Vector UiOffset;
        double Zoom;
        Color Color;
        bool Drawn;
        // constructor, this takes the camera properties as paramters

        public AutomatedDraw(Vector centering, Vector uiOffset, Color color, bool drawn = true, double zoom = 1)
        {
            this.Centering = centering;
            this.UiOffset = uiOffset;
            this.Zoom = zoom;
            this.Color = color;
            this.Drawn = drawn;
        }

        // overloads for constructor

        public AutomatedDraw(Vector uiOffset, Color color, bool drawn = true, double zoom = 1)
            : this(new Vector((int)(Game1.XAdjustedScreen / 2 / ScreenSizeAdjustment), (int)(Game1.YAdjustedScreen / 2 / ScreenSizeAdjustment)), uiOffset, color, drawn, zoom) { 
        }
        public AutomatedDraw(bool drawn = true, double zoom = 1)
            : this(new Vector(0 ,0), Color.White, drawn, zoom) { 
        }

        // the draw code that coombines individrual draw parameters and constructor parameters and draws the result

        public void draw(Rectangle locationShape, Texture2D texture, Rectangle usedTexture)
        {
            if (Drawn)
            {

                // the size, shape, and location of the object on the screen

                Rectangle Processed = DisplayRectangle(locationShape);

                // code that stops drawing objects that are offscreen

                if (!(Processed.Right < 0 || Processed.Left > graphics.PreferredBackBufferWidth
                    || Processed.Bottom < 0 || Processed.Top > graphics.PreferredBackBufferHeight))
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(texture, Processed, usedTexture, Color);
                    spriteBatch.End();
                }
            }
        }
        public Rectangle DisplayRectangle(Rectangle locationShape)
        {
            return new Rectangle(

                    (int)((((locationShape.X - Centering.X)
                    * Zoom)
                    + UiOffset.X)
                    * ScreenSizeAdjustment
                    + (graphics.PreferredBackBufferWidth / 2)
                    ),

                    (int)((((locationShape.Y - Centering.Y)
                    * Zoom)
                    + UiOffset.Y)
                    * ScreenSizeAdjustment
                    + (graphics.PreferredBackBufferHeight / 2)
                    ),

                    (int)((locationShape.Width) * Zoom
                    * ScreenSizeAdjustment
                    ),
                    (int)((locationShape.Height) * Zoom
                    * ScreenSizeAdjustment
                    ));
        }
        // overload

        public void draw(Rectangle locationShape, Texture2D texture)
        {
            draw(locationShape, texture, new Rectangle(0, 0, texture.Width, texture.Height));
        }
    }
}
