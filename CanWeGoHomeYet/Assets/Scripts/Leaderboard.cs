using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
	[Serializable]
	public class ScoreAndName
	{
		public string score;
		public string name;
	}

	[Serializable]
	public class SerializableScoreList
	{
		public List<ScoreAndName> m_scores = new List<ScoreAndName>();
	}

	public Text m_leaderboardtext;
	public GameObject m_leaderboardUI;
	public GameObject m_inputUI;
	public Text m_yourScoreText;

	private string scoresKey = "scores";
	private SerializableScoreList m_scores = new SerializableScoreList();

	private string m_pendingScore;

	private void Awake()
	{
		LoadScores();

		int totalMinutes = PlayerPrefs.GetInt("Timer");
		string score = GameManager.ConvertMinutes(totalMinutes);
		AddScore(score);

		m_yourScoreText.text = score;

		m_inputUI.SetActive(true);
		m_leaderboardUI.SetActive(false);
	}

	private void Start()
	{
		//Make sure we have the mouse again
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void AddScore(string score)
	{
		m_pendingScore = score;
	}

	public void AddName(string name)
	{
		ScoreAndName scoreEntry = new ScoreAndName
		{
			score = m_pendingScore,
			name = name
		};

		m_scores.m_scores.Add(scoreEntry);

		SaveScores();

		UpdateLeaderboardText();
		m_inputUI.SetActive(false);
		m_leaderboardUI.SetActive(true);
	}

	public void UpdateLeaderboardText()
	{
		string scoresFormatted = "";

		foreach (var scoreEntry in m_scores.m_scores)
		{
			scoresFormatted += scoreEntry.score + "   " + scoreEntry.name + "\n";
		}

		m_leaderboardtext.text = scoresFormatted;
	}

	public void ClearScores()
	{
		m_scores.m_scores.Clear();
		SaveScores();
		UpdateLeaderboardText();
	}

	private void LoadScores()
	{
		string scoresJson = PlayerPrefs.GetString(scoresKey, "");

		SerializableScoreList loadedList = JsonUtility.FromJson<SerializableScoreList>(scoresJson);
		if (loadedList != null)
		{
			m_scores = loadedList;
		}
	}

	private void SaveScores()
	{
		string scoresJson = JsonUtility.ToJson(m_scores);

		PlayerPrefs.SetString(scoresKey, scoresJson);
	}
}
