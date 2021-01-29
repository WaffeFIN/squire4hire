using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
	public Image image;

    // Update is called once per frame
    void Update()
    {
        image.transform.position = gameObject.transform.position;
    }
}
