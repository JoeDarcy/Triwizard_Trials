using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipLeaveScene : MonoBehaviour
{
	[SerializeField] private int numberOfWizards = 3;

    // Start is called before the first frame update
    void Start()
    {
        // If Ice Wizard is dead or chosen
	    if (BattleSystem.isIceWizardDead || Wizard_Selector.iceChosen)
	    {
		    numberOfWizards -= 1;
	    }

	    // If Fire Wizard is dead or chosen
	    if (BattleSystem.isFireWizardDead || Wizard_Selector.fireChosen) {
		    numberOfWizards -= 1;
	    }

	    // If lightening Wizard is dead or chosen
	    if (BattleSystem.isLighteningWizardDead || Wizard_Selector.lighteningChosen) {
		    numberOfWizards -= 1;
	    }
	}

    // Update is called once per frame
    void Update()
    {
	    if (numberOfWizards < 2)
	    {
		    SceneManager.LoadScene(BattleSystem.nextSceneAfterLeavingChoice);
	    }
    }
}
