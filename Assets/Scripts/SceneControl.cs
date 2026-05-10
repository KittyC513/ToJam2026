using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    public static SceneControl instance;

    public GameObject cheeseObj;

    public Transform canvas;

    public SpriteRenderer piece_left;
    public SpriteRenderer piece_right;
    public SpriteRenderer piece_left_outline;
    public SpriteRenderer piece_right_outline;

    public TargetCheese targetCheese;
    public CuttingFeature cuttingFeature;

    public GameObject sliceObj;

    public bool isSecondCut;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        GameEvents.OnCheeseCut += OnCheeseCut;
        GameEvents.OnCuttingBoard += OnCuttingBoard;
        GameEvents.OnCheeseCut?.Invoke();
        GameEvents.OnSecondCut += OnSecondCut;
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
        sliceObj = Instantiate(cheeseObj, canvas);
        cuttingFeature = sliceObj.GetComponent<CuttingFeature>();

        if (!isSecondCut)
            GameEvents.OnCuttingBoard?.Invoke();
    }

    public void OnCheeseCut()
    {
        canvas.gameObject.SetActive(true);
        piece_left.enabled = false;
        piece_right.enabled = false;
        piece_left_outline.enabled = false;
        piece_right_outline.enabled = false;
    }

    public void OnSecondCut()
    {
        isSecondCut = true;
        StartCoroutine(TransitionToCuttingPhase(0.3f));
    }
    public void OnCuttingBoard()
    {
        StartCoroutine(WaitForCut(0.5f));

    }
    IEnumerator WaitForCut(float timer)
    {
        yield return new WaitForSeconds(timer);
        GameManager.Instance.SliceSize();
        yield return new WaitForSeconds(timer);

        piece_left.enabled = true;
        piece_right.enabled = true;
        piece_left_outline.enabled = true;
        piece_right_outline.enabled = true;

        canvas.gameObject.SetActive(false);
    }

    IEnumerator TransitionToCuttingPhase(float timer)
    {
        yield return new WaitForSeconds(timer);
        canvas.gameObject.SetActive(true);
        //SliceCheese(1);
        piece_left.enabled = false;
        piece_right.enabled = false;
        piece_left_outline.enabled = false;
        piece_right_outline.enabled = false;
        isSecondCut = false;
    }
}
