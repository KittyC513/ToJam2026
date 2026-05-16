using UnityEngine;
using UnityEngine.UI;

public class GenerateNewSlice : MonoBehaviour
{
    public Image radialBar;
    public RectTransform cheese;

    public CuttingFeature CuttingFeature;
    public float cutAmount;

    public float outwardForce = 60f;


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

        cutAmount = 1 - CuttingFeature.cutAmount;
       
        if (cutAmount < 0.5f)
        {
            outward = -cheese.up * outwardForce + cheese.right * outwardForce;
            
            if(cutAmount < 0.25f)
            {
                outward = -cheese.up * outwardForce * 2f;
            }

        }
        else if (cutAmount > 0.5f)
        {
            outward = cheese.up * outwardForce + cheese.right * outwardForce;
            
            if(cutAmount > 0.75f)
            {
                outward = cheese.up * outwardForce * 2f;
            }
        }
        else
        {
            outward = -cheese.right * outwardForce;
        }

        cheese.localScale = new Vector3(-1, 1, 1);
        cheese.anchoredPosition += outward;

        radialBar.fillAmount = cutAmount;

    }

}
