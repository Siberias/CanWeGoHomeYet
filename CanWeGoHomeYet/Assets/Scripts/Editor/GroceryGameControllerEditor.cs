using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GroceryGameController))]
public class GroceryGameControllerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		GroceryGameController gameController = target as GroceryGameController;

		if (GUILayout.Button("Win Game!"))
		{
			gameController.FinishGame();
		}
	}
}
