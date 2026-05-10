using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<CuttingFeature> cheeseList = new List<CuttingFeature>();

    public List<SlicePiece> sliceList = new List<SlicePiece>();

    public int cheeseIndex = 0;

    public bool isSelected;

    public int sliceCount = 0;

    private static GameManager instance;

    public float requiredCutAmount = 0.5f;

    public float slice_leftSize;
    public float slice_rightSize;

    //public float slide_leftSize;

    //public float slide_rightSize;
    public static GameManager Instance
    {
        get { return instance; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IsSelected(int index)
    {
        isSelected = true;
    }

    public void Deselect(int index)
    {
        isSelected = false;
    }

    public void ResetGame()
    {
        cheeseList.Clear();
        sliceList.Clear();
        cheeseIndex = 0;
        sliceCount = 0;
        isSelected = false;
    }

    public void NextCheese()
    {
        for (int i = 0; i < cheeseList.Count - 1; i++)
        {
            cheeseList[i].gameObject.SetActive(false);
        }
    }

    public void SliceSize()
    {
        sliceList[0].cheeseSize = slice_leftSize;
        sliceList[1].cheeseSize = slice_rightSize;
    }
}
