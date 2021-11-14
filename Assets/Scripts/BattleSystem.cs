using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{
    START, 
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}



public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab = null;
    public GameObject enemyPrefab = null;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    [SerializeField] private GameObject attackButton = null;
    [SerializeField] private GameObject healButton = null;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleState state;


    // Start is called before the first frame update
    void Start()
    {
	    state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
	    GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
	    playerUnit = playerGO.GetComponent<Unit>();

	    GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
	    enemyUnit = enemyGO.GetComponent<Unit>();

	    dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

	    playerHUD.SetHUD(playerUnit);
	    enemyHUD.SetHUD(enemyUnit);

	    yield return new WaitForSeconds(2.0f);

	    state = BattleState.PLAYERTURN;
	    PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        // Damage the enemy
        bool isEnemyDead = enemyUnit.TakeDamage(playerUnit.damage);

        // Update enemy HP on HUD
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack was successful!";

        yield return new WaitForSeconds(2.0f);

        // Check if the enemy is dead
        if (isEnemyDead == true)
        {
            // End the battle
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            // Change state to enemy turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        // Enemy logic to attack or heal (random chance)
        dialogueText.text = enemyUnit.unitName + " is attacking!";

        yield return new WaitForSeconds(1.0f);

        // Damage the player
        bool isPlayerDead = playerUnit.TakeDamage(enemyUnit.damage);

        // Update player HP on HUD
        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "The " + enemyUnit.unitName + " attack hit you!";

        yield return new WaitForSeconds(1.0f);

        // Check if the player is dead
        if (isPlayerDead == true) {
	        // End the battle
	        state = BattleState.LOST;
	        EndBattle();
        } else {
            // Change state to player turn
            state = BattleState.PLAYERTURN;
	        PlayerTurn();
        }
    }

    void EndBattle()
    {
	    if (state == BattleState.WON)
	    {
		    dialogueText.text = "You have won the battle!";
		}
	    else if (state == BattleState.LOST)
	    {
		    dialogueText.text = "You have lost the battle!";
        }
    }

    void PlayerTurn()
    {
	    // Enable buttons until choice has been made
	    attackButton.SetActive(true);
	    healButton.SetActive(true);

        dialogueText.text = "Choose an action: ";
    }

    IEnumerator PlayerHeal()
    {
	    playerUnit.Heal(5);

	    playerHUD.SetHP(playerUnit.currentHP);
	    dialogueText.text = "You have healed yourself.";

	    yield return new WaitForSeconds(2.0f);

	    // Change state to enemy turn
	    state = BattleState.ENEMYTURN;
	    StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        // Disable buttons until next turn
	    attackButton.SetActive(false);
	    healButton.SetActive(false);

	    if (state != BattleState.PLAYERTURN)
	    {
            return;
	    }

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
	    // Disable buttons until next turn
	    attackButton.SetActive(false);
	    healButton.SetActive(false);

        if (state != BattleState.PLAYERTURN)
	    {
            return;
	    }
	    else
	    {
		    StartCoroutine(PlayerHeal());
        }
    }
}
