using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    internal class GameStateManager
    {
        private GameState oldState = null;

        internal GameState Update(GameState currentState)
        {
            if (currentState != null)
            {
                if(oldState == null)
                {
                    oldState = currentState;
                    currentState = new PlayState();
                }
                else
                {
                    SwitchGameStates(currentState);
                }
            }
            else
            {
                currentState = new MenuState();
            }
            return currentState;
        }

        internal GameState SwitchGameStates(GameState currentState)
        {
            switch(currentState.nextState)
            {
                case GameState.States.Play:
                    return new PlayState();
                    break;
                case GameState.States.Menu:
                    break;
                case GameState.States.GameOver:
                    break;
            }

            return new MenuState();
        }
    }
}
