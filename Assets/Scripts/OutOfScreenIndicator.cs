using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutOfScreenIndicator : MonoBehaviour
{
	public GameObject indicator;

	public int margin;

	void Update() {
		Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));
		Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
		var px = transform.position.x;
		var py = transform.position.y;
		var x = Mathf.Min(Mathf.Max(bottomLeft.x + margin, px), topRight.x - margin);
		var y = Mathf.Min(Mathf.Max(bottomLeft.y + margin, py), topRight.y - margin);
		indicator.transform.position = new Vector3(x, y, 0);
		indicator.GetComponent<Image>().enabled = px < bottomLeft.x || py < bottomLeft.y || px > topRight.x || py > topRight.y;
	}
}
