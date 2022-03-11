using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJom
{
    public class AutomatedDraw
    {

        static SpriteBatch spriteBatch = Game1.spriteBatch;
        static GraphicsDeviceManager graphics = Game1.graphics;
        static double ScreenSizeAdjustment = Game1.ScreenSizeAdjustment;
        static Vector CalculationScreenSize = Game1.calculationScreenSize;
        //constructor variables

        Rectangle DisplayLocation;
        Vector Centering;
        Color Color;
        double zoom;
        bool Drawn;
        // constructor, this takes the camera properties as paramters

        public AutomatedDraw(Rectangle displayLocation, Vector centering, Color color, bool drawn = true, double zoom = 1)
        {
            this.Centering = centering;
            this.DisplayLocation = displayLocation;
            this.Color = color;
            this.Drawn = drawn;
            this.zoom = zoom;
        }
        public AutomatedDraw(Rectangle displayLocation, Color color, bool drawn = true, double zoom = 1)
            : this(displayLocation, 
                  
                  //this code gets 3840 / 2 and 2160 / 2
                  new Vector((int)(displayLocation.Width / 2 / ScreenSizeAdjustment), (int)(displayLocation.Height / 2 / ScreenSizeAdjustment)), 
                  
                  color, drawn, zoom) { }

        // the draw code that coombines individrual draw parameters and constructor parameters and draws the result

        public void draw(Rectangle locationShape, Texture2D texture, Rectangle usedTexture, Color color)
        {
            if (Drawn)
            {

                // the size, shape, and location of the object on the screen

                Rectangle Processed = DisplayRectangle(locationShape);

                // code that stops drawing objects that are offscreen

                if (!(Processed.Right < DisplayLocation.Left || Processed.Left > DisplayLocation.Right
                    || Processed.Bottom < DisplayLocation.Top || Processed.Top > DisplayLocation.Bottom))
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(texture, Processed, usedTexture, color);
                    spriteBatch.End();
                }
            }
        }
        public Rectangle DisplayRectangle(Rectangle locationShape)
        {
            Rectangle calculationRectangle = new Rectangle(
                locationShape.X - Centering.X,
                locationShape.Y - Centering.Y,
                locationShape.Width,
                locationShape.Height);
            return new Rectangle(

                    (int)((calculationRectangle.X
                    * ScreenSizeAdjustment + DisplayLocation.Width / 2) * zoom + DisplayLocation.X 
                    ),

                    (int)((calculationRectangle.Y 
                    * ScreenSizeAdjustment + DisplayLocation.Height / 2) * zoom + DisplayLocation.Y
                    ),

                    (int)((calculationRectangle.Width)
                    * ScreenSizeAdjustment * zoom
                    ),
                    (int)((calculationRectangle.Height)
                    * ScreenSizeAdjustment * zoom
                    ));
        }
        // overload
        public void draw(Rectangle locationShape, Texture2D texture, Color color)
        {
            draw(locationShape, texture, new Rectangle(0, 0, texture.Width, texture.Height), color);
        }
        public void draw(Rectangle locationShape, Texture2D texture)
        {
            draw(locationShape, texture, Color);
        }
    }
}
