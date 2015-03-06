using UnityEngine;
using System.Collections;

public class LvlButtonHandler : MonoBehaviour {

	public void LoadLvl(int lvl)
	{
		Application.LoadLevel (lvl);
	}
	
}
