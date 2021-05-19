using System.Collections;
using System.Collections.Generic;
using GameScripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;


public delegate void OnMove();
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    [SerializeField] private MapManager _mapManager;
    [SerializeField] private DiceCheckSideScript diceCheck;

    private Vector3 cellSize;
    private Vector3 direction;

    public static int MovesLeft;
    private bool hasMoved;

    public static event OnMove PlayerMoved;

    void Start()
    {
        cellSize = grid.cellSize;
        MovesLeft = 0;
    }
    
    void Update()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            hasMoved = false;
        } else if (Input.GetAxis("Horizontal") != 0 && !hasMoved && MovesLeft > 0)
        {
            hasMoved = true;
            GetMovementDirection();
        }
    }

    public void GetMovementDirection()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                direction = new Vector3((float) (-0.5 * cellSize.x), (float) -0.75 * cellSize.y , 0);
            } else if (Input.GetAxis("Vertical") > 0)
            {
                direction = new Vector3((float) (-0.5 * cellSize.x), (float) 0.75 * cellSize.y , 0);
            }
            else
            {
                direction = new Vector3(-cellSize.x, 0, 0);
            }
            
        } else if (Input.GetAxis("Horizontal") > 0)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                direction = new Vector3((float) (0.5 * cellSize.x), (float) -0.75 * cellSize.y , 0);
            } else if (Input.GetAxis("Vertical") > 0)
            {
                direction = new Vector3((float) (0.5 * cellSize.x), (float) 0.75 * cellSize.y , 0);
            }
            else
            {
                direction = new Vector3(cellSize.x, 0, 0);
            }
        }
        
        Vector3 futurePosition = transform.position + direction;
        
        if (_mapManager.CheckIfSwimmable(futurePosition))
        {
            Move(direction);
        }
    }

    public static void AddOnMoved(OnMove handler)
    {
        PlayerMoved += handler;
    }

    

    public void Move(Vector3 direction)
    {
        transform.position += direction;
        MovesLeft--;
        PlayerMoved();
        if (_mapManager.CheckIfWin(transform.position))
        {
            GameController.Instance.SetGameState(GameState.PlayerWin);
        }
        else if (MovesLeft == 0)
        {
            GameController.Instance.SetGameState(GameState.EnemyRollingDice);
        }
    }
}
