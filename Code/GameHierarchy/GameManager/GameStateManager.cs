using MoRe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        internal Player chosenPlayer;
       
        internal GameState Update(GameState currentState)
        {
            // if there is no previous gamestate, set it to the currentstate (Menu), and update the current state to the next state (Play).
            // call SwitchGameStates;
            if(oldState == null)
            {
                oldState = currentState;

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
            switch (currentState.nextState)
            {
                case GameState.States.Play:
                    return new PlayState(chosenPlayer);
                case GameState.States.Menu:
                    return new MenuState();
                case GameState.States.Settings:
                    return new SettingsState();
            }

            // in case of error or mistake, just return a new MenuState.
            return new MenuState();
        }
    }
}
