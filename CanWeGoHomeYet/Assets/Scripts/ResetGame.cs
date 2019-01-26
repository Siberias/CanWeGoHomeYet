using UnityEngine;

public class ResetGame : MonoBehaviour
{
	public static bool HasResetTimer = false;

	private void Start()
	{
		ResetTimer();
	}

	public static void ResetTimer()
	{
		HasResetTimer = true;

		PlayerPrefs.DeleteKey("Timer");
	}
}
