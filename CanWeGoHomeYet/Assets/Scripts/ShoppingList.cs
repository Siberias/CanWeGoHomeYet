using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShoppingList : MonoBehaviour
{
	public static ShoppingList Instance { get; private set; }

	public List<UIShoppingListItem> m_items;

	private Dictionary<string, bool> m_collectedItems = new Dictionary<string, bool>();
	private Animator m_animator;

	public float m_timeBeforeHide = 5.0f;
	private float m_showTimer;

	private void Awake()
	{
		Instance = this;

		m_animator = GetComponent<Animator>();
	}

	private void Start()
	{
		CreateList();
	}

	private void Update()
	{
		if (m_showTimer > 0.0f)
		{
			m_showTimer -= Time.deltaTime;

			if (m_showTimer <= 0.0f)
			{
				m_animator.SetTrigger("Hide");
			}
		}
	}

	public void ShowList()
	{
		m_animator.SetTrigger("Appear");
	}

	private void CreateList()
	{
		List<GroceryItem> groceriesCopy = new List<GroceryItem>(GroceryItemList.Instance.m_items);
		groceriesCopy.Shuffle();

		m_collectedItems.Clear();

		for (int i = 0; i < m_items.Count; ++i)
		{
			m_items[i].GroceryItem = groceriesCopy[i];
			m_collectedItems[groceriesCopy[i].m_id] = false;
		}
	}

	public void AttemptToCollect(GroceryItem item, out bool wasCollected)
	{
		if (m_collectedItems.ContainsKey(item.m_id) == false)
		{
			//Item is not on the shopping list
			wasCollected = false;
			return;
		}

		if (m_collectedItems[item.m_id] == true)
		{
			//Already have this item!
			wasCollected = false;
			return;
		}

		m_collectedItems[item.m_id] = true;

		UIShoppingListItem collectedItemUI = m_items.Find(match => match.GroceryItem.m_id.Equals(item.m_id));
		collectedItemUI.SetCollected();

		wasCollected = true;
	}

	public bool DoWeHaveThemAll
	{
		get
		{
			foreach (var item in m_collectedItems)
			{
				if (item.Value == false)
				{
					return false;
				}
			}

			return true;
		}
	}

	public void ShoppingListHidden()
	{
		GroceryGameController.Instance.StartNextChoice();
	}

	public void ShoppingListAppeared()
	{
		m_showTimer = m_timeBeforeHide;
	}

	public GroceryItem GetUncollectedGroceryItem()
	{
		int choice = Random.Range(0, m_collectedItems.Count);
		GroceryItem shoppingListItem = m_items[choice].GroceryItem;

		while (m_collectedItems[shoppingListItem.m_id] == true)
		{
			choice = Random.Range(0, m_collectedItems.Count);
			shoppingListItem = m_items[choice].GroceryItem;
		}

		return shoppingListItem;
	}
}
