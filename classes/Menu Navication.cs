using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace GameJom
{
    public class Menu
    {
        List<Button> buttons;
        Rectangle boundingBox;
        TextFont printParam;
        TextFont hoverPrint;
        TextFont pressPrint;
        int spacing;
        bool loaded;
        public List<bool> outputs;
        public Menu(AutomatedDraw DrawParameter, PrintManager Print, string[] Buttons, Rectangle BoundingBox, int Spacing, 
            TextFont PrintParam, Texture2D HoverPrint, Texture2D PressPrint,
            bool Loaded = true)
        {
            this.buttons = new List<Button>();
            for (int n = 0; n < Buttons.Length; n++)
            {
                this.buttons.Add(new Button(DrawParameter, Print, new Point(BoundingBox.X, (n * spacing) + ((n + 1) * Print.fontSize.Y)), Buttons[n], true));
            }
            this.printParam = PrintParam;
            this.hoverPrint = new TextFont(HoverPrint);
            this.pressPrint = new TextFont(PressPrint);
            this.boundingBox = BoundingBox;
            this.spacing = Spacing;
            this.loaded = Loaded;
        }


        public void MenuUpdate()
        {
            if (loaded)
            {
                for (int n = 0; n < buttons.Count; n++)
                {
                    if (buttons[n].pressedCheck())
                    { buttons[n].ButtonUpdate(pressPrint); }
                    if (buttons[n].hovercheck())
                    { buttons[n].ButtonUpdate(hoverPrint); }
                    buttons[n].ButtonUpdate(printParam);
                }
            }
        }
    }
    public class Button 
    {

        static Point correctScreenSize = Game1.calculationScreenSize;

        AutomatedDraw drawButton;
        Rectangle button;
        
        PrintManager printParam;
        string text;

        public bool pressedLeft;
        public bool pressedRight;

        public bool hovered;

        public bool loaded;

        public Button(AutomatedDraw DrawParameters, Rectangle Button, bool Loaded = true)
        {
            this.drawButton = DrawParameters;
            this.loaded = Loaded;
            this.button = Button;
        }
        public Button(AutomatedDraw DrawParameters, PrintManager PrintParam, Point TextButtonLocation, string Text, bool Loaded = true)
        {
            this.drawButton = DrawParameters;
            this.printParam = PrintParam;
            this.loaded = Loaded;
            this.button = new Rectangle(TextButtonLocation, PrintParam.printSize(Text));
            this.text = Text;
        }


        public void ButtonUpdate(Texture2D texture)
        {
            if (loaded)
            {
                pressedLeft = false;
                pressedRight = false;
                drawButton.draw(button, texture);
                pressedCheck();
            }
        }
        public void ButtonUpdate(TextFont Font)
        {
            if (loaded)
            {
                pressedLeft = false;
                pressedRight = false;
                printParam.Print(drawButton, Font, text, button.Location);
                pressedCheck();
            }
        }

        public bool pressedCheck()
        {
            if (hovercheck())
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    pressedLeft = true;
                    return true;
                }
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    pressedRight = true;
                    return true;
                }
            }
            return false;
        }
        public bool hovercheck()
        {
            button = drawButton.DisplayRectangle(button);
            if (Mouse.GetState().X < button.Right &&
                Mouse.GetState().X > button.Left &&
                Mouse.GetState().Y < button.Bottom &&
                Mouse.GetState().Y > button.Top)
            {
                hovered = true;
                return true;
            }
            return false;
        }
    }
}
