using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public string m_sceneToLoad;

	public void LoadScene()
	{
		SceneManager.LoadScene(m_sceneToLoad);
	}
}
