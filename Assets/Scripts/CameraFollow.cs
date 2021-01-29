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

	public Camera camera;

    void LateUpdate() {
        if (Input.GetKey("space"))
        {
            // choose the margin randomly
            float margin = Random.Range(0.0f, 0.3f);
            // setup the rectangle
            camera.rect = new Rect(margin, 0.0f, 1.0f - margin * 2.0f, 1.0f);
        }
    }
}
