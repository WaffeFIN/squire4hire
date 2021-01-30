using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D col)
    {
		var itemComponent = col.gameObject.GetComponent<Item>();
        if (itemComponent != null && itemComponent.pickupTimer < 0)
        {
            var inventoryComponent = GetComponent<Inventory>();

            if (inventoryComponent.Weight() + itemComponent.ItemWeight <= inventoryComponent.maxWeight) {

                inventoryComponent.itemsCarried.Add(col.gameObject);
                col.gameObject.SetActive(false);
                var imageManager = col.gameObject.GetComponent<ImageManager>();
                if (imageManager != null)
                {
                    imageManager.image.enabled =false;
                }
            }

        }
    }
    void Update()
    {

    }
}
