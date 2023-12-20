using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Chest : MonoBehaviour
{
	[SerializeField]
	Transform lockParent;
	[SerializeField]
	GameObject lockPrefab;

	public GameObject deletePanel;

	public Button buttonChest;
    public char chestName = ' ';
	public List<char> keyLock = new List<char>();
    public char keyLoot = ' ';
    public bool condition;
	public bool opened;

	public TMPro.TMP_Text textName;
	[SerializeField] Animator animator;

	GameManager gameManager;

	private bool willBeRemoved;
	private void Awake()
	{
		gameManager = FindObjectOfType<GameManager>();
		gameManager.deleteChest.AddListener(EnableRemove);
		condition = true;
	}

	public void InitializeUI()
	{
		Debug.Log("init ui");
		foreach (Transform child in lockParent)
		{
			Destroy(child.gameObject);
			Debug.Log("remove ui");
		}

		if (!condition)
		{
			lockParent.parent.gameObject.SetActive(false);
			return;
		}

		foreach (char c in keyLock)
		{
			GameObject go = Instantiate(lockPrefab, lockParent);
			go.transform.GetComponentInChildren<TMPro.TMP_Text>().text = c.ToString();
		}
	}

	public void OnClick()
	{
		if (!willBeRemoved)
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
		else
		{
			RemoveChest();
		}
	}

	public void RemoveChest()
	{
		gameManager.RemoveChest(this);
	}

	private void EnableRemove(bool value)
	{
		willBeRemoved = value;
	}
	
}
