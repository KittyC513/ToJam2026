using UnityEngine;
using UnityEngine.UI;

public class GenerateNewSlice : MonoBehaviour
{
    public Image radialBar;
    public RectTransform cheese;

    public CuttingFeature CuttingFeature;
    public float cheeseSize;

    public float outwardForce = 60f;

    public int cheeseIndex;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewSlice(Vector3 localScale)
    {
        Vector2 outward = Vector2.zero;

        cheeseSize = 1 - CuttingFeature.cutAmount;
       
        if (cheeseSize < 0.5f)
        {
            outward = -cheese.up * outwardForce + cheese.right * outwardForce;
            
            if(cheeseSize < 0.25f)
            {
                outward = -cheese.up * outwardForce * 1.5f;
            }

        }
        else if (cheeseSize > 0.5f)
        {
            outward = cheese.up * outwardForce + cheese.right * outwardForce;
            
            //if(cheeseSize > 0.75f)
            //{
            //    outward = cheese.up * outwardForce * 1.5f;
            //}
        }
        else
        {
            outward = -cheese.right * outwardForce;
        }

        cheese.localScale = new Vector3(-1, 1, 1);
        cheese.anchoredPosition += outward;

        radialBar.fillAmount = cheeseSize;

        CheeseManager.Instance.cheeseList.Add(new CheeseData(cheeseSize, 1));
        CheeseManager.Instance.sliceCount++;



    }

}
