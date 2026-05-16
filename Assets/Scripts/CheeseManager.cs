using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CheeseManager : MonoBehaviour
{
    public static CheeseManager Instance { get; private set; }
    public List<CheeseData> cheeseList = new List<CheeseData>();
    public int sliceCount;

    [ContextMenu("Check Cheese Data")]
    public void CheckCheeseData()
    {
        foreach (CheeseData cheese in cheeseList)
        {
            Debug.Log(cheese.cheeseSize + " " +  cheese.cheeseType);
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCheese(GameObject cheesePrefab, Transform location)
    {

        GameObject cheeseObj = Instantiate(cheesePrefab.gameObject, location);
        cheeseList.Add(cheesePrefab.GetComponent<CheeseData>());
    }



}
