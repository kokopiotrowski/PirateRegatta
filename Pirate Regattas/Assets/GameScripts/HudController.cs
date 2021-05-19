using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts
{
    public class HudController : MonoBehaviour
{
    [SerializeField] private GameObject rollDiceButton;

    [SerializeField] public TextMeshProUGUI gameStateInformer;

    [SerializeField] public GameObject nextPhaseButton;

    [SerializeField] public TextMeshProUGUI numberOfMovesInformer;
    
    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.AddOnStateChanged(UpdateHud);
        PlayerMovementController.AddOnMoved(UpdateNumberOfMoves);
    }

    void UpdateNumberOfMoves()
    {
        numberOfMovesInformer.SetText(PlayerMovementController.MovesLeft.ToString());
    }

    // Update is called once per frame
    void UpdateHud()
    {
        switch (GameController.Instance.currentGameState)
        { 
            case GameState.PlayerTurn:
                numberOfMovesInformer.enabled = false;
                nextPhaseButton.SetActive(true);
                rollDiceButton.SetActive(false);
                gameStateInformer.SetText("Your turn");
                break;
            case GameState.PlayerRoleDice:
                rollDiceButton.SetActive(true);
                nextPhaseButton.SetActive(false);
                gameStateInformer.SetText("Roll the dice!");
                break;
            case GameState.PlayerMoving:
                rollDiceButton.SetActive(false);
                numberOfMovesInformer.SetText(PlayerMovementController.MovesLeft.ToString());
                numberOfMovesInformer.enabled = true;
                gameStateInformer.SetText("Move using Arrows or WASD");
                break;
            case GameState.EnemyRollingDice:
                rollDiceButton.SetActive(false);
                numberOfMovesInformer.enabled = false;
                gameStateInformer.SetText("Enemy is rolling the dice");
                break;
            case GameState.EnemyMoving:
                rollDiceButton.SetActive(false);
                gameStateInformer.SetText("Enemy is moving");
                break;
            case GameState.PlayerWin:
                numberOfMovesInformer.enabled = false;
                nextPhaseButton.SetActive(false);
                rollDiceButton.SetActive(false);
                gameStateInformer.SetText("YOU WIN!");
                break;
            case GameState.OpponentWin:
                numberOfMovesInformer.enabled = false;
                nextPhaseButton.SetActive(false);
                rollDiceButton.SetActive(false);
                gameStateInformer.SetText("YOU LOST :(");
                break;
            default:
                rollDiceButton.SetActive(false);
                numberOfMovesInformer.enabled = false;
                gameStateInformer.SetText("");
                break;
              
        }
        
    }

    public void SetNextGameState()
    {
        GameController.Instance.SetGameState(GameController.Instance.currentGameState + 1);
    }
}
}
