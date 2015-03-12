using UnityEngine;
using System.Collections;

public class ButtonBackToMenu : MonoBehaviour {

	public void Press()
	{
		Application.LoadLevel (0);
	}
}
