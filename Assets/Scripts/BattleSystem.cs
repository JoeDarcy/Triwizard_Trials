using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState
{
    START, 
    ICETURN,
    FIRETURN,
    LIGHTENINGTURN,
    ENEMYTURN,
    WON,
    LOST
}



public class BattleSystem : MonoBehaviour
{
    // Triwizards
    public GameObject icePrefab = null;
    public GameObject firePrefab = null;
    public GameObject lighteningPrefab = null;
    // Triwizard instances
    public static GameObject iceWizardInstance = null;
    public static GameObject fireWizardInstance = null;
    public static GameObject lighteningWizardInstance = null;
    // Dead Triwizard instances
    [SerializeField] private GameObject iceWizardDead = null;
    [SerializeField] private GameObject fireWizardDead = null;
    [SerializeField] private GameObject lighteningWizardDead = null;
	// Enemy
	public GameObject enemyPrefab = null;
    // Enemy instance
    public static GameObject enemyInstance = null;

    public Transform iceSpawnPoint;
    public Transform fireSpawnPoint;
    public Transform lighteningSpawnPoint;
    public Transform enemySpawnPoint;

    // Ice attack and heal buttons
    [SerializeField] private GameObject iceAttackButton = null;
    [SerializeField] private GameObject iceHealButton = null;
    // Fire attack and heal buttons
    [SerializeField] private GameObject fireAttackButton = null;
    [SerializeField] private GameObject fireHealButton = null;
    // Lightening attack and heal buttons
    [SerializeField] private GameObject lighteningAttackButton = null;
    [SerializeField] private GameObject lighteningHealButton = null;

    // All attack and all heal buttons
    [SerializeField] private GameObject allAttackButton = null;
    [SerializeField] private GameObject allHealButton = null;

	// Attack effects
	[SerializeField] private GameObject iceAttack = null;
	[SerializeField] private GameObject fireAttack = null;
	[SerializeField] private GameObject lighteningAttack = null;

	// Sound effects
	[SerializeField] private GameObject iceAttackSound = null;
	[SerializeField] private GameObject fireAttackSound = null;
	[SerializeField] private GameObject lighteningAttackSound = null;
	[SerializeField] private GameObject healSound = null;

	// Enemy attack effects
	[SerializeField] private GameObject topAttack = null;
	[SerializeField] private GameObject middleAttack = null;
	[SerializeField] private GameObject bottomAttack = null;

	// Almighty Beast attacks effects
	[SerializeField] private GameObject iceAttackAlmighty = null;
	[SerializeField] private GameObject fireAttackAlmighty = null;
	[SerializeField] private GameObject lighteningAttackAlmighty = null;

	// Heal effect
	[SerializeField] private GameObject healEffect = null;

	public Text dialogueText;

	// HUDs
    public BattleHUD iceWizardHUD;
    public BattleHUD fireWizardHUD;
    public BattleHUD lighteningWizardHUD;
	public BattleHUD enemyHUD;

	// HUD elements for deactivating
	[SerializeField] private GameObject iceHUD = null;
	[SerializeField] private GameObject fireHUD = null;
	[SerializeField] private GameObject lighteningHUD = null;

	public static bool isIceWizardDead = false;
    public static bool isFireWizardDead = false;
    public static bool isLighteningWizardDead = false;

	public static bool isEnemyDead = false;
	private bool endSceneCalled = false;

    Unit iceUnit;
    Unit fireUnit;
    Unit lighteningUnit;
    Unit enemyUnit;

    public static BattleState state;

	// Next scene 
	[SerializeField] private int nextScene = 0;
	public static int nextSceneAfterLeavingChoice = 2;


	// Start is called before the first frame update
	private void Start()
    {
	    iceAttackButton.SetActive(false);
	    iceHealButton.SetActive(false);
	    fireAttackButton.SetActive(false);
        fireHealButton.SetActive(false);
        lighteningAttackButton.SetActive(false);
        lighteningHealButton.SetActive(false);
        allAttackButton.SetActive(false);
		allHealButton.SetActive(false);

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        // Instantiate triwizards
        // Ice
        if (!isIceWizardDead && !Wizard_Selector.iceChosen)
        {
	        iceWizardInstance = Instantiate(icePrefab, iceSpawnPoint);
	        iceUnit = iceWizardInstance.GetComponent<Unit>();
	        iceWizardHUD.SetHUD(iceUnit);
		}
        // Fire
        if (!isFireWizardDead && !Wizard_Selector.fireChosen)
        {
	        fireWizardInstance = Instantiate(firePrefab, fireSpawnPoint);
	        fireUnit = fireWizardInstance.GetComponent<Unit>();
	        fireWizardHUD.SetHUD(fireUnit);
		}
        // Lightening
        if (!isLighteningWizardDead && !Wizard_Selector.lighteningChosen)
        {
	        lighteningWizardInstance = Instantiate(lighteningPrefab, lighteningSpawnPoint);
	        lighteningUnit = lighteningWizardInstance.GetComponent<Unit>();
	        lighteningWizardHUD.SetHUD(lighteningUnit);
		}

        // Instantiate enemy
        enemyInstance = Instantiate(enemyPrefab, enemySpawnPoint);
	    enemyUnit = enemyInstance.GetComponent<Unit>();
	    enemyHUD.SetHUD(enemyUnit);
		dialogueText.text = "A raging " + enemyUnit.unitName + " approaches...";
		
	    yield return new WaitForSeconds(2.0f);

	    if (!isIceWizardDead && !Wizard_Selector.iceChosen)
	    {
		    state = BattleState.ICETURN;
		    IceTurn();
		}
	    else if (!isFireWizardDead && !Wizard_Selector.fireChosen)
	    {
		    state = BattleState.FIRETURN;
		    FireTurn();
		} else if (!isLighteningWizardDead && !Wizard_Selector.lighteningChosen) {
		    state = BattleState.LIGHTENINGTURN;
		    LighteningTurn();
	    }
	}

    private void IceTurn() {
	    // Enable buttons until choice has been made
	    iceAttackButton.SetActive(true);
	    iceHealButton.SetActive(true);
	    allAttackButton.SetActive(true);
	    allHealButton.SetActive(true);

		dialogueText.text = " Ice Wizard, choose your action: ";
    }

    void FireTurn() {
	    // Enable buttons until choice has been made
	    fireAttackButton.SetActive(true);
	    fireHealButton.SetActive(true);
	    allAttackButton.SetActive(true);
	    allHealButton.SetActive(true);

		dialogueText.text = " Fire Wizard, choose your action: ";
    }

    private void LighteningTurn() {
	    // Enable buttons until choice has been made
	    lighteningAttackButton.SetActive(true);
	    lighteningHealButton.SetActive(true);
	    allAttackButton.SetActive(true);
	    allHealButton.SetActive(true);

		dialogueText.text = " Lightening Wizard, choose your action: ";
    }

    private IEnumerator EnemyTurn()
    {
        // Enemy logic to attack or heal (random chance)
        dialogueText.text = enemyUnit.unitName + " is attacking!";

        yield return new WaitForSeconds(1.0f);

        // Damage the player
        if (!isIceWizardDead && !Wizard_Selector.iceChosen)
        {
	        Instantiate(topAttack, new Vector3(enemyInstance.transform.position.x - 1, enemyInstance.transform.position.y, enemyInstance.transform.position.z), Quaternion.identity);

			// Chose sound effect based on enemy type
			if (enemyInstance.CompareTag("Ice_Enemy"))
			{
				// Spawn attack sound effect
				Instantiate(iceAttackSound, transform.position, Quaternion.identity);
			}
			if (enemyInstance.CompareTag("Fire_Enemy"))
			{
				// Spawn attack sound effect
				Instantiate(fireAttackSound, transform.position, Quaternion.identity);
			}
			if (enemyInstance.CompareTag("Lightening_Enemy")) {
				// Spawn attack sound effect
				Instantiate(lighteningAttackSound, transform.position, Quaternion.identity);
			}

			// For the Almighty Beast
			if (enemyInstance.CompareTag("Almighty_Enemy")) {

				// Spawn all attack effects
				Instantiate(iceAttackAlmighty, new Vector3(enemyInstance.transform.position.x - 1, enemyInstance.transform.position.y, enemyInstance.transform.position.z), Quaternion.identity);
				Instantiate(fireAttackAlmighty, new Vector3(enemyInstance.transform.position.x - 1, enemyInstance.transform.position.y, enemyInstance.transform.position.z), Quaternion.identity);
				Instantiate(lighteningAttackAlmighty, new Vector3(enemyInstance.transform.position.x - 1, enemyInstance.transform.position.y, enemyInstance.transform.position.z), Quaternion.identity);
				// Spawn all attack sound effect
				Instantiate(iceAttackSound, transform.position, Quaternion.identity);
				Instantiate(fireAttackSound, transform.position, Quaternion.identity);
				Instantiate(lighteningAttackSound, transform.position, Quaternion.identity);
			}


			yield return new WaitForSeconds(2.0f);
			isIceWizardDead = iceUnit.IceTakeDamage(enemyUnit.enemyDamage);

	        if (isIceWizardDead)
	        {
				iceHUD.SetActive(false);
		        iceWizardInstance.SetActive(false);
				iceWizardDead.SetActive(true);
	        }

	        iceWizardHUD.SetHP(iceUnit.currentHP);
	        dialogueText.text = "The " + enemyUnit.unitName + " attack hit the Ice Wizard for " + iceUnit.iceDamageTaken + " damage.";
	        yield return new WaitForSeconds(1.5f);
        }

        if (!isFireWizardDead && !Wizard_Selector.fireChosen)
        {
	        Instantiate(middleAttack, new Vector3(enemyInstance.transform.position.x - 1, enemyInstance.transform.position.y, enemyInstance.transform.position.z), Quaternion.identity);

			// Chose sound effect based on enemy type
			if (enemyInstance.CompareTag("Ice_Enemy")) {
				// Spawn attack sound effect
				Instantiate(iceAttackSound, transform.position, Quaternion.identity);
			}
			if (enemyInstance.CompareTag("Fire_Enemy")) {
				// Spawn attack sound effect
				Instantiate(fireAttackSound, transform.position, Quaternion.identity);
			}
			if (enemyInstance.CompareTag("Lightening_Enemy")) {
				// Spawn attack sound effect
				Instantiate(lighteningAttackSound, transform.position, Quaternion.identity);
			}

			// For the Almighty Beast
			if (enemyInstance.CompareTag("Almighty_Enemy")) {

				// Spawn all attack effects
				Instantiate(iceAttackAlmighty, new Vector3(enemyInstance.transform.position.x - 1, enemyInstance.transform.position.y, enemyInstance.transform.position.z), Quaternion.identity);
				Instantiate(fireAttackAlmighty, new Vector3(enemyInstance.transform.position.x - 1, enemyInstance.transform.position.y, enemyInstance.transform.position.z), Quaternion.identity);
				Instantiate(lighteningAttackAlmighty, new Vector3(enemyInstance.transform.position.x - 1, enemyInstance.transform.position.y, enemyInstance.transform.position.z), Quaternion.identity);
				// Spawn all attack sound effect
				Instantiate(iceAttackSound, transform.position, Quaternion.identity);
				Instantiate(fireAttackSound, transform.position, Quaternion.identity);
				Instantiate(lighteningAttackSound, transform.position, Quaternion.identity);
			}

			yield return new WaitForSeconds(2.0f);
			isFireWizardDead = fireUnit.FireTakeDamage(enemyUnit.enemyDamage);

	        if (isFireWizardDead) 
	        {
		        fireHUD.SetActive(false);
				fireWizardInstance.SetActive(false);
		        fireWizardDead.SetActive(true);
	        }

			fireWizardHUD.SetHP(fireUnit.currentHP);
			dialogueText.text = "The " + enemyUnit.unitName + " attack hit you Fire Wizard for " + fireUnit.fireDamageTaken + " damage.";
			yield return new WaitForSeconds(1.5f);
		}
        
		if (!isLighteningWizardDead && !Wizard_Selector.lighteningChosen) 
		{
			Instantiate(bottomAttack, new Vector3(enemyInstance.transform.position.x - 1, enemyInstance.transform.position.y, enemyInstance.transform.position.z), Quaternion.identity);

			// Chose sound effect based on enemy type
			if (enemyInstance.CompareTag("Ice_Enemy")) {
				// Spawn attack sound effect
				Instantiate(iceAttackSound, transform.position, Quaternion.identity);
			}
			if (enemyInstance.CompareTag("Fire_Enemy")) {
				// Spawn attack sound effect
				Instantiate(fireAttackSound, transform.position, Quaternion.identity);
			}
			if (enemyInstance.CompareTag("Lightening_Enemy")) {
				// Spawn attack sound effect
				Instantiate(lighteningAttackSound, transform.position, Quaternion.identity);
			}

			// For the Almighty Beast
			if (enemyInstance.CompareTag("Almighty_Enemy")) {

				// Spawn all attack effects
				Instantiate(iceAttackAlmighty, new Vector3(enemyInstance.transform.position.x - 1, enemyInstance.transform.position.y, enemyInstance.transform.position.z), Quaternion.identity);
				Instantiate(fireAttackAlmighty, new Vector3(enemyInstance.transform.position.x - 1, enemyInstance.transform.position.y, enemyInstance.transform.position.z), Quaternion.identity);
				Instantiate(lighteningAttackAlmighty, new Vector3(enemyInstance.transform.position.x - 1, enemyInstance.transform.position.y, enemyInstance.transform.position.z), Quaternion.identity);
				// Spawn all attack sound effect
				Instantiate(iceAttackSound, transform.position, Quaternion.identity);
				Instantiate(fireAttackSound, transform.position, Quaternion.identity);
				Instantiate(lighteningAttackSound, transform.position, Quaternion.identity);
			}

			yield return new WaitForSeconds(2.0f);
			isLighteningWizardDead = lighteningUnit.LighteningTakeDamage(enemyUnit.enemyDamage);

			if (isLighteningWizardDead) 
			{
				lighteningHUD.SetActive(false);
				lighteningWizardInstance.SetActive(false);
				lighteningWizardDead.SetActive(true);
			}

			lighteningWizardHUD.SetHP(lighteningUnit.currentHP);
			dialogueText.text = "The " + enemyUnit.unitName + " attack hit you Lightening Wizard for " + lighteningUnit.lighteningDamageTaken + " damage.";
			yield return new WaitForSeconds(1.5f);
		}
		

		// Check if all the wizards are dead
        if ((isIceWizardDead || Wizard_Selector.iceChosen) && (isFireWizardDead || Wizard_Selector.fireChosen) && (isLighteningWizardDead || Wizard_Selector.lighteningChosen)) {
	        // End the battle
	        state = BattleState.LOST;
	        EndBattle();
        } 
        else if (!isIceWizardDead && !Wizard_Selector.iceChosen)
        {
            // Change state to Ice Wizard turn
            state = BattleState.ICETURN;
	        IceTurn();
        } 
        else if (!isFireWizardDead && !Wizard_Selector.fireChosen) {
	        // Change state to Fire Wizard turn
	        state = BattleState.FIRETURN;
	        FireTurn();
        }
        else if (!isLighteningWizardDead && !Wizard_Selector.lighteningChosen) {
	        // Change state to Lightening Wizard turn
	        state = BattleState.LIGHTENINGTURN;
	        LighteningTurn();
        }
	}

    private void EndBattle()
    {
		// Only call this function once per round
		endSceneCalled = true;

	    if (state == BattleState.WON)
	    {
		    dialogueText.text = "You have won the battle!";
		    nextSceneAfterLeavingChoice += 1;

			// Skip leaving wizard behind scene if boss fight is next
			Scene activeScene = SceneManager.GetActiveScene();
			if (activeScene.name == "04_Fight_3")
			{
				SceneManager.LoadScene(5);
			}
			else
			{
				if ((isIceWizardDead && !Wizard_Selector.iceChosen) || (isFireWizardDead && !Wizard_Selector.fireChosen) || (isLighteningWizardDead && !Wizard_Selector.lighteningChosen)) 
				{
					SceneManager.LoadScene(nextScene + 1);
				} else {
					SceneManager.LoadScene(nextScene);
				}
			}
	    }
	    else if (state == BattleState.LOST)
	    {
		    dialogueText.text = "You have lost the battle!";
		    SceneManager.LoadScene(2);
		}
    }

    // Attacks
	IEnumerator IceAttack() {
		// Spawn attack effect
		Instantiate(iceAttack, iceWizardInstance.transform.position, Quaternion.identity);
		// Spawn attack sound effect
		Instantiate(iceAttackSound, iceWizardInstance.transform.position, Quaternion.identity);
		// Attack damage delay
		yield return new WaitForSeconds(2.0f);
		// Damage the enemy
		bool isEnemyDead = enemyUnit.EnemyTakeDamage(iceUnit.iceDamage);

	    // Update enemy HP on HUD
	    enemyHUD.SetHP(enemyUnit.currentHP);
	    dialogueText.text = "The ice attack did " + enemyUnit.enemyDamageTaken + " damage.";

	    yield return new WaitForSeconds(2.0f);

	    // Check if the enemy is dead
	    if (isEnemyDead && !endSceneCalled) {
		    // End the battle
		    state = BattleState.WON;
		    EndBattle();
	    } 
	    else if (!isFireWizardDead && !Wizard_Selector.fireChosen)
	    {
		    // Change state to Fire Wizard turn
		    state = BattleState.FIRETURN;
		    FireTurn();
	    }
	    else if (!isLighteningWizardDead && !Wizard_Selector.lighteningChosen)
	    {
		    // Change state to Lightening Wizard turn
		    state = BattleState.LIGHTENINGTURN;
		    LighteningTurn();
		}
	    else
	    {
		    // Change state to enemy turn
		    state = BattleState.ENEMYTURN;
		    StartCoroutine(EnemyTurn());
		}
    }

    IEnumerator FireAttack() {
	    // Spawn attack effect
	    Instantiate(fireAttack, fireWizardInstance.transform.position, Quaternion.identity);
	    // Spawn attack sound effect
	    Instantiate(fireAttackSound, fireWizardInstance.transform.position, Quaternion.identity);
		// Attack damage delay
		yield return new WaitForSeconds(2.0f);
		// Damage the enemy
		isEnemyDead = enemyUnit.EnemyTakeDamage(fireUnit.fireDamage);

	    // Update enemy HP on HUD
	    enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The fire attack did " + enemyUnit.enemyDamageTaken + " damage.";

		yield return new WaitForSeconds(2.0f);

	    // Check if the enemy is dead
	    if (isEnemyDead && !endSceneCalled) {
		    // End the battle
		    state = BattleState.WON;
		    EndBattle();
	    } 
	    else if (!isLighteningWizardDead && !Wizard_Selector.lighteningChosen)
	    {
		    // Change state to Lightening Wizard turn
		    state = BattleState.LIGHTENINGTURN;
		    LighteningTurn();
	    } 
	    else 
	    {
		    // Change state to enemy turn
		    state = BattleState.ENEMYTURN;
		    StartCoroutine(EnemyTurn());
	    }
	}

    private IEnumerator LighteningAttack() {
	    // Spawn attack effect
	    Instantiate(lighteningAttack, new Vector3(transform.position.x - 12, transform.position.y - 2, transform.position.z), Quaternion.identity);
		// Spawn attack sound effect
		Instantiate(lighteningAttackSound, lighteningWizardInstance.transform.position, Quaternion.identity);
		// Attack damage delay
		yield return new WaitForSeconds(2.0f);
		// Damage the enemy
		bool isEnemyDead = enemyUnit.EnemyTakeDamage(lighteningUnit.lighteningDamage);

	    // Update enemy HP on HUD
	    enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The lightening attack did " + enemyUnit.enemyDamageTaken + " damage.";

		yield return new WaitForSeconds(2.0f);

	    // Check if the enemy is dead
	    if (isEnemyDead && !endSceneCalled) {
		    // End the battle
		    state = BattleState.WON;
		    EndBattle();
	    } else {
		    // Change state to enemy turn
		    state = BattleState.ENEMYTURN;
		    StartCoroutine(EnemyTurn());
	    }
    }

	// Healing
    private IEnumerator IceHeal()
    {
		// Spawn heal effect
		Instantiate(healEffect, iceWizardInstance.transform.position, Quaternion.identity);
		// Spawn heal sound effect
		Instantiate(healSound, transform.position, Quaternion.identity);

		iceUnit.Heal(10);

	    iceWizardHUD.SetHP(iceUnit.currentHP);
		dialogueText.text = "You have healed yourself by " + iceUnit.healAmount + " points.";

		yield return new WaitForSeconds(2.0f);

	    // Change state to Fire Wizard turn
	    if (!isFireWizardDead && !Wizard_Selector.fireChosen)
	    {
		    state = BattleState.FIRETURN;
		    FireTurn();
		}
		else if (!isLighteningWizardDead && !Wizard_Selector.lighteningChosen)
	    {
		    // Change state to Lightening Wizard turn
		    state = BattleState.LIGHTENINGTURN;
		    LighteningTurn();
		} 
	    else 
	    {
		    // Change state to enemy turn
		    state = BattleState.ENEMYTURN;
		    StartCoroutine(EnemyTurn());
	    }
	}

    private IEnumerator FireHeal() {
	    // Spawn heal effect
	    Instantiate(healEffect, fireWizardInstance.transform.position, Quaternion.identity);
	    // Spawn heal sound effect
	    Instantiate(healSound, transform.position, Quaternion.identity);
		fireUnit.Heal(10);

	    fireWizardHUD.SetHP(fireUnit.currentHP);
		dialogueText.text = "You have healed yourself by " + fireUnit.healAmount + " points.";

		yield return new WaitForSeconds(2.0f);

	    // Change state to Lightening Wizard turn
	    if (!isLighteningWizardDead && !Wizard_Selector.lighteningChosen)
	    {
		    state = BattleState.LIGHTENINGTURN;
		    LighteningTurn();
		} 
	    else
	    {
		    // Change state to enemy turn
		    state = BattleState.ENEMYTURN;
		    StartCoroutine(EnemyTurn());
	    }
	}

    private IEnumerator LighteningHeal() {
	    // Spawn heal effect
	    Instantiate(healEffect, lighteningWizardInstance.transform.position, Quaternion.identity);
	    // Spawn heal sound effect
	    Instantiate(healSound, transform.position, Quaternion.identity);
		lighteningUnit.Heal(10);

	    lighteningWizardHUD.SetHP(lighteningUnit.currentHP);
	    dialogueText.text = "You have healed yourself by " + lighteningUnit.healAmount + " points.";

	    yield return new WaitForSeconds(2.0f);

	    // Change state to enemy turn
	    state = BattleState.ENEMYTURN;
	    StartCoroutine(EnemyTurn());
    }

	// Attack buttons
    public void OnIceAttackButton()
    {
        // Disable buttons until next turn
	    iceAttackButton.SetActive(false);
	    iceHealButton.SetActive(false);
	    allAttackButton.SetActive(false);
	    allHealButton.SetActive(false);

		if (state != BattleState.ICETURN)
	    {
            return;
	    }

        StartCoroutine(IceAttack());
    }

    public void OnFireAttackButton() {
	    // Disable buttons until next turn
	    fireAttackButton.SetActive(false);
	    fireHealButton.SetActive(false);
	    allAttackButton.SetActive(false);
	    allHealButton.SetActive(false);

		if (state != BattleState.FIRETURN) {
		    return;
	    }

	    StartCoroutine(FireAttack());
    }

    public void OnLighteningAttackButton() {
	    // Disable buttons until next turn
	    lighteningAttackButton.SetActive(false);
	    lighteningHealButton.SetActive(false);
	    allAttackButton.SetActive(false);
	    allHealButton.SetActive(false);

		if (state != BattleState.LIGHTENINGTURN) {
		    return;
	    }

	    StartCoroutine(LighteningAttack());
    }

	// Healing Buttons
    public void OnIceHealButton()
    {
	    // Disable buttons until next turn
	    iceAttackButton.SetActive(false);
	    iceHealButton.SetActive(false);
	    allAttackButton.SetActive(false);
	    allHealButton.SetActive(false);

		if (state != BattleState.ICETURN)
	    {
            return;
	    }
	    else
	    {
		    StartCoroutine(IceHeal());
        }
    }

    public void OnFireHealButton() {
	    // Disable buttons until next turn
	    fireAttackButton.SetActive(false);
	    fireHealButton.SetActive(false);
	    allAttackButton.SetActive(false);
	    allHealButton.SetActive(false);

		if (state != BattleState.FIRETURN) {
		    return;
	    } else {
		    StartCoroutine(FireHeal());
	    }
    }

    public void OnLighteningHealButton() {
	    // Disable buttons until next turn
	    lighteningAttackButton.SetActive(false);
	    lighteningHealButton.SetActive(false);
	    allAttackButton.SetActive(false);
	    allHealButton.SetActive(false);

		if (state != BattleState.LIGHTENINGTURN) {
		    return;
	    } else {
		    StartCoroutine(LighteningHeal());
	    }
    }

    public void OnAllHealButtonPressed()
    {
		// Disable buttons until next turn
		iceAttackButton.SetActive(false);
		iceHealButton.SetActive(false);
		fireAttackButton.SetActive(false);
		fireHealButton.SetActive(false);
		lighteningAttackButton.SetActive(false);
		lighteningHealButton.SetActive(false);
		// All attack and all heal buttons to false too
		allAttackButton.SetActive(false);
		allHealButton.SetActive(false);


		if (!isIceWizardDead && !Wizard_Selector.iceChosen)
	    {
		    StartCoroutine(IceHeal());
		}

		if (!isFireWizardDead && !Wizard_Selector.fireChosen)
		{
			StartCoroutine(FireHeal());
		}

		if (!isLighteningWizardDead && !Wizard_Selector.lighteningChosen) 
		{
			StartCoroutine(LighteningHeal());
		}
    }

    public void OnAllAttackButtonPressed() 
    {
	    // Disable buttons until next turn
	    iceAttackButton.SetActive(false);
	    iceHealButton.SetActive(false);
	    fireAttackButton.SetActive(false);
	    fireHealButton.SetActive(false);
	    lighteningAttackButton.SetActive(false);
	    lighteningHealButton.SetActive(false);
		// All attack and all heal buttons to false too
		allAttackButton.SetActive(false);
		allHealButton.SetActive(false);

		if (!isIceWizardDead && !Wizard_Selector.iceChosen) {
		    StartCoroutine(IceAttack());
	    }

	    if (!isFireWizardDead && !Wizard_Selector.fireChosen) {
		    StartCoroutine(FireAttack());
	    }

	    if (!isLighteningWizardDead && !Wizard_Selector.lighteningChosen) {
		    StartCoroutine(LighteningAttack());
	    }
	}
}
