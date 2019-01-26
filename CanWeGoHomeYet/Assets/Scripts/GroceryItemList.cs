using System.Collections.Generic;
using UnityEngine;

public class GroceryItemList : MonoBehaviour
{
	public static GroceryItemList Instance { get; private set; }
	public List<GroceryItem> m_items;

	private void Awake()
	{
		Instance = this;
	}
}
