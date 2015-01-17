using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseHp : MonoBehaviour {
	
	public Image hpBarImgScript;	//for adjusting fill of hp bar
	
	public int hp;
	public int maxHp;
	
	void Start()
	{
		//(refs
		hpBarImgScript = transform.FindChild ("Canvas/Green").GetComponent<Image>();
		
		hp = maxHp;
	}
	
	public void modHp(int factor)
	{
		hp += factor;
		hpBarImgScript.fillAmount = (float)hp/(float)maxHp;
	}
}