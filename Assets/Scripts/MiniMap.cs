using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private GameObject minimapArrow = null;
    private bool iceArrowSet = false;
	private bool fireArrowSet = false;
	private bool lighteningArrowSet = false;
	private bool almightyArrowSet = false;

	// Start and current X position for all enemies
	private float iceXPosition = -3.45f;
	private float fireXPosition = -1.37f;
	private float lighteningXPosition = 0.8f;
	private float almightyXPosition = 2.9f;

	// End X position for all enemies (Almighty not needed)
	private float iceEndXPosition = -1.43f;
	private float fireEndXPosition = 0.8f;
	private float lighteningEndXPosition = 2.9f;

	// The gap between start and end X positions
	private float iceGap = 0.0f;
	private float fireGap = 0.0f;
	private float lighteningGap = 0.0f;

	private float oneHPMoveAmount = 0.0f;
	public static float enemyPreviousHP = 0.0f;
	private float minimapArrowTargetXPosition = 0.0f;
	public static bool updatedArrowPosition = true;
	

	private void Start()
	{
		minimapArrowTargetXPosition = 0.0f;
		oneHPMoveAmount = 0.0f;
		enemyPreviousHP = 0.0f;
		iceGap = iceEndXPosition - iceXPosition;
		fireGap = fireEndXPosition - fireXPosition;
		lighteningGap = lighteningEndXPosition - lighteningXPosition;
	}

	// Update is called once per frame
	void Update()
    {
	    if (BattleSystem.enemyInstance.CompareTag("Ice_Enemy")) {
		    if (!iceArrowSet)
		    {
			    minimapArrow.transform.position = new Vector3(iceXPosition, 4.1f, 0.0f);
				iceArrowSet = true;
			}

		    if (enemyPreviousHP == 0)
		    {
			    enemyPreviousHP = Unit.currentIceEnemyHPMinimap;
		    }

			// Update minimap arrow position based on enemy health
			if (minimapArrow.transform.position.x < iceEndXPosition && Unit.maxIceEnemyHPMinimap != 0 && updatedArrowPosition == false)
			{
				// Get 1HP worth of the move distance
				oneHPMoveAmount = iceGap / Unit.maxIceEnemyHPMinimap;
				// Get new X position amount
				minimapArrowTargetXPosition = minimapArrow.transform.position.x + (enemyPreviousHP - Unit.currentIceEnemyHPMinimap) * oneHPMoveAmount;
				// Update arrow position
				minimapArrow.transform.position = new Vector3(minimapArrowTargetXPosition, minimapArrow.transform.position.y, minimapArrow.transform.position.z);
				// Update previous enemy HP
				enemyPreviousHP = Unit.currentIceEnemyHPMinimap;
				// Updated arrow
				updatedArrowPosition = true;
			}


	    }
	    else if (BattleSystem.enemyInstance.CompareTag("Fire_Enemy")) {
		    if (!fireArrowSet) {
				minimapArrow.transform.position = new Vector3(fireXPosition, 4.1f, 0.0f);
				fireArrowSet = true;
		    }

		    if (enemyPreviousHP == 0) {
			    enemyPreviousHP = Unit.currentFireEnemyHPMinimap;
		    }

			// Update minimap arrow position based on enemy health
			if (minimapArrow.transform.position.x < fireEndXPosition && Unit.currentFireEnemyHPMinimap != 0 && updatedArrowPosition == false) {
			    // Get 1HP worth of the move distance
			    oneHPMoveAmount = fireGap / Unit.maxFireEnemyHPMinimap;
			    // Get new X position amount
			    minimapArrowTargetXPosition = minimapArrow.transform.position.x + (enemyPreviousHP - Unit.currentFireEnemyHPMinimap) * oneHPMoveAmount;
			    // Update arrow position
			    minimapArrow.transform.position = new Vector3(minimapArrowTargetXPosition, minimapArrow.transform.position.y, minimapArrow.transform.position.z);
			    // Update previous enemy HP
			    enemyPreviousHP = Unit.currentFireEnemyHPMinimap;
			    // Updated arrow
			    updatedArrowPosition = true;
		    }
		}
		else if (BattleSystem.enemyInstance.CompareTag("Lightening_Enemy")) {
		    if (!lighteningArrowSet) {
				minimapArrow.transform.position = new Vector3(lighteningXPosition, 4.1f, 0.0f);
				lighteningArrowSet = true;
		    }

		    if (enemyPreviousHP == 0) {
			    enemyPreviousHP = Unit.currentLighteningEnemyHPMinimap;
		    }

			// Update minimap arrow position based on enemy health
			if (minimapArrow.transform.position.x < lighteningEndXPosition && Unit.maxLighteningEnemyHPMinimap != 0 && updatedArrowPosition == false) {
			    // Get 1HP worth of the move distance
			    oneHPMoveAmount = lighteningGap / Unit.maxLighteningEnemyHPMinimap;
			    // Get new X position amount
			    minimapArrowTargetXPosition = minimapArrow.transform.position.x + (enemyPreviousHP - Unit.currentLighteningEnemyHPMinimap) * oneHPMoveAmount;
			    // Update arrow position
			    minimapArrow.transform.position = new Vector3(minimapArrowTargetXPosition, minimapArrow.transform.position.y, minimapArrow.transform.position.z);
			    // Update previous enemy HP
			    enemyPreviousHP = Unit.currentLighteningEnemyHPMinimap;
			    // Updated arrow
			    updatedArrowPosition = true;
		    }
		} 
	    else if (BattleSystem.enemyInstance.CompareTag("Almighty_Enemy")) {
		    if (!almightyArrowSet) {
				minimapArrow.transform.position = new Vector3(almightyXPosition, 4.1f, 0.0f);
				almightyArrowSet = true;
		    }
			
	    }
	}
}
