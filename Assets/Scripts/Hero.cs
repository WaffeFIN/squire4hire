using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = new Vector3(200, 200);
    }

    // Update is called once per frame
    void Update()
    {
        var currentPosition = gameObject.transform.position;
        gameObject.transform.position = new Vector3(currentPosition.x + 1, currentPosition.y);
    }
}
