using System;
public sealed class GameModel
{
       public event Action<GameState> OnChangeGameState;

       private GameState _currentState;
       private SoundModel _soundModel;

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

       public SoundModel SoundModel
       {
              get => _soundModel;
              set => _soundModel = value;
       }

       public GameModel(SoundModel soundModel)
       {
              _soundModel = soundModel;
       }
}