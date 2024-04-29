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
        Pawn pawn = target.GetComponent<Pawn>();
        Controller pawnController = pawn.pawnController;
        pawnController.addToScore(scoreToAdd);
        
    }

    public override void Remove(PowerUpManager target)
    {
        Pawn pawn = target.GetComponent<Pawn>();
        Controller pawnController = pawn.pawnController;
        pawnController.removeFromScore(scoreToRemove);
    }
}
