using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wizard_Selector : MonoBehaviour
{
    // Sprites
    [SerializeField] private GameObject iceWizard = null;
    [SerializeField] private GameObject fireWizard = null;
    [SerializeField] private GameObject lighteningWizard = null;

    // Buttons
    [SerializeField] private GameObject iceWizardButton = null;
    [SerializeField] private GameObject fireWizardButton = null;
    [SerializeField] private GameObject lighteningWizardButton = null;

	// Chosen Wizards
	public static bool iceChosen = false;
	public static bool fireChosen = false;
	public static bool lighteningChosen = false;

	// Start is called before the first frame update
	void Start()
    {
	    if (iceChosen)
	    {
		    iceWizard.SetActive(false);
		    iceWizardButton.SetActive(false);
        }

	    if (fireChosen) {
		    fireWizard.SetActive(false);
		    fireWizardButton.SetActive(false);
	    }

	    if (lighteningChosen) {
		    lighteningWizard.SetActive(false);
		    lighteningWizardButton.SetActive(false);
	    }
    }

	private void Update() 
	{
		if (iceChosen) {
			iceWizard.SetActive(false);
			iceWizardButton.SetActive(false);
		}

		if (fireChosen) {
			fireWizard.SetActive(false);
			fireWizardButton.SetActive(false);
		}

		if (lighteningChosen) {
			lighteningWizard.SetActive(false);
			lighteningWizardButton.SetActive(false);
		}
	}


	public void OnIceButtonPressed()
    {
	    iceChosen = true;
		SceneManager.LoadScene(BattleSystem.nextSceneAfterLeavingChoice);
    }

    public void OnFireButtonPressed() 
    {
	    fireChosen = true;
	    SceneManager.LoadScene(BattleSystem.nextSceneAfterLeavingChoice);
	}

    public void OnLighteningButtonPressed()
    {
	    lighteningChosen = true;
	    SceneManager.LoadScene(BattleSystem.nextSceneAfterLeavingChoice);
	}
}
