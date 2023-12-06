using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chest : MonoBehaviour
{
    public char chestName = ' ';
    public char keyLoot = ' ';
    public bool condition = true;

    public void Initialize(char name, char keyLoot = ' ')
    {
        chestName = name;
        this.keyLoot = keyLoot;
       
    }
}
