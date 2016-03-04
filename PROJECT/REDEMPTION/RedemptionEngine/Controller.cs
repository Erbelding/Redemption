using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RedemptionEngine.Screens
{
    public static class Controller
    {
        //attributes
        private static KeyboardState keyboardState;
        private static KeyboardState oldKeyboardState;
        private static MouseState mouseState;
        private static MouseState oldMouseState;


        //constructor
        static Controller()
        {
            keyboardState = new KeyboardState();
            oldKeyboardState = new KeyboardState();
            mouseState = new MouseState();
            oldMouseState = new MouseState();
        }

        //get new input every update call
        public static void Update()
        {
            oldKeyboardState = keyboardState;
            oldMouseState = mouseState;
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        //returns true if the key is down
        public static bool KeyIsDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        //returns true if the key was just pressed down
        public static bool SingleKeyPress(Keys key)
        {
            return (oldKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key));
        }

        //returns true if the key was just released
        public static bool OnKeyReleased(Keys key)
        {
            return (oldKeyboardState.IsKeyDown(key) && keyboardState.IsKeyUp(key));
        }


        //returns true if the mouse button is down
        public static bool IsMouseButtonDown(bool Button1)
        {
            if (Button1) return (mouseState.LeftButton == ButtonState.Pressed);
            else return (mouseState.RightButton == ButtonState.Pressed);
        }

        //returnes true if the specified mouse button has been clicked
        public static bool IsMouseButtonClicked(bool Button1)
        {
            if (Button1) return (oldMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed);
            else return (oldMouseState.RightButton == ButtonState.Released && mouseState.RightButton == ButtonState.Pressed);
        }

        //returns true if the specified mouse button has been released
        public static bool IsMouseButtonReleased(bool Button1)
        {
            if (Button1) return (oldMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released);
            else return (oldMouseState.RightButton == ButtonState.Pressed && mouseState.RightButton == ButtonState.Released);
        }

    }
}
