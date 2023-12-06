using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[Header("UI")] 
	[SerializeField]
	private Transform ordersChestTransform;
	[SerializeField]
	private UIChestRouteInfo prefabChestRouteInfo;
	private List<GameObject> listChestRouteInfo = new List<GameObject>();
	
	[Space]
	
	[SerializeField] 
	private Transform chestPanelTransform;
	[SerializeField]
	private Chest prefabChest;
	private List<GameObject> listChest = new List<GameObject>();

	[Space] [Header("Random")]
	public int nbChests;
    [HideInInspector] 
    public List<Chest> chests = new List<Chest>();
    public List<char> keys = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
    public List<char> names = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

    public List<ChestRoute> routes = new List<ChestRoute>();

	public List<char> inventoryKey = new List<char>();


	public void Initialize()
    {
	    if (routes.Count > 0)
	    {
		    routes.Clear();
	    }
	    
	    if (listChest.Count > 0)
	    {
		    foreach (var go in listChest)
		    {
			    Destroy(go);
		    }
		    listChest.Clear();
		    chests.Clear();
	    }

	    if (listChestRouteInfo.Count > 0)
	    {
		    foreach (var go in listChestRouteInfo)
		    {
			    Destroy(go);
		    }
		    listChestRouteInfo.Clear();
	    }
	    
	    //Create chests
	    for (int i = 0; i < nbChests; i++)
	    {
		    Chest chest = Instantiate(prefabChest, chestPanelTransform);
		    listChest.Add(chest.gameObject);
		    chests.Add(chest);
	    }

	    List<char> listName = new List<char>(names);
	    for (int i = 0; i < chests.Count; i++)
	    {
		    int rdm = Random.Range(0, listName.Count);
		    chests[i].chestName = listName[rdm];
			chests[i].textName.text = listName[rdm].ToString();
			listName.RemoveAt(rdm);
	    }

	    int r = Random.Range(2, 5);

	    for (int i = 0; i < r; i++)
	    {
		    ChestRoute cr = new ChestRoute();
		    routes.Add(cr);
	    }

	    int totalChest = nbChests;
	    for (int i = 0; i < routes.Count; i++)
	    {
		    int c = 0;
		    if (i == routes.Count - 1)
			    c = totalChest;
		    else
			    c = Random.Range(2, (totalChest / 2)-1);

		    while (routes[i].route.Count < c && chests.Count > 0)
		    {
			    int c2 = Random.Range(0, chests.Count);
			    routes[i].route.Add(chests[c2]);
			    chests.RemoveAt(c2);
		    }
	    }

	    foreach (var route in routes)
	    {
		    UIChestRouteInfo uiInfoChest = Instantiate(prefabChestRouteInfo, ordersChestTransform);
		    //TODO Remplir initialize avec l'ordre des coffres par route
		    listChestRouteInfo.Add(uiInfoChest.gameObject);
		    uiInfoChest.Initialize(route.ToString());
			
		    route.route[0].condition = false;
		    for (int i = 0; i < route.route.Count; i++)
		    {
			    if(i < route.route.Count - 1)
				    route.route[i].keyLoot = route.route[i + 1].chestName;
		    }
	    }
    }
}

[System.Serializable]
public class ChestRoute
{
    public List<Chest> route = new List<Chest>();
}
