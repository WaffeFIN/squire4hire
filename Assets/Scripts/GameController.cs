using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Camera currentCamera;
    public Inventory playerInventory;

    // Overlay visuals
    public GameObject overlay;
    public GameObject pauseMenu;
    public GameObject inventoryContent;

    private bool isGameOngoing = true;
	private int previousCount = -1;

    // Update is called once per frame
    void Update()
    {
        var currentCameraPosition = currentCamera.transform.position;
        overlay.transform.position = new Vector2(currentCameraPosition.x, currentCameraPosition.y);

        if (Input.GetKeyDown(KeyCode.P)) {
            isGameOngoing = !isGameOngoing;
            Time.timeScale = isGameOngoing ? 1 : 0;
            pauseMenu.SetActive(!isGameOngoing);
        }

        if (previousCount != playerInventory.itemsCarried.Count) {
			previousCount = playerInventory.itemsCarried.Count;
            var content = "";
            for (int i = 0; i < playerInventory.itemsCarried.Count; i++)
            {
                var itemName = playerInventory.itemsCarried[i].GetComponent<Item>().GetName();
                content += itemName;
                if (i < playerInventory.itemsCarried.Count - 1) {
                    content += ", ";
                }
            }
           
            inventoryContent.GetComponent<Text>().text = content;
            inventoryContent.GetComponent<Text>().color = playerInventory.IsFull() ? new Color(1f, 0.1f, 0.1f) : new Color(1f, 1f, 1f);
        }
    }
}
