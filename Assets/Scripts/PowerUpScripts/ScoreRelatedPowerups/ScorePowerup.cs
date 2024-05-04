using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ScorePowerup : Powerup
{
    
    public float scoreToAdd;
    public float scoreToRemove;

    public override void Apply(PowerUpManager target)
    {

        TankPawn pawn = target.GetComponent<TankPawn>();
        PlayerController pawnController = pawn.GetComponent<PlayerController>();
        if (pawnController != null)
        {
            pawnController.addToScore(scoreToAdd);
        }
        else
        {
            List<PlayerController> playerList =  GameManager.instance.players;
        for (int playerlistNum = 0; playerlistNum < playerList.Count; playerlistNum++)
        {
            if (target.gameObject == playerList[playerlistNum].pawn.gameObject)
            {
                //Debug.Log("dwqfevdf");
                PlayerController playerController = playerList[playerlistNum];
                playerController.addToScore(scoreToAdd);
            }
        }
        }
        
    }

    public override void Remove(PowerUpManager target)
    {
        TankPawn pawn = target.GetComponent<TankPawn>();
        PlayerController pawnController = pawn.GetComponent<PlayerController>();
        pawnController.addToScore(scoreToRemove);
    }
}
