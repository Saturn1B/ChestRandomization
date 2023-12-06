using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIChestRouteInfo : MonoBehaviour
{
    public TextMeshProUGUI routeText;
    [SerializeField]
    private Button buttonClipboard;

    public void Initialize(string text)
    {
        routeText.text = text;
        buttonClipboard.onClick.AddListener(CopyToClipboard);
    }

    public void CopyToClipboard()
    {
        GUIUtility.systemCopyBuffer = routeText.text;
    }
    
}
