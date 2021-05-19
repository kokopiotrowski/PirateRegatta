using UnityEngine;

namespace GameScripts
{
    public class DiceCheckSideScript : MonoBehaviour
    {
        private Vector3 diceVelocity;
        public int diceValue = 0;
    
        
   
        private void FixedUpdate()
        {
            diceVelocity = DiceScript.diceVelocity;
        }

        private void OnTriggerStay(Collider other)
        {
            if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f && DiceScript.roolingDice)
            {
            
                switch (other.gameObject.name)
                {
                    case "Side1":
                        diceValue = 6;
                        break;
                    case "Side2":
                        diceValue = 5;
                        break;
                    case "Side3":
                        diceValue = 4;
                        break;
                    case "Side4":
                        diceValue = 3;
                        break;
                    case "Side5":
                        diceValue = 2;
                        break;
                    case "Side6":
                        diceValue = 1;
                        break;
                }
                print("Rolled: " + diceValue);
                DiceScript.roolingDice = false;
                if (GameController.Instance.currentGameState == GameState.PlayerRoleDice)
                {
                    PlayerMovementController.MovesLeft = diceValue;
                    GameController.Instance.SetGameState(GameState.PlayerMoving);
                } else if (GameController.Instance.currentGameState == GameState.EnemyRollingDice)
                {
                    OpponentMovementController.MovesLeft = diceValue;
                    GameController.Instance.SetGameState(GameState.EnemyMoving);
                }
            }
        }
    }
}
