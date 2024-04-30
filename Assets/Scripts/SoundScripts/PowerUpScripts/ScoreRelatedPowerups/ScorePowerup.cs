using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class ScorePowerup : Powerup
{
    
    public float scoreToAdd;
    public float scoreToRemove;

    public override void Apply(PowerUpManager target)
    {
        TankPawn pawn = target.GetComponent<TankPawn>();
        PlayerController pawnController = pawn.GetComponent<PlayerController>();
        pawnController.addToScore(scoreToAdd);
        
    }

    public override void Remove(PowerUpManager target)
    {
        TankPawn pawn = target.GetComponent<TankPawn>();
        PlayerController pawnController = pawn.GetComponent<PlayerController>();
        pawnController.addToScore(scoreToRemove);
    }
}
