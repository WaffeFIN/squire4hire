using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
	public float decayTime;

    // Update is called once per frame
    void Update()
    {
        decayTime -= Time.deltaTime;
		if (decayTime < 0)
			Destroy(gameObject);
    }
}
