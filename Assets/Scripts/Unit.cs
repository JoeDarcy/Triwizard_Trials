using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int iceDamage;
    public int fireDamage;
    public int lighteningDamage;
    public int enemyDamage;
    public int enemyDamageTaken;
    public int normalDamage;
    public int boostDamage;
    public int iceDamageTaken;
    public int fireDamageTaken;
    public int lighteningDamageTaken;
    public int healAmount = 0;
	public int maxHP;
    public int currentHP;

	private void Start() {
        // Set Ice Wizard damage
		if (gameObject.CompareTag("Ice_Wizard") && BattleSystem.enemyInstance.CompareTag("Ice_Enemy"))
		{
			iceDamage = normalDamage;
		}
		else 
		{
			iceDamage = boostDamage;
		}

        // Set Fire Wizard damage
        if (gameObject.CompareTag("Fire_Wizard") && BattleSystem.enemyInstance.CompareTag("Fire_Enemy")) 
        {
	        fireDamage = normalDamage;
        } 
        else 
        {
	        fireDamage = boostDamage;
        }

        // Set Lightening Wizard damage
        if (gameObject.CompareTag("Lightening_Wizard") && BattleSystem.enemyInstance.CompareTag("Lightening_Enemy")) 
        {
	        lighteningDamage = normalDamage;
        } 
        else 
        {
	        lighteningDamage = boostDamage;
        }
	}

	public bool IceTakeDamage(int dmg)
    {
		// Reduce enemy damage if of same type
	    if (BattleSystem.enemyInstance.CompareTag("Ice_Enemy"))
	    {
		    dmg /= 2;
	    }
	    // Randomise damage taken within range
		if (dmg == 5)
	    {
		    iceDamageTaken = Random.Range(1, dmg);
		}
		else 
		{
			iceDamageTaken = Random.Range(5, dmg);
		}

		currentHP -= iceDamageTaken;

        if (currentHP <= 0)
        {
	        return true;
        }

		return false;

	}

	public bool FireTakeDamage(int dmg) 
	{
		// Reduce enemy damage if of same type
		if (BattleSystem.enemyInstance.CompareTag("Fire_Enemy")) {
			dmg /= 2;
		}
		// Randomise damage taken within range
		if (dmg == 5) 
		{
			fireDamageTaken = Random.Range(1, dmg);
		}
		else
		{
			fireDamageTaken = Random.Range(5, dmg);
		}
		

		// Apply damage
		currentHP -= fireDamageTaken;

		if (currentHP <= 0) 
		{
			return true;
		}

		return false;
	}

	public bool LighteningTakeDamage(int dmg) 
	{
		// Reduce enemy damage if of same type
		if (BattleSystem.enemyInstance.CompareTag("Lightening_Enemy")) {
			dmg /= 2;
		}
		// Randomise damage taken within range
		if (dmg == 5) {
			lighteningDamageTaken = Random.Range(1, dmg);
		}
		else
		{
			lighteningDamageTaken = Random.Range(5, dmg);
		}

		// Apply damage
		currentHP -= lighteningDamageTaken;

		if (currentHP <= 0) 
		{
			return true;
		}

		return false;
		
	}

	public bool EnemyTakeDamage(int dmg)
	{
		// Randomise damage taken within range
		if (dmg == 5) 
		{
			enemyDamageTaken = Random.Range(1, dmg);
		}
		else
		{
			enemyDamageTaken = Random.Range(5, dmg);
		}

		// Apply damage
		currentHP -= enemyDamageTaken;

		if (currentHP <= 0) 
		{
			return true;
		} 
		
		return false;
		
	}

	public void Heal(int amount)
    {
		healAmount = Random.Range(1, amount);
		currentHP += healAmount;

        if (currentHP > maxHP)
        {
	        currentHP = maxHP;
        }
    }
}
