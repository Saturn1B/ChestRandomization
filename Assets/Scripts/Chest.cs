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
	[SerializeField] Animator animator;

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
			if(!opened)
			{
				animator.SetBool("open", true);
				opened = true;

				if (keyLoot != ' ')
				gameManager.inventoryKey.Add(keyLoot);
			}
		}
	}
}
