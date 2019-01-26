using System.Collections.Generic;
using UnityEngine;

public class GroceryGameController : MonoBehaviour
{
	public float m_timeToChoose = 5.0f;
	public float m_timeToHide = 2.0f;

	public UIGroceryItem m_leftItem;
	public UIGroceryItem m_rightItem;

	public GameObject m_youWinFeedback;

	private int m_numChoicesLeft;
	private List<UIShoppingListItem> m_shoppingListCopy;

	private float m_choiceTimer = 0.0f;
	private float m_hideTimer = 0.0f;

	public static GroceryGameController Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		CountdownToGameStart();
	}

	private void Update()
	{
		if (m_choiceTimer > 0.0f)
		{
			m_choiceTimer -= Time.deltaTime;

			if (m_choiceTimer <= 0.0f)
			{
				ResolveChoice(false);
			}
		}

		if (m_hideTimer > 0.0f)
		{
			m_hideTimer -= Time.deltaTime;

			if (m_hideTimer <= 0.0f)
			{
				CheckWinCondition();
			}
		}
	}

	public void CountdownToGameStart()
	{
		//TODO: Wait for countdown animation
		StartGame();
	}

	public void StartGame()
	{
		m_shoppingListCopy = new List<UIShoppingListItem>(ShoppingList.Instance.m_items);
		m_shoppingListCopy.Shuffle();

		StartNewRound();
	}

	public void StartNewRound()
	{
		m_numChoicesLeft = ShoppingList.Instance.m_items.Count;

		ShowShoppingList();
	}

	public void ShowShoppingList()
	{
		ShoppingList.Instance.ShowList();
	}

	public void StartNextChoice()
	{
		--m_numChoicesLeft;

		//Choose 1 required item and one random item
		GroceryItem item1 = m_shoppingListCopy[m_numChoicesLeft].GroceryItem;
		int secondChoice = Random.Range(0, GroceryItemList.Instance.m_items.Count);
		GroceryItem item2 = GroceryItemList.Instance.m_items[secondChoice];

		//If both items are the same, roll again!
		while (item1.m_id.Equals(item2.m_id))
		{
			secondChoice = Random.Range(0, GroceryItemList.Instance.m_items.Count);
			item2 = GroceryItemList.Instance.m_items[secondChoice];
		}

		//Randomly choose whether the required item is on the left of right
		if (Random.value > 0.5f)
		{
			m_leftItem.GroceryItem = item1;
			m_rightItem.GroceryItem = item2;
		}
		else
		{
			m_leftItem.GroceryItem = item2;
			m_rightItem.GroceryItem = item1;
		}

		m_leftItem.Appear();
		m_rightItem.Appear();

		m_choiceTimer = m_timeToChoose;
	}

	public void ResolveChoice(bool madeWrongChoice)
	{
		m_choiceTimer = 0.0f;

		if (madeWrongChoice == false)
		{
			m_leftItem.Hide();
			m_rightItem.Hide();

			m_hideTimer = m_timeToHide;
		}
	}

	public void ShakeCompleted()
	{
		m_leftItem.Hide();
		m_rightItem.Hide();

		m_hideTimer = m_timeToHide;
	}

	public void CheckWinCondition()
	{
		if (ShoppingList.Instance.DoWeHaveThemAll)
		{
			FinishGame();
			return;
		}

		if (m_numChoicesLeft == 0)
		{
			StartNewRound();
		}
		else
		{
			StartNextChoice();
		}
	}

	public void FinishGame()
	{
		m_youWinFeedback.SetActive(true);
	}
}
