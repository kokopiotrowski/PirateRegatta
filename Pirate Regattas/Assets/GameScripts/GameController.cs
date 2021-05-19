using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public enum GameState { PlayerTurn, PlayerRoleDice, PlayerMoving, EnemyTurn, EnemyRollingDice, EnemyMoving, PlayerWin, OpponentWin}

    public delegate void OnStateChangeHandler(); 

    public class GameController : MonoBehaviour
    {
    
        private event OnStateChangeHandler OnStateChange; 
        public GameState currentGameState { get; private set; } 

        public GameState previousGameState { get; private set; }
        private static GameController _instance;

        public static GameController Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }

        public void Start()
        {
            previousGameState = GameState.EnemyMoving;
            currentGameState = GameState.PlayerTurn;
        
        }
 
        //function to change the state
        public void SetGameState(GameState state)
        {
            print("State changed");
            this.previousGameState = this.currentGameState;
            this.currentGameState = state;
            OnStateChange();
        }

        public void AddOnStateChanged(OnStateChangeHandler handler)
        {
            OnStateChange += handler;
        }
 
        public void OnApplicationQuit() {
            GameController._instance = null;
        }
    }
}