using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnimationTrigger))]
public class AnimationTriggerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		AnimationTrigger trigger = target as AnimationTrigger;

		bool guiEnabledState = GUI.enabled;

		GUI.enabled = Application.isPlaying;

		if (GUILayout.Button("Trigger!"))
		{
			trigger.Trigger();
		}

		GUI.enabled = guiEnabledState;
	}
}
