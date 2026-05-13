using UnityEngine;

public class WedgeShader : MonoBehaviour
{
    
    public Material m_Material;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public float ConstrainShader
    {
        get { return sliderValue; }
        set { sliderValue = Mathf.Clamp(value, 0, 1); }
    }

    private float sliderValue;


}
