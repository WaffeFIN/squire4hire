using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverlayController : MonoBehaviour
{
    public Camera currentCamera;
    public GameObject player;
    public GameObject hero;

    // Overlay visuals
    public GameObject overlay;
    public GameObject menuBox;
    public GameObject pauseText;
    public GameObject gameOverText;
    public GameObject inventoryContent;
    public Text healthText;

    private bool isGameOngoing = true;
	private int previousCount = -1;
    private bool isGameOver = false;

    void Update()
    {
        var currentCameraPosition = currentCamera.transform.position;
        overlay.transform.position = new Vector2(currentCameraPosition.x, currentCameraPosition.y);

        if (!isGameOngoing) {
            if(Input.GetKeyDown(KeyCode.R)) {
                // Doesn't work properly yet
                // SceneManager.LoadScene(0);
            }
            if(Input.GetKeyDown(KeyCode.Q)) {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            }
        }

        if (isGameOver) { return; }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            isGameOngoing = !isGameOngoing;
            Time.timeScale = isGameOngoing ? 1 : 0;
            pauseText.SetActive(!isGameOngoing);
            menuBox.SetActive(!isGameOngoing);
        }

        if (player.GetComponent<Health>().IsDead() || hero.GetComponent<Health>().IsDead()) {
            isGameOver = true;
            isGameOngoing = false;
            Time.timeScale = 0;
            gameOverText.SetActive(true);
            menuBox.SetActive(true);
        }

        var playerInventory = player.GetComponent<Inventory>();
        if (previousCount != playerInventory.itemsCarried.Count) {
			previousCount = playerInventory.itemsCarried.Count;
            var content = playerInventory.Weight() + "/" + playerInventory.maxWeight + "\n";
            for (int i = 0; i < playerInventory.itemsCarried.Count; i++)
            {
                var itemName = playerInventory.itemsCarried[i].GetComponent<Item>().GetName();
                content += itemName;
                if (i < playerInventory.itemsCarried.Count - 1) {
                    content += ", ";
                }
            }
           
            inventoryContent.GetComponent<Text>().text = content;
            inventoryContent.GetComponent<Text>().color = playerInventory.IsFull() ? new Color(0.9f, 0.2f, 0.2f) : new Color(1f, 1f, 1f);
        }

		var playerHealth = player.GetComponent<Health>();
		healthText.text = "HP\n" + playerHealth.currentHealth;
    }
}
