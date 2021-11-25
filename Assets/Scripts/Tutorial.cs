using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	[SerializeField] private float movementSpeed = 0.0f;
    [SerializeField] private float timerStart = 0.0f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
	    timer = timerStart;
    }

    // Update is called once per frame
    void Update()
    {
	    
	    if (timer <= 0)
	    {
		    movementSpeed = 0.0f;
	    }
	    else
	    {
		    timer -= Time.deltaTime;
		}

	    transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
