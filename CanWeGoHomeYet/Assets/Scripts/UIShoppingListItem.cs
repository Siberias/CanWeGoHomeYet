using UnityEngine;
using UnityEngine.UI;

public class UIShoppingListItem : MonoBehaviour
{
	public Image m_backgroundImage;
	public Image m_productImage;
	public Color m_collectedColor;

	private GroceryItem m_groceryItem;
	public GroceryItem GroceryItem
	{
		get
		{
			return m_groceryItem;
		}
		set
		{
			m_groceryItem = value;
			m_productImage.sprite = m_groceryItem.m_sprite;
		}
	}

	public void SetCollected()
	{
		m_backgroundImage.color = m_collectedColor;
	}
}
