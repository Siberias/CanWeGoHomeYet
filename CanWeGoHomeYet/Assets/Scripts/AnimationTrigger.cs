using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationTrigger : MonoBehaviour
{
	public string m_triggerName;

	private Animator m_animator;

	private void Awake()
	{
		m_animator = GetComponent<Animator>();
	}

	public void Trigger()
	{
		m_animator.SetTrigger(m_triggerName);
	}
}
