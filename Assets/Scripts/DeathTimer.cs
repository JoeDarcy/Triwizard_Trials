using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    [SerializeField] private GameObject attackEffect = null;
    [SerializeField] private float timerStart = 0.0f;
	private float timer = 0.0f;

	private void Start() {
		timer = timerStart;
	}

	// Start is called before the first frame update
	private void Update()
	{

		timer -= Time.deltaTime;
		
		if (timer <= 0)
		{
			Destroy(attackEffect);
		}
	}
}
