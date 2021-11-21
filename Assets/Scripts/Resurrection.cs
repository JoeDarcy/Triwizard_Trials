using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resurrection : MonoBehaviour
{
    // Alive sprites
	[SerializeField] private GameObject iceAlive = null;
	[SerializeField] private GameObject fireAlive = null;
	[SerializeField] private GameObject lighteningAlive = null;
    // Dead sprites
	[SerializeField] private GameObject iceDead = null;
	[SerializeField] private GameObject fireDead = null;
	[SerializeField] private GameObject lighteningDead = null;
	// Resurrection Buttons
	[SerializeField] private GameObject iceResurrection = null;
	[SerializeField] private GameObject fireResurrection = null;
	[SerializeField] private GameObject lighteningResurrection = null;

	// Scene to load after resurrection
	[SerializeField] private int sceneToLoadAfterResurrection = 0;

	// Start is called before the first frame update
	void Start()
    {
		// Ice
	    if (BattleSystem.isIceWizardDead && !Wizard_Selector.iceChosen)
	    {
		    iceAlive.SetActive(false);
		    iceDead.SetActive(true);
	    }
	    else if (!BattleSystem.isIceWizardDead && !Wizard_Selector.iceChosen)
	    {
		    iceAlive.SetActive(true);
		    iceDead.SetActive(false);
		}
	    // Hide Wizard if chosen to stay behind 
	    if (Wizard_Selector.iceChosen)
	    {
		    iceAlive.SetActive(false);
		    iceDead.SetActive(false);
			iceResurrection.SetActive(false);
		}

	    // Fire
	    if (BattleSystem.isFireWizardDead && !Wizard_Selector.fireChosen) 
	    {
		    fireAlive.SetActive(false);
		    fireDead.SetActive(true);
	    } 
	    else if (!BattleSystem.isFireWizardDead && !Wizard_Selector.fireChosen)
	    {
		    fireAlive.SetActive(true);
		    fireDead.SetActive(false);
	    }
	    // Hide Wizard if chosen to stay behind 
	    if (Wizard_Selector.fireChosen) {
		    fireAlive.SetActive(false);
		    fireDead.SetActive(false);
		    fireResurrection.SetActive(false);
	    }

		// Lightening
		if (BattleSystem.isLighteningWizardDead && !Wizard_Selector.lighteningChosen) 
	    {
		    lighteningAlive.SetActive(false);
			lighteningDead.SetActive(true);
	    } 
	    else if (!BattleSystem.isLighteningWizardDead && !Wizard_Selector.lighteningChosen)
	    {
			lighteningAlive.SetActive(true);
		    lighteningDead.SetActive(false);
	    }
		// Hide Wizard if chosen to stay behind 
		if (Wizard_Selector.lighteningChosen) {
			lighteningAlive.SetActive(false);
			lighteningDead.SetActive(false);
			lighteningResurrection.SetActive(false);
		}
	}

	private void Update() {
		// All alive and none chosen
		if ((!BattleSystem.isIceWizardDead || !Wizard_Selector.iceChosen) && (!BattleSystem.isFireWizardDead || !Wizard_Selector.fireChosen) && (!BattleSystem.isLighteningWizardDead || !Wizard_Selector.lighteningChosen))
		{
			SceneManager.LoadScene(sceneToLoadAfterResurrection);
		}
		// Ice chosen
		if (Wizard_Selector.iceChosen && (!BattleSystem.isFireWizardDead || !Wizard_Selector.fireChosen) && (!BattleSystem.isLighteningWizardDead || !Wizard_Selector.lighteningChosen)) {
			SceneManager.LoadScene(sceneToLoadAfterResurrection);
		}
		// Fire chosen
		if ((!BattleSystem.isIceWizardDead || !Wizard_Selector.iceChosen) && Wizard_Selector.fireChosen && (!BattleSystem.isLighteningWizardDead || !Wizard_Selector.lighteningChosen)) {
			SceneManager.LoadScene(sceneToLoadAfterResurrection);
		}
		// Lightening Chosen
		if ((!BattleSystem.isIceWizardDead || !Wizard_Selector.iceChosen) && (!BattleSystem.isFireWizardDead || !Wizard_Selector.fireChosen) && Wizard_Selector.lighteningChosen) {
			SceneManager.LoadScene(sceneToLoadAfterResurrection);
		}
	}

	public void OnIceResurrectPressed()
    {
	    iceAlive.SetActive(true);
	    iceDead.SetActive(false);
		BattleSystem.isIceWizardDead = false;
	}

    public void OnFireResurrectPressed() 
    {
	    fireAlive.SetActive(true);
	    fireDead.SetActive(false);
	    BattleSystem.isFireWizardDead = false;
	}

    public void OnLighteningResurrectPressed() 
    {
	    lighteningAlive.SetActive(true);
	    lighteningDead.SetActive(false);
	    BattleSystem.isLighteningWizardDead = false;
	}
}
