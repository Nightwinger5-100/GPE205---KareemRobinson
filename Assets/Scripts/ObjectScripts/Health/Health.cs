using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //the current hp of the object
    public float currentHealth;
    //the max hp of the object
    public float maxHealth;

    public void Start()
    {
        //Set the health to max
        currentHealth = maxHealth;
    }

    //get the pawn and reduce their hp by the amount
    public void TakeDamage(float amount, Pawn source)
    {
        //check if the damage amount will be greater than the max hp. If so reduce the damage amount to only damage up to the max
        if (maxHealth < currentHealth + amount)
        {
            //Clamp could be done here but I feel this is simpler for getting the accurate healed amount
            amount = maxHealth - currentHealth + amount;
        }
        //subtract the amount from health and print it
        currentHealth = currentHealth - amount;
        Debug.Log(source.name + " dealt " + amount + " damage to " + gameObject.name);
        //After losing health, check if the pawn is now equal to or less than 0 hp
        if(currentHealth <= 0)
        {
            Die(source);
        }
    }

    //get the pawn and increase their hp by the amount
    public void Heal(float amount, Pawn source)
    {
        //if they aren't gonna gain any hp then there's nothing more to do
        if(currentHealth == maxHealth)
        {
            print("Your health is already full!");
            return;
        }
        //check if the heal amount will be greater than the max hp. If so reduce the heal amount to only heal up to the max
        else if (maxHealth < currentHealth + amount)
        {
            //Clamp could be done here but I feel this is simpler for getting the accurate healed amount
            amount = maxHealth - currentHealth + amount;
        }
        //add the amount to health and print it
        currentHealth = currentHealth + amount;
        Debug.Log(source.name + " healed " + amount + " to " + gameObject.name);
    }

    //check if their dead because their hp is less than 0
    public void Die(Pawn source)
    {
        Destroy(gameObject);
    }
}
