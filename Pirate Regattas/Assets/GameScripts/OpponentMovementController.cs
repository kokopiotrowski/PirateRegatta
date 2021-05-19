using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class OpponentMovementController : MonoBehaviour
    {
        [SerializeField] private Grid grid;

        [SerializeField] private MapManager _mapManager;

        [SerializeField] private DiceScript _dice;

        private enum Direction
        {
            RightUp,
            Right,
            RightDown,
            LeftDown,
            Left,
            LeftUp
        };

        private List<Direction> path;
        private Dictionary<Direction, Vector3> convertDirToVector;

        private Vector3 cellSize;
        private Vector3 direction;

        public static int MovesLeft;
        private int currentStep;

        private bool hasMoved;

        void Start()
        {
            currentStep = 0;
            cellSize = grid.cellSize;
            MovesLeft = 0;
            
            GameController.Instance.AddOnStateChanged(ThrowDice);

            path = new List<Direction>() //prewritten path for the opponent
            {
                Direction.Right,
                Direction.Right,
                Direction.Right,
                Direction.Right,
                Direction.Right,
                Direction.RightUp,
                Direction.RightUp,
                Direction.LeftUp,
                Direction.LeftUp,
                Direction.RightUp,
                Direction.RightUp,
                Direction.Right,
                Direction.Right,
                Direction.Right,
                Direction.Right,
                Direction.Right,
                Direction.Right,
                Direction.Right,
                Direction.RightUp,
                Direction.RightUp,
            };

            convertDirToVector = new Dictionary<Direction, Vector3>();
            convertDirToVector.Add(Direction.RightUp, new Vector3((float) (0.5 * cellSize.x), (float) 0.75 * cellSize.y, 0));
            convertDirToVector.Add(Direction.Right, new Vector3( cellSize.x, 0, 0));
            convertDirToVector.Add(Direction.RightDown, new Vector3((float) (0.5 * cellSize.x), (float) -0.75 * cellSize.y, 0));
            convertDirToVector.Add(Direction.LeftDown, new Vector3((float) (-0.5 * cellSize.x), (float) -0.75 * cellSize.y, 0));
            convertDirToVector.Add(Direction.Left, new Vector3(-cellSize.x, 0, 0));
            convertDirToVector.Add(Direction.LeftUp, new Vector3((float) (-0.5 * cellSize.x), (float) 0.75 * cellSize.y, 0));
        }

        void ThrowDice()
        {
            if (GameController.Instance.currentGameState == GameState.EnemyRollingDice)
            {
                _dice.RollTheDice();
            }
        }


        void Update()
        {
            
            if (GameController.Instance.currentGameState == GameState.EnemyMoving)
            {
                GetMovementDirection();
            }
            
        }

        public void GetMovementDirection()
        {
            direction = convertDirToVector[path[currentStep]];
            Vector3 futurePosition = transform.position + direction;

            if (_mapManager.CheckIfSwimmable(futurePosition))
            {
                Move(direction);
            }
        }


        public void Move(Vector3 direction)
        {
            transform.position += direction;
            MovesLeft--;
            currentStep++;
            
            if (_mapManager.CheckIfWin(transform.position))
            {
                GameController.Instance.SetGameState(GameState.OpponentWin);
            }
            else if (MovesLeft == 0)
            {
                GameController.Instance.SetGameState(GameState.PlayerTurn);
            }
        }



    }
}
