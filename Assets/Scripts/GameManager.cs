using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

	[Space] [Header("Chest Suppression")]
	public Image buttonRemoveImage;
	[HideInInspector] public UnityEvent<bool> deleteChest;
	private bool willRemoveChest = false;
	public void Initialize()
	{
		//Reset Remove Chest Button Color
		willRemoveChest = false;
	    buttonRemoveImage.color = willRemoveChest ? Color.gray : Color.white;
	    
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

	    if (inventoryKey.Count > 0)
	    {
		    inventoryKey.Clear();
	    }
	    
	    //Create chests
	    for (int i = 0; i < nbChests; i++)
	    {
		    Chest chest = Instantiate(prefabChest, chestPanelTransform);
		    listChest.Add(chest.gameObject);
		    chests.Add(chest);
	    }

	    //Name attribution 
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



	    RefreshInfoChests();
    }

	private void RefreshInfoChests()
	{
		if (listChestRouteInfo.Count > 0)
		{
			foreach (var go in listChestRouteInfo)
			{
				Destroy(go);
			}
			listChestRouteInfo.Clear();
		}

		foreach (var chestGo in listChest)
		{
			chestGo.GetComponent<Chest>().keyLock.Clear();
		}

		//Get main chest route that will have all the new branchs  
		ChestRoute mainChestRoute = routes.OrderByDescending(route => route.route.Count).First();

		List<ChestRoute> otherRoutes = new List<ChestRoute>(routes);
		otherRoutes.Remove(mainChestRoute);

		//Get the count of the other routes and make them linked to the +1 of the main route (same index if the other route has the same length as the main one)
		foreach (var route in otherRoutes)
		{
			if(route.route.Count > 0)
			{
				if (route.route.Count < mainChestRoute.route.Count)
				{
					mainChestRoute.route[route.route.Count].keyLock.Add(route.route[^1].chestName);
				}
				else
				{
					mainChestRoute.route[^1].keyLock.Add(route.route[^1].chestName);
				}
			}
		}

		foreach (var route in routes)
		{
			if (route.route.Count > 0)
			{
				UIChestRouteInfo uiInfoChest = Instantiate(prefabChestRouteInfo, ordersChestTransform);
				listChestRouteInfo.Add(uiInfoChest.gameObject);
		    
				string chestRoute = "";
		    
				route.route[0].condition = false;
				for (int i = 0; i < route.route.Count; i++)
				{
					if(i < route.route.Count)
						route.route[i].keyLoot = route.route[i].chestName;

					if (route.route[i].condition)
						route.route[i].keyLock.Add(route.route[i - 1].keyLoot);

					route.route[i].InitializeUI();

					if (route.route[i].keyLock.Count > 1)
					{
						foreach (var key in route.route[i].keyLock)
						{
							if (!chestRoute.Contains(key))
							{
								chestRoute += key;
							}
						}
					}
					chestRoute += route.route[i].chestName;
				}
		    
				uiInfoChest.Initialize(chestRoute);
			}
		}
	}
	
	//Remove chest from the pool and update the game 
	public void RemoveChest(Chest chest)
	{
		List<GameObject> otherListChest = new List<GameObject>(listChest);
		otherListChest.Remove(chest.gameObject);
		
		//Remove keyLock index of selected chest from other chests
		foreach (var otherChest in otherListChest)
		{
			Chest chestObj = otherChest.GetComponent<Chest>();
			if (chestObj.keyLock.Contains(chest.chestName))
			{
				chestObj.keyLock.Remove(chest.chestName);
			}
		}

		//Remove chest from route
		foreach (var route in routes)
		{
			if (route.route.Contains(chest))
			{
				route.route.Remove(chest);
			}
		}
		
		//Remove chest from listChest
		listChest.Remove(chest.gameObject);
		
		RefreshInfoChests();
				
		//Remove chest from the game 
		Destroy(chest.gameObject);
	}

	public void WillRemoveChest()
	{
		deleteChest.Invoke(!willRemoveChest);
		willRemoveChest = !willRemoveChest;
		buttonRemoveImage.color = willRemoveChest ? Color.gray : Color.white;
	}
}

[System.Serializable]
public class ChestRoute
{
    public List<Chest> route = new List<Chest>();
}
