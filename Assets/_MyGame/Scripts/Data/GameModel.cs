using System;
public sealed class GameModel
{
       public event Action<GameState> OnChangeGameState;

       private GameState _currentState; 

       public GameState CurrentState
       {
              get => _currentState;
              set
              {
                     if (_currentState == value)
                     {
                            return;
                     }
                     OnChangeGameState?.Invoke(value);
                     _currentState = value;
             
              }
       }
       
}