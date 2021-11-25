using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Tracker : MonoBehaviour
{
	// Game state
	public string gameState = null;
	// Wizards
    public bool iceWizardDead = false;
    public bool iceWizardChosen = false;
    public bool fireWizardDead = false;
    public bool fireWizardChosen = false;
    public bool lighteningWizardDead = false;
    public bool lighteningWizardChosen = false;
	// Enemy
	public string enemyType = null;
	// Next scene after leaving choice scene
	public int nextSceneInGameplay = 0;
	// Next scene after resurrection
	public int nextSceneAfterResurrection = 0;

	// Game States
	/*
	START, 
	ICETURN,
	FIRETURN,
	LIGHTENINGTURN,
	ENEMYTURN,
	WON,
	LOST
	*/

	// Update is called once per frame
	void Update()
    {
		// Get game state
		if (BattleSystem.state.ToString() == "START") {
			gameState = "Game Started";
		}
		else if (BattleSystem.state.ToString() == "ENEMYTURN")
		{
			gameState = "Enemy Turn";
		} 
		else if (BattleSystem.state.ToString() == "ICETURN")
		{
			gameState = "Ice Turn";
		}
		else if (BattleSystem.state.ToString() == "FIRETURN") {
			gameState = "Fire Turn";
		} 
		else if (BattleSystem.state.ToString() == "LIGHTENINGTURN") {
			gameState = "Lightening Turn";
		} 
		else if (BattleSystem.state.ToString() == "WON") {
			gameState = "Game Won";
		} 
		else if (BattleSystem.state.ToString() == "LOST") {
			gameState = "Game Lost";
		}


		// Wizard dead and chosen states
		iceWizardDead = BattleSystem.isIceWizardDead;
	    iceWizardChosen = Wizard_Selector.iceChosen;
	    fireWizardDead = BattleSystem.isFireWizardDead;
	    fireWizardChosen = Wizard_Selector.fireChosen;
	    lighteningWizardDead = BattleSystem.isLighteningWizardDead;
		lighteningWizardChosen = Wizard_Selector.lighteningChosen;

		// Enemy type
		if (BattleSystem.enemyInstance)
		{
			enemyType = BattleSystem.enemyInstance.tag;
		}

		// Which scene is up next in gameplay
		nextSceneInGameplay = BattleSystem.nextSceneAfterLeavingChoice;

		// Next scene after resurrection scene
		nextSceneAfterResurrection = Resurrection.sceneToLoadAfterResurrection;

    }
}
