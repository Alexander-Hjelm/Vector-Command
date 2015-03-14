using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class LvlGenerator : MonoBehaviour {

	public GameObject lvlButtonPrefab;
	public GameObject canvas;

	public int numberOfLvls = 11;

	// Use this for initialization
	void Start () {

		for (int i = 1; i <= 5; i++)
		{
			for (int j = 1; j <= 4; j++)
			{
				if((i-1)*4 +j <= numberOfLvls)
				{
					GameObject button = Instantiate(lvlButtonPrefab) as GameObject;
					button.transform.SetParent(canvas.transform);
					button.GetComponent<RectTransform>().position = new Vector3(100 + 100*j, 600 - 100*i, 0);
					int lvl = (i-1)*4 +j;
					button.GetComponentInChildren<Text>().text = (lvl).ToString();
					button.GetComponent<Button>().onClick.RemoveAllListeners();
					button.GetComponent<Button>().onClick.AddListener(delegate{LoadLvl(lvl);});
				}
			}
		}
	}

	public void LoadLvl(int lvl)
	{
		Application.LoadLevel (lvl);
	}
}
