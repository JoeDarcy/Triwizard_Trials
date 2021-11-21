using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitFunction : MonoBehaviour
{
	[SerializeField] private float timerStart = 0.0f;
	[SerializeField] private bool timerOn = false;
	private float timer = 0.0f;

	private void Start()
	{
		timer = timerStart;
	}

	// Update is called once per frame
	void Update()
	{
		if (timerOn)
		{
			timer -= Time.deltaTime;
		}

		if (timer < 0)
		{
			Application.Quit();
		}

	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
            Application.Quit();
	    }
    }

    public void QuitButtonPressed()
    {
	    Application.Quit();
    }
}
