using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chest : MonoBehaviour
{
    public char chestName = ' ';
    public char keyLoot = ' ';
    public bool condition;
	bool opened;

	public TMPro.TMP_Text textName;

	GameManager gameManager;

	private void Awake()
	{
		gameManager = FindObjectOfType<GameManager>();
		condition = true;
	}

	public void OnClick()
	{
        if((condition && gameManager.inventoryKey.Contains(chestName)) || !condition)
		{
			if(keyLoot != ' ' && !opened)
			{
				gameManager.inventoryKey.Add(keyLoot);
				opened = true;
			}
		}
	}
}
