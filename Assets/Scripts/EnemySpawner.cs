using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Canvas canvas;
    public GameObject enemyUI;

    private float nextSpawn = 1;
    private Dictionary<string, GameObject> prefabDic;

    void Awake() {
    	prefabDic = MapEnemyLegendToPrefabs(ENEMY_LEGEND);
    }

    void Update()
    {
        if (Time.time > nextSpawn) {
            var canvasRect = canvas.GetComponent<RectTransform>().rect;
            var randomX = UnityEngine.Random.Range(0.1f, canvasRect.width) - canvasRect.width/2;
            var randomY = UnityEngine.Random.Range(0.1f, canvasRect.height) - canvasRect.height/2;
            SpawnEnemy("enemyGnome", new Vector2(randomX, randomY));
            nextSpawn += 2;
        }
    }

    public GameObject SpawnEnemy(string enemyId, Vector2 spawnPosition) {
        if (prefabDic.TryGetValue(enemyId, out GameObject obj)) {
			var enemyObj = Instantiate(obj, spawnPosition, Quaternion.identity);
            var enemySprite = enemyObj.GetComponent<Enemy>().SpriteRef;
            var animator = enemyObj.GetComponent<Animator>();
            enemyObj.GetComponent<ImageLink>().image = GenerateImageForEnemy(enemyId, enemySprite, animator);
			enemyObj.GetComponent<TargetMovement>().target = enemyObj.transform.position;
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
		
        imgObject.transform.SetParent(enemyUI.transform);
        Animator anim = imgObject.AddComponent<Animator>();
        anim.runtimeAnimatorController = animator.runtimeAnimatorController;

        animator.SetBool("gnoming", true);
        animator.Play("GNOME_Walking");
        return image;
    }
    
    private static Dictionary<string, GameObject> MapEnemyLegendToPrefabs(List<string> enemyLegend) {
        var resourceDic = new Dictionary<string, GameObject>();

        var errors = new List<string>();
        foreach (var id in enemyLegend) {
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

    private static readonly List<string> ENEMY_LEGEND = new List<string>() {
		"enemyGnome"
	};
}
