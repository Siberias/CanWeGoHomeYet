using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public delegate void OnGameStartDelegate();
	public static event OnGameStartDelegate OnGameStart;

    //whether cursor is on or not
    public bool cursorOn = false;

	public string nextScene;
	public AudioClip endGameSound;
	public float m_clockZoomScaleFactor = 1.0f;

	//clock variables
	[SerializeField]
	private GameObject clockPanel1, clockPanel2;
	[SerializeField]
	private Text clock;
	private int minFontSize = 36;
	private int maxFontSize = 200;
	private int totalMinutes;
	private float minuteTimer;
	private float minuteTime = 1;

	//fading in variables
	[SerializeField]
	private CanvasGroup fader;
	private int framesToLoad = 100;
	[SerializeField]
	private Text countdownText;
	[SerializeField]
	private GameObject countDownPanel;

	[SerializeField]
	private GameObject transitionCanvas;

	private AudioSource m_audioPlayer;

	public static GameManager Instance { get; private set; }

	private void Awake()
	{
		m_audioPlayer = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start()
	{
		Instance = this;

		transitionCanvas.SetActive(true);

		StartCoroutine(FadeIn());

		if (ResetGame.HasResetTimer == false)
		{
			ResetGame.ResetTimer();
		}

		//set up clock variables
		totalMinutes = PlayerPrefs.GetInt("Timer", 9 * 60); //Start at 9:00 if the timer hasn't been set yet
		minuteTimer = 0;

        if (cursorOn) {
            Cursor.visible = true;
        } else {
            Cursor.visible = false;
        }
	}

	// Update is called once per frame
	void Update()
	{
		if (minuteTimer <= Time.time)
		{
			totalMinutes++;
			minuteTimer = Time.time + minuteTime;

			clock.text = ConvertMinutes();
		}
	}

	//change to the next scene
	public void ChangeScene()
	{
		SceneManager.LoadScene(nextScene);
	}

	//convert minutes to digital time as string
	private string ConvertMinutes()
	{

		//calculate time in hours and minutes
		int hours = totalMinutes / 60;
		int minutes = totalMinutes % 60;
		string returnTime;

		if (hours < 10)
		{
			returnTime = "0" + hours + ":";
		}
		else
		{
			returnTime = hours + ":";
		}

		if (minutes < 10)
		{
			returnTime += "0" + minutes;
		}
		else
		{
			returnTime += minutes;
		}

		return returnTime;
	}

	//fade into the game
	IEnumerator FadeIn()
	{

		//set the clock to be large and in the centre of the screen
		clock.fontSize = (int)maxFontSize;
		Vector3 initialPos = clock.transform.parent.transform.parent.TransformPoint(new Vector3(0, Screen.height / 4, 0));
		clock.transform.position = initialPos;

		for (float i = 1; i > 0; i -= (1f / framesToLoad))
		{
			fader.alpha = i;

			clock.fontSize = (int)Mathf.Lerp(maxFontSize, minFontSize, 1 - i);
			clock.transform.position = Vector3.Lerp(initialPos, clockPanel1.transform.position, 1 - i);

			yield return new WaitForEndOfFrame();
		}

		//make clock background visible
		clockPanel1.SetActive(true);
		clockPanel2.SetActive(true);

		//countdown
		for (int i = 3; i >= 0; i--)
		{

			if (i != 0)
			{
				countdownText.text = i.ToString();
			}
			else
			{
				countdownText.text = "GO!";
			}

			yield return new WaitForSeconds(0.75f);

		}

		countDownPanel.SetActive(false);
		countdownText.text = "";

		OnGameStart?.Invoke();
	}

	//fade into the game
	public IEnumerator FadeOut()
	{
		//The mini game has been won! Let's celebrate
		m_audioPlayer.clip = endGameSound;
		m_audioPlayer.Play();

		//set the clock to be large and in the centre of the screen
		Vector3 endPos = clock.transform.parent.transform.parent.TransformPoint(new Vector3(0, Screen.height / (4.0f * m_clockZoomScaleFactor), 0));

		//make clock background invisible
		clockPanel1.SetActive(false);
		clockPanel2.SetActive(false);

		for (float i = 0; i < 1; i += (1f / framesToLoad))
		{
			fader.alpha = i;

			clock.fontSize = (int)Mathf.Lerp(minFontSize, maxFontSize, i);
			clock.transform.position = Vector3.Lerp(clockPanel1.transform.position, endPos, i);

			yield return new WaitForEndOfFrame();
		}

		PlayerPrefs.SetInt("Timer", totalMinutes);

		ChangeScene();
	}
}
