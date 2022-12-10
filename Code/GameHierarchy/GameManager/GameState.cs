using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    // the based class for every GameState.
    internal abstract class GameState
    {
        // the Different GameState options.
        public enum States { Menu, Settings, Play, None}

        // the next State, None by default, if changed it means the state will go to the desired state.
        internal protected States nextState { get; protected set; }
        internal static protected bool s_hardmodeSelected;
        internal static protected bool s_fastmodeSelected;
        internal static protected bool s_windowedSelected = true;

        internal static protected int s_red = 0;
        internal static protected int s_green = 0;
        internal static protected int s_blue = 0;
        internal static protected Color s_nameColor;

        internal static protected List<string> s_playerNameList;
        internal static protected string s_playerName = "";

        internal static protected Vector2 s_selectBoxPos = new Vector2(100, 100);

        internal static protected Vector2 s_soundSliderPosition = new Vector2(500, 225);
        internal static protected Vector2 s_soundBoxPosition = new Vector2(500, 225);

        internal static protected Vector2 s_redSliderPosition = new Vector2(500, 425);
        internal static protected Vector2 s_redBoxPosition = new Vector2(410, 425);
        internal static protected Vector2 s_greenSliderPosition = new Vector2(500, 525);
        internal static protected Vector2 s_greenBoxPosition = new Vector2(410, 525);
        internal static protected Vector2 s_blueSliderPosition = new Vector2(500, 625);
        internal static protected Vector2 s_blueBoxPosition = new Vector2(410, 625);

        internal GameState()
        {
            nextState = States.None;
        }

        internal abstract void Update(GameTime gameTime);
        internal abstract void Draw(SpriteBatch batch);

    }
}
