using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chest : MonoBehaviour
{
    public char chestName = ' ';
    public char keyLoot = ' ';
    public bool condition = true;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
}
