using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemUI;

    private Dictionary<string, GameObject> PrefabDic;
    void Awake() {
    	PrefabDic = MapItemLegendToPrefabs(ITEM_LEGEND);
    }

    public GameObject SpawnItem(string itemId, Transform entityTransform) {
		GameObject itemObj;
        if (PrefabDic.TryGetValue(itemId, out GameObject obj)) {
			itemObj = Instantiate(obj, entityTransform.position, Quaternion.identity);
            var itemSprite = itemObj.GetComponent<Item>().SpriteRef;
			var image = GenerateImageForItem(itemId, itemSprite);
            itemObj.GetComponent<ImageLink>().image = image;
       		image.transform.position = entityTransform.position;
		} else {
			throw new NotImplementedException($"Error trying to instantiate {itemId}");
		}
        return itemObj;
    }

    private Image GenerateImageForItem(string itemId, Sprite spriteRef) {
        GameObject imgObject = new GameObject(itemId);

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
        trans.localPosition = new Vector3(0, 0, 0);
        trans.position = new Vector3(0, 0, 0);
        trans.sizeDelta = new Vector2(16, 16);

        Image image = imgObject.AddComponent<Image>();
        image.sprite = spriteRef;
        imgObject.transform.SetParent(itemUI.transform);
        return image;
    }
    
    private static Dictionary<string, GameObject> MapItemLegendToPrefabs(List<string> itemLegend) {
        var resourceDic = new Dictionary<string, GameObject>();

        var errors = new List<string>();
        foreach (var id in itemLegend) {
            if (!resourceDic.ContainsKey(id)) {
                string path = System.IO.Path.Combine("Prefabs", id);
                GameObject prefab = Resources.Load<GameObject>(path);
                if (prefab == null) {
                    errors.Add($"Couldn't load {path}");
                }
                resourceDic[id] = prefab;
            }
        }
        if (errors.Count > 0) {
            throw new NotImplementedException($"Error trying to map items:\n\t{System.String.Join("\n\t", errors.ToArray())}");
        }
		return resourceDic;
	}

    private static readonly List<string> ITEM_LEGEND = new List<string>() {
        "pickupArrow",
        "pickupMace",
        "pickupHealthPotion",
        "pickupBow",
        "pickupShield",
        "pickupArmor"
    };
}
