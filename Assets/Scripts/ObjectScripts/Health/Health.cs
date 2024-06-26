using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //the current hp of the object
    public float currentHealth;
    //the max hp of the object
    public float maxHealth;

    //the time for if the effect is over
    private float nextTimeEvent = 0;
    
    //the time for if the effect needs to occur
    private float timeEventDelay;

    private float timeEventDuration;

    private float amount;

    private float speed;

    private Pawn pawn;

    public Image healthUi;

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
            amount = currentHealth + amount - maxHealth;
        }
        //subtract the amount from health and print it
        currentHealth = currentHealth - amount;
        //Debug.Log(source.name + " dealt " + amount + " damage to " + gameObject.name);
        //After losing health, check if the pawn is now equal to or less than 0 hp
        if(currentHealth <= 0)
        {
            Die(source);
        }
        else
        {
            updateHealthUi();
        }
    }

    //get the pawn and increase their hp by the amount
    public void Heal(float amount, Pawn source)
    {
        //check if the heal amount will be greater than the max hp. If so reduce the heal amount to only heal up to the max
        if (maxHealth < (currentHealth + amount))
        {
            //Clamp could be done here but I feel this is simpler for getting the accurate healed amount
            amount = maxHealth - currentHealth;
        }
        //if they aren't gonna gain any hp then there's nothing more to do
        if(currentHealth == maxHealth || currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            //Debug.Log("Your health is already full!");
            return;
        }
        //add the amount to health and print it
        currentHealth = currentHealth + amount;
        updateHealthUi();
        //Debug.Log(source.name + " healed " + amount + " to " + gameObject.name);
    }

    public void HealOvertime(float healAmount, float healDuration, float healSpeed, Pawn source)
    {   
        //if there's no timer start one and set all the variables
        if (nextTimeEvent <= 0)
        {
            amount = healAmount;
            timeEventDuration = healDuration;
            pawn = source;
            speed = healSpeed;
            timeEventDelay = healSpeed + Time.time;
            nextTimeEvent = Time.time + healDuration;
            
        }
        else
        {
            timeEventDelay = healSpeed + Time.time;
        }
        
        Heal(amount, source);
    }

    public void updateHealthUi()
    {
        healthUi.fillAmount = Mathf.InverseLerp(0, maxHealth, currentHealth);
    }

    public void Update()
    {
        if (nextTimeEvent > 0)
        {
            //if the heal effect is still active...
            if (nextTimeEvent > Time.time)
            {
                //if it's time to heal again...
                if (timeEventDelay < Time.time)
                {
                timeEventDelay = Time.time;
                //regardless run the function again until the effect is over
                HealOvertime(amount, timeEventDuration, speed, pawn); 
                }
       
            }
            else
            {
                //Debug.Log("done");
                nextTimeEvent = 0;
            }
        }
    }

    //check if their dead because their hp is less than 0
    public void Die(Pawn objectThatDidDmg)
    {
        //play death sound effect on the pawn
        GetComponent<TankPawn>().deathSound();
        checkIfGameObjectIsPlayer(gameObject);
        checkIfPawnIsAPlayer(objectThatDidDmg);
        checkifAi(gameObject);
        removeFromPawnList();
        Destroy(gameObject);

    }

    private void removeFromPawnList()
    {
        for (int playerlistNum = 0; playerlistNum < GameManager.instance.storedPawns.Count; playerlistNum++)
        {
            if (gameObject == GameManager.instance.storedPawns[playerlistNum].gameObject)
            {
                GameManager.instance.storedPawns.Remove(GameManager.instance.storedPawns[playerlistNum]);
            }
        }
    }

    private void checkIfGameObjectIsPlayer(GameObject player)
    {
        List<PlayerController> playerList =  GameManager.instance.players;
        for (int playerlistNum = 0; playerlistNum < playerList.Count; playerlistNum++)
        {
            if (gameObject == playerList[playerlistNum].pawn.gameObject)
            {
                GameManager.instance.RespawnPlayer(playerList[playerlistNum]);
            }
        }
    }

    private void checkIfPawnIsAPlayer(Pawn objectThatDidDmg)
    {
            List<PlayerController> playerList =  GameManager.instance.players;
        for (int playerlistNum = 0; playerlistNum < playerList.Count; playerlistNum++)
        {
            if (objectThatDidDmg.gameObject == playerList[playerlistNum].pawn.gameObject)
            {
                //Debug.Log("dwqfevdf");
               PlayerController playerController = playerList[playerlistNum];
                playerController.addToScore(objectThatDidDmg.theScoreWorthAmount);
            }
        }
    }

    private void checkifAi(GameObject attackedObject)
    {
            List<AiController> aiList =  GameManager.instance.ai;
        for (int aiListNum = 0; aiListNum < aiList.Count; aiListNum++)
        {
            if (attackedObject == aiList[aiListNum].pawn.gameObject)
            {
                aiList.Remove(aiList[aiListNum]);
            }
        }
    }
}
