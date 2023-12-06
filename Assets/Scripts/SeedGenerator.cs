using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SeedGenerator : MonoBehaviour
{
    [Space]
    [Header("UI Elements")]
    [SerializeField] TMP_InputField seedInputField;
    [SerializeField] TextMeshProUGUI placeHolderSeedText;
    [SerializeField] TextMeshProUGUI seedName;
    
    private int currentSeed;


    public void SetRandomSeed()
    {
        if (seedInputField.text != "")
        {
            try 
            {
                currentSeed = System.Int32.Parse(seedInputField.text);
            }
            catch 
            {
                currentSeed = seedInputField.text.GetHashCode();
            }
        }
        else
        {
            currentSeed = (int) DateTime.Now.Ticks;
        }
        
        Random.InitState(currentSeed);
        placeHolderSeedText.text = currentSeed.ToString();
    }
}
