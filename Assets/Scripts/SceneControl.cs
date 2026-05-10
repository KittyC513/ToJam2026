using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    public static SceneControl instance;

    public GameObject cheeseObj;

    public Transform canvas;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SliceCheese(int caseNum)
    {
        switch(caseNum)
        {
            case 1:
                cheeseObj = Resources.Load<GameObject>("Prefabs/Slider_cheese");
                break;
            case 2:
                cheeseObj = Resources.Load<GameObject>("Prefabs/Cheese2");
                break;
            case 3:
                cheeseObj = Resources.Load<GameObject>("Prefabs/Cheese3");
                break;
            case 4:
                cheeseObj = Resources.Load<GameObject>("Prefabs/Cheese3");
                break;

        }
        GameObject slice = Instantiate(cheeseObj, canvas);
    }
}
