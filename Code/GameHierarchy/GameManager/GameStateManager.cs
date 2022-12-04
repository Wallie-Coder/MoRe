using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    // a class for managing the GameStates
    internal class GameStateManager
    {
        // the previous GameState.
        private GameState oldState = null;

        internal GameState Update(GameState currentState)
        {
            // if there is no previous gamestate, set it to the currentstate (Menu), and update the current state to the next state (Play).
            // call SwitchGameStates;
            if(oldState == null)
            {
                oldState = currentState;
                currentState = new PlayState();
            }
            else
            {
                currentState = SwitchGameStates(currentState);
            }

            return currentState;
        }

        internal GameState SwitchGameStates(GameState currentState)
        {
            // Swith to the GameState desired by the currentState.
            switch(currentState.nextState)
            {
                case GameState.States.Play:
                    return new PlayState();
                case GameState.States.Menu:
                    return new MenuState();
            }

            // in case of error or mistake, just return a new MenuState.
            return new MenuState();
        }
    }
}
