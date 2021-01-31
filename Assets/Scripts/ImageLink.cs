using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLink : MonoBehaviour
{
	public Image image;

    // Update is called once per frame
    void Update()
    {
        image.transform.position = gameObject.transform.position;
    }

	void OnDestroy()
	{
		if (image != null)
			Destroy(image.gameObject);
	}
}
