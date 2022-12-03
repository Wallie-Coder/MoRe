using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class InputHelper
    {
        public static Keys[] currentKeys = { };
        public static Keys[] previousKeys = { };


        
        static InputHelper()
        {
            currentKeys = Keyboard.GetState().GetPressedKeys();
            previousKeys = currentKeys;
        }
        public static void Update() 
        {
            previousKeys = currentKeys;
            currentKeys= Keyboard.GetState().GetPressedKeys();
        }

        public static bool IsKeyJustReleased(Keys key)
        {
            if (!previousKeys.Contains(key) && currentKeys.Contains(key))
                return true;
            else
                return false;
        }
        public static bool IsKeyPressed(Keys key)
        {
            if (currentKeys.Contains(key))
                return true;
            else
                return false;
        }
    }
}
