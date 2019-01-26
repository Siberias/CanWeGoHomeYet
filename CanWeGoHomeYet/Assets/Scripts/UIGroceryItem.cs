using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class UIGroceryItem : MonoBehaviour
{
	public Image m_image;
	public Button m_button;
	public string m_collectTrigger;
	public string m_errorTrigger;
	public string m_appearTrigger;
	public string m_hideTrigger;

	private Animator m_animator;
	private bool m_canBePressed = false;

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
			m_image.sprite = m_groceryItem.m_sprite;
		}
	}

	private void Awake()
	{
		m_animator = GetComponent<Animator>();
	}

	public void Appear()
	{
		MakeNotPressable();
		ClearAnimationTriggers();

		m_animator.SetTrigger(m_appearTrigger);
	}

	public void Hide()
	{
		MakeNotPressable();

		m_animator.SetTrigger(m_hideTrigger);
	}

	public void AttemptCollect()
	{
		if (m_canBePressed == false)
		{
			return;
		}

		bool wasCollected;

		ShoppingList.Instance.AttemptToCollect(m_groceryItem, out wasCollected);

		if (wasCollected)
		{
			m_animator.SetTrigger(m_collectTrigger);
		}
		else
		{
			m_animator.SetTrigger(m_errorTrigger);
		}

		MakeNotPressable();

		GroceryGameController.Instance.ResolveChoice(!wasCollected);
		GroceryGameController.Instance.PlayChoiceSound(wasCollected);
	}

	public void MakePressable()
	{
		m_canBePressed = true;
		m_button.interactable = true;
	}

	public void MakeNotPressable()
	{
		m_canBePressed = false;
		m_button.interactable = false;
	}

	public void ClearAnimationTriggers()
	{
		foreach (var param in m_animator.parameters)
		{
			if (param.type == AnimatorControllerParameterType.Trigger)
			{
				m_animator.ResetTrigger(param.name);
			}
		}
	}

	public void ShakeCompleted()
	{
		GroceryGameController.Instance.ShakeCompleted();
	}
}
