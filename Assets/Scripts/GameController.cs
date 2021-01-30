using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Camera currentCamera;
    public GameObject pauseMenu;

    private bool isGameOngoing = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            isGameOngoing = !isGameOngoing;
            Time.timeScale = isGameOngoing ? 1 : 0;
            if (!isGameOngoing) {
                var currentCameraPosition = currentCamera.transform.position;
                pauseMenu.transform.position = new Vector2(currentCameraPosition.x, currentCameraPosition.y);
            }
            pauseMenu.SetActive(!isGameOngoing);
        }
    }
}
