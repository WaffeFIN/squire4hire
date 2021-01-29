using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform heroPosition;

    void FixedUpdate() {
        gameObject.transform.position = new Vector3(heroPosition.position.x, heroPosition.position.y);
    }
}
