using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
	public static int otherItemsRetrieved = 0;
	public static int weaponsRetrieved = 0;
	public static int armorPartsRetrieved = 0;
	public static int hitsDodged = 0;
	public static int totalDodges = 0;
	public static int itemsLost = 0;
	public static int armorPolishes = 0;

	public static int Score() {
		return otherItemsRetrieved * 50 +
			weaponsRetrieved * 100 +
			armorPartsRetrieved * 75 +
			hitsDodged * 5 -
			itemsLost * 25 +
			armorPolishes * 250;
	}
}