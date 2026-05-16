using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CuttingFeature : MonoBehaviour, IDragHandler
{
    public bool isCutting;
    public RectTransform line;
    public float cutSpeed = 20f;
    public Image radialBar;
    public Image cheessBar;
    public Image fillBar;
    public RectTransform cheese;
    public RectTransform staticLine;
    public RectTransform cuttingLine;
    private Transform originalPos;
    public Color radialBarColor;

    public RectTransform fill;

    //public RectTransform fill;
    public float cutAmount = 1;

    public float cheeseSize = 1;

    public int cheeseIndex;

    public float outwardForce = 60f;

    public float radius;

    public bool isSecondCut;

    public Image cheeseImage;

    public SlotScript slotScript;

    public GameObject newSlice;

    private void OnEnable()
    {

    }
    private void Start()
    {

        GameManager.Instance.cheeseList.Add(this);
        cheeseIndex = GameManager.Instance.sliceCount;
        GameManager.Instance.sliceCount += 1;

        originalPos = cuttingLine.transform;
        radialBar = fillBar;

    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //Initial setup for the cutting process, enable the fillBar and set its color to a semi-transparent version of its original color
        if (!fillBar.enabled)
            fillBar.enabled = true;

        radialBar.fillAmount = cutAmount;
        radialBar = fillBar;
        radialBarColor = radialBar.color;
        radialBarColor.a = 0.25f;
        radialBar.color = radialBarColor;
        fill = radialBar.rectTransform;


        isCutting = true;
        // Calculate the direction from the line to the mouse position
        Vector2 direction = eventData.position - (Vector2)RectTransformUtility.WorldToScreenPoint(null, line.position);

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust the angle to account for the initial orientation of the line
        angle -= 90f;

        //convert negative angles
        if(angle< 0)
        {
            angle += 360f;
        }

        // Smoothly rotate the line towards the target angle
        Quaternion target = Quaternion.Euler(0, 0, angle);

        line.rotation = Quaternion.Lerp(line.rotation, target, Time.deltaTime * cutSpeed);

    }
     void Update()
    {
        if(isCutting && radialBar != null) 
            Cut();

        //if (isSecondCut)
        //{
        //    cheeseSize = SceneControl.instance.targetCheese.cheeseSize;
        //    radialBar = cheessBar;
        //    radialBar.fillAmount = radialBar.fillAmount;
        //    fill = radialBar.rectTransform;
            
        //    foreach (var cheese in GameManager.Instance.cheeseList)
        //    {
        //        if (!GameManager.Instance.cheeseList[cheeseIndex])
        //        {
        //            cheeseImage.enabled = false;
        //            Debug.Log("Cheese " + cheeseIndex + " deactivated");
        //        }
        //    }

        //    staticLine.GetComponent<Image>().enabled = true;
        //    cuttingLine.GetComponent<Image>().enabled = true;
        //    isSecondCut = false;
        //}
    }

    void Cut()
    {

        // Calculate the angle difference between the static line and the cutting line
        float staticAngle = staticLine.eulerAngles.z;
        float cuttingAngle = cuttingLine.eulerAngles.z - 180f;

        float angleDifference = (staticAngle - cuttingAngle + 360f) % 360f;

        if (angleDifference <= 10)
        {
            //Reset();
            return;
        }
        radius = angleDifference;
        radialBar = fillBar;
        //radialBar.fillAmount = 1 - (angleDifference / 360);
        radialBar.fillAmount = angleDifference / 360;
        //Cut Amount% = Fill Amount
        cutAmount = radialBar.fillAmount;
        

        // If the mouse left button is released, consider the cut is done
        if (Input.GetMouseButtonUp(0))
        {
            //calculateHalves(cutAmount);
            CheeseCutCheck();
            isCutting = false;
        }
        

    }

    void Reset()
    {
        if(cutAmount >= 0.9f)
        {
            line.rotation = Quaternion.Lerp(line.rotation, originalPos.rotation, Time.deltaTime * cutSpeed);
            radialBar.fillAmount = cutAmount;
        }
    }

    void CheeseCutCheck()
    {
        if (cutAmount >= cheeseSize)
        {
            //Reset();
        }
        else
        {
            if (fillBar != null) {
            radialBar = cheessBar;
            fillBar.enabled = false;

            radialBar.fillAmount = radialBar.fillAmount;
            fill = radialBar.rectTransform;

            radialBar.fillAmount = cutAmount;

            cheeseSize = cheessBar.fillAmount;

            //cuttingLine.gameObject.SetActive(false);
            //staticLine.gameObject.SetActive(false);

            staticLine.GetComponent<Image>().enabled = false;
            cuttingLine.GetComponent<Image>().enabled = false;
            GenerateNewSlice();
                //SceneControl.instance.SliceCheese(1);
            }
        }
    }

    void GenerateNewSlice() 
    {
        newSlice.SetActive(true);
        GenerateNewSlice newSliceScript = newSlice.GetComponent<GenerateNewSlice>();
        newSliceScript.NewSlice(cheese.localScale);

        StartCoroutine(OnCut());

        //print("Generating new slice");
        //Vector2 outward = Vector2.zero;
        //radialBar = cheessBar;
        //fillBar.enabled = false;

        //radialBar.fillAmount = radialBar.fillAmount;
        //fill = radialBar.rectTransform;

        //if(GameManager.Instance.cheeseList.Count > 1)
        //{
        //    cutAmount = cheeseSize - GameManager.Instance.cheeseList[cheeseIndex].cheeseSize;
        //    Debug.Log("Cheese Size: " + cheeseSize);
        //    Debug.Log("Previous Cheese Size: " + GameManager.Instance.cheeseList[cheeseIndex].cheeseSize);
        //    Debug.Log("Cut Amount: " + cutAmount);

        //    if (cutAmount < 0.5f)
        //    {
        //        outward = -cheese.up * outwardForce + cheese.right * outwardForce;
        //    }
        //    else if (cutAmount > 0.5f)
        //    {
        //        outward = cheese.up * outwardForce + cheese.right * outwardForce;
        //    }
        //    else 
        //    {
        //        outward = -cheese.right * outwardForce;
        //    }

        //    cheese.localScale = new Vector3(-1, 1, 1);
        //    cheese.anchoredPosition += outward;


        //    staticLine.GetComponent<Image>().enabled = false;
        //    cuttingLine.GetComponent<Image>().enabled = false;

        //}



        //radialBar.fillAmount = cutAmount;

        //cheeseSize = cheessBar.fillAmount;

        //radius = cheeseSize * 360f;

    }

    public void calculateHalves(float cutAmount) 
    { 
        float remainder = 1 - cutAmount;
        if (cutAmount > remainder)
        {
            slotScript.setCutSize(cutAmount, remainder);
        }
        else if(cutAmount <= remainder)
        {
            slotScript.setCutSize(remainder, cutAmount);
        }
    }

    IEnumerator OnCut()
    {
        yield return new WaitForSeconds(0.5f);
        EventManager.isCutting = false;
        print("Cutting mode: OFF");
    }
}
