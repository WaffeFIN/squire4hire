using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Canvas canvas;
    // Get proper layering this way
    public GameObject enemyUI;

    //temporary spawnTimer
    private float nextSpawn = 1;

    //public bool gnoming = true;

    private Dictionary<string, GameObject> PrefabDic;
    void Awake() {
    	PrefabDic = MapEnemyLegendToPrefabs(ENEMY_LEGEND);
    }

    void Update()
    {
        if (Time.time > nextSpawn) {
            var canvasRect = canvas.GetComponent<RectTransform>().rect;
            var randomX = UnityEngine.Random.Range(0.1f, canvasRect.width) - canvasRect.width/2;
            var randomY = UnityEngine.Random.Range(0.1f, canvasRect.height) - canvasRect.height/2;
            SpawnEnemy("enemy-1", new Vector2(randomX, randomY));
            nextSpawn += 2;
        }
    }

    public GameObject SpawnEnemy(string enemyId, Vector2 spawnPosition) {
        if (PrefabDic.TryGetValue(enemyId, out GameObject obj)) {
			var itemObj = Instantiate(obj, spawnPosition, Quaternion.identity);
            var itemSprite = itemObj.GetComponent<Enemy>().SpriteRef;
            var animator = itemObj.GetComponent<Animator>();
            itemObj.GetComponent<ImageManager>().image = GenerateImageForEnemy(enemyId, itemSprite, animator);
		} else {
			throw new NotImplementedException($"Error trying to instantiate {enemyId}");
		}
        return obj;
    }

    private Image GenerateImageForEnemy(string enemyId, Sprite spriteRef, Animator animator) {
        GameObject imgObject = new GameObject(enemyId);

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
        trans.localPosition = new Vector3(0, 0, 0);
        trans.position = new Vector3(0, 0, 0);
        trans.sizeDelta = new Vector2(14, 24);

        Image image = imgObject.AddComponent<Image>();
        image.sprite = spriteRef;
       //image.color = new Color(1f, 0.1f, 0.1f);
        imgObject.transform.SetParent(enemyUI.transform);
        Animator anim = imgObject.AddComponent<Animator>();
        anim.runtimeAnimatorController = animator.runtimeAnimatorController;

        animator.SetBool("gnoming", true);
        animator.Play("GNOME_Walking");
        return image;
    }
    
    private static Dictionary<string, GameObject> MapEnemyLegendToPrefabs(List<string> enemyLegend) {
        var resource_dic = new Dictionary<string, GameObject>();

        var errors = new List<string>();
        foreach (var id in enemyLegend) {
            if (!resource_dic.ContainsKey(id)) {
                string path = System.IO.Path.Combine("Prefabs", id);
                GameObject prefab = Resources.Load<GameObject>(path);
                if (prefab == null) {
                    errors.Add($"Couldn't load {path}");
                }
                resource_dic[id] = prefab;
            }
        }
        if (errors.Count > 0) {
            throw new NotImplementedException($"Error trying to map items:\n\t{System.String.Join("\n\t", errors.ToArray())}");
        }
		return resource_dic;
	}

    private static readonly List<string> ENEMY_LEGEND = new List<string>() {
		"enemy-1"
	};
}
