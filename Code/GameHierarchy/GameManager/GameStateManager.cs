using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoRe.Code.GameHierarchy.GameManager
{
    internal class GameStateManager
    {
        internal enum GameState { Play, startMenu, Pause};
        private GameState oldState = GameState.Pause;

        internal GameState Update(GameState currentState)
        {
            return currentState;
        }
    }
}
