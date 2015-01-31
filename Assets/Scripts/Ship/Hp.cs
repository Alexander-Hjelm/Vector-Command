using UnityEngine;
using System.Collections;

public class Hp : MonoBehaviour {

	public int hp;
	public int maxHp;

	void Start()
	{
		hp = maxHp;
	}

	public void modHp(int factor)
	{
		hp += factor;
	}
}
