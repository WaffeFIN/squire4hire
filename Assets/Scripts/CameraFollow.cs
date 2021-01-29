using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform HeroTransform;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public float CameraDistance;
    void Awake() {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / CameraDistance);
    }

    void LateUpdate() {
        transform.position = Vector3.Slerp(transform.position, HeroTransform.position, SmoothFactor);
    }
}
