using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cleanup : MonoBehaviour
{
    [SerializeField] private GameObject iceHealthBar = null;
    [SerializeField] private GameObject fireHealthBar = null;
    [SerializeField] private GameObject lighteningHealthBar = null;


    // Update is called once per frame
    void Update()
    {
	    if (Wizard_Selector.iceChosen)
	    {
			iceHealthBar.SetActive(false);
	    }
	    else
	    {
		    iceHealthBar.SetActive(true);
		}

	    if (Wizard_Selector.fireChosen) {
		    fireHealthBar.SetActive(false);
		} 
	    else 
	    {
		    fireHealthBar.SetActive(true);
		}

	    if (Wizard_Selector.lighteningChosen) {
		    lighteningHealthBar.SetActive(false);
		} 
	    else 
	    {
		    lighteningHealthBar.SetActive(true);
		}
	}
}
