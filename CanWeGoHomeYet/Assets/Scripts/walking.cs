using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class walking : MonoBehaviour
{

	public GameObject player;
	//public GameObject canvas;
	private int lane;
	public bool isWalking;
	public Text speaking;

	//Get canvas images
	public Image flyer, grandma, mum, sister, speechBubble;

	private bool inInteraction = false;
	private bool isCounting;

	private void Awake()
	{
		GameManager.OnGameStart += GameStart;
		isCounting = true;
	}

	// Use this for initialization


	// Use this for initialization
	void GameStart()
	{
		GameManager.OnGameStart -= GameStart;

		//Set lane to 1 (middle)
		lane = 1;

		isWalking = true;
		isCounting = false;


	}

	// Update is called once per frame
	void Update()
	{
		if (isCounting == true || inInteraction == true)
		{
			return;
		}

		//if press a move left
		if (lane != 0 && (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow)))
		{
			MoveLeft();
		}
		//if press d move right
		if (lane != 2 && (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow)))
		{
			MoveRight();
		}

		//Check walking is true
		if (isWalking == true)
		{
			//Move Forward
			player.transform.Translate(new Vector3(0, 0, 16) * Time.deltaTime);
		}
	}

	IEnumerator ListenToPerson()
	{
		yield return new WaitForSeconds(3.0f);
		TurnOffCanvas();
		inInteraction = false;
		isWalking = true;
	}

	void MoveRight()
	{
		isWalking = true;
		player.transform.Translate(new Vector3(3, 0, 0));
		lane++;
	}

	void MoveLeft()
	{
		isWalking = true;
		player.transform.Translate(new Vector3(-3, 0, 0));
		lane--;
	}

	//walk into enemy
	private void OnTriggerEnter(Collider other)
	{
		isWalking = false;
		if (other.tag == "Enemy" || other.tag == "Grandma" || other.tag == "Mum" || other.tag == "Flyer" || other.tag == "Sister")
		{
			Destroy(other.gameObject);
			Interaction(other.tag);

		}
		else if (other.tag == "Bush" || other.tag == "Fence")
		{


		}
		else if (other.tag == "School" || other.tag == "Home")
		{
			//end game here
			print("end game");
			isWalking = false;

            if(other.tag == "Home") {
                GameObject.Find("Door_Hinge").GetComponent<Animator>().Play("doorOpen");
            }

			StartCoroutine(GameManager.Instance.FadeOut());
		}
	}

	void Interaction(string person)
	{
		if (person == "Grandma")
		{
			grandma.gameObject.SetActive(true);
		}
		else if (person == "Mum")
		{
			mum.gameObject.SetActive(true);
		}
		else if (person == "Sister")
		{
			sister.gameObject.SetActive(true);
		}
		else if (person == "Flyer")
		{
			flyer.gameObject.SetActive(true);
		}
		speechBubble.gameObject.SetActive(true);
		inInteraction = true;
		string saying = "";

		//randomise what person is saying
		string st = "bcMdJI ZGHBoq rAhijk FDltux ";
		for (int i = 0; i < 10; i++)
		{
			char c = st[Random.Range(0, st.Length)];
			saying = saying.Insert(0, c.ToString());
		}
		speaking.text = saying;

		StartCoroutine(ListenToPerson());
	}

	void TurnOffCanvas()
	{
		grandma.gameObject.SetActive(false);
		mum.gameObject.SetActive(false);
		sister.gameObject.SetActive(false);
		flyer.gameObject.SetActive(false);
		speechBubble.gameObject.SetActive(false);
	}


}
