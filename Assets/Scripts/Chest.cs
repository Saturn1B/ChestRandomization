using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chest : MonoBehaviour
{
    public char chestName = ' ';
	public List<char> keyLock = new List<char>();
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
		bool hasKeys = true;
		foreach (char c in keyLock)
		{
			if (!gameManager.inventoryKey.Contains(c))
			{
				hasKeys = false;
			}
		}

        if((condition && hasKeys) || !condition)
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
