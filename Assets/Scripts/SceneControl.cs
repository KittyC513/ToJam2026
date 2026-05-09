using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    public static SceneControl instance;

    public GameObject cheeseObj;

    public Transform canvas;

    public List<GameObject> cheeseList = new List<GameObject>();

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
        cheeseList.Add(Instantiate(cheeseObj, canvas));
        Transform cuttingLine = cheeseList[cheeseList.Count - 1].transform.Find("CuttingLine");

        CuttingFeature cuttingFeature = cheeseList[cheeseList.Count - 1].GetComponent<CuttingFeature>();
        if (cuttingFeature == null) return;

        cuttingFeature.cheeseSize = 1 - cheeseList[cheeseList.Count - 2].GetComponent<CuttingFeature>().cheeseSize;

        cuttingFeature.radialBar = cuttingFeature.cheessBar;
        cuttingFeature.fillBar.enabled = false;

        cuttingFeature.radialBar.fillAmount = cuttingFeature.radialBar.fillAmount;
        cuttingFeature.fill = cuttingFeature.radialBar.rectTransform;

        cuttingFeature.radialBar.fillAmount = cuttingFeature.cutAmount;

        cuttingFeature.cheeseSize = cuttingFeature.cheessBar.fillAmount;

    }
}
