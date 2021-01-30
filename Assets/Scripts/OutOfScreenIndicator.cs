using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutOfScreenIndicator : MonoBehaviour
{
	public GameObject indicator;

	private int margin = 28;

	void Update() {
		Vector3 corner1 = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));
		Vector3 corner2 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
		var px = transform.position.x;
		var py = transform.position.y;
		var x = Mathf.Min(Mathf.Max(corner1.x + margin, px), corner2.x - margin);
		var y = Mathf.Min(Mathf.Max(corner1.y + margin, py), corner2.y - margin);
		indicator.transform.position = new Vector3(x, y, 0);
		indicator.GetComponent<Image>().enabled = px < corner1.x || py < corner1.y || px > corner2.x || py > corner2.y;
	}
}
