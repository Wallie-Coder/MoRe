using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoRe.Code.Utility
{
    // static InputHandler class. (still missing some methods, but has the essential ones.
    public static class InputHelper
    {
        public static Keys[] currentKeys = { };
        public static Keys[] previousKeys = { };
        public static MouseState previousMouseState;
        public static MouseState currentMouseState;



        static InputHelper()
        {
            currentKeys = Keyboard.GetState().GetPressedKeys();
            previousKeys = currentKeys;

            currentMouseState = Mouse.GetState();
            previousMouseState = currentMouseState;
        }
        public static void Update()
        {
            previousKeys = currentKeys;
            currentKeys = Keyboard.GetState().GetPressedKeys();

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        public static bool IsKeyJustReleased(Keys key)
        {
            if (previousKeys.Contains(key) && !currentKeys.Contains(key))
                return true;
            else
                return false;
        }
        public static bool IsKeyJustPressed(Keys key)
        {
            if (!previousKeys.Contains(key) && currentKeys.Contains(key))
                return true;
            else
                return false;
        }
        public static bool IsKeyDown(Keys key)
        {
            if (currentKeys.Contains(key))
                return true;
            else
                return false;
        }

        public static Vector2 MousePosition
        {
            get
            {
                return new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y) / GameObject.WorldScale;
            }
        }

        internal static bool IsMouseOver(GameObject g)
        {
            if (MousePosition.X < g.location.X + g.sprite.Width / 2 * g.ObjectScale &&
                MousePosition.Y < g.location.Y + g.sprite.Height / 2 * g.ObjectScale &&
                MousePosition.X > g.location.X - g.sprite.Width / 2 * g.ObjectScale &&
                MousePosition.Y > g.location.Y - g.sprite.Width / 2 * g.ObjectScale)
                return true;
            else
                return false;
        }

        public static bool LeftMouseButtonJustRelease
        {
            get
            {
                if (previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                    return true;
                return false;
            }
        }
    }
}
