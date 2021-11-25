using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWizards : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Reset Wizards to alive
        BattleSystem.isIceWizardDead = false;
        BattleSystem.isFireWizardDead = false;
        BattleSystem.isLighteningWizardDead = false;

        // Reset Wizards chosen to stay behind bool to false
        Wizard_Selector.iceChosen = false;
        Wizard_Selector.fireChosen = false;
        Wizard_Selector.lighteningChosen = false;

        // Reset Scene
        BattleSystem.nextSceneAfterLeavingChoice = 2;
    }
}
