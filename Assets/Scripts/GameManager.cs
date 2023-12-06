using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Chest> chests = new List<Chest>();
    public Chest[] chestsTotal;
    public List<char> keys = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
    public List<char> names = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

    public List<ChestRoute> routes = new List<ChestRoute>();

    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < chests.Count; i++)
		{
            chests[i].chestName = names[i];
		}

        int r = Random.Range(2, 5);

		for (int i = 0; i < r; i++)
		{
            ChestRoute cr = new ChestRoute();
            routes.Add(cr);
		}

        int totalChest = 10;
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
                Debug.Log(c2 + " | " + chests.Count);
                routes[i].route.Add(chests[c2]);
                chests.RemoveAt(c2);
			}
        }

		foreach (var route in routes)
		{
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
