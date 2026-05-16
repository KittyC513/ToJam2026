using UnityEngine;

public class SlicePiece : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public GameObject outline;

    public float resetTimer = 0.3f;

    public bool isHovering;

    public float timer = 0.3f;

    public int sliceIndex;

    private bool dragging;

    private Vector3 offset;

    public bool isOnCutting;

    public float cheeseSize;

    public TargetCheese targetCheese;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
        outline.SetActive(false);
        GameManager.Instance.sliceList.Add(this);
       

        sliceIndex = GameManager.Instance.sliceList.Count - 1;
        
        //if (GameManager.Instance.cheeseList.Count < 1) return;
        //    cheeseSize = GameManager.Instance.cheeseList[sliceIndex].cheeseSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isSelected && !isHovering)
        {
            outline.SetActive(false);
        }

        if(cheeseSize == 0)
        {
            if(sliceIndex == 0)
                cheeseSize = GameManager.Instance.cheeseList[0].cheeseSize;
            else if(sliceIndex == 1)
                cheeseSize = 1 - GameManager.Instance.cheeseList[0].cheeseSize;
        }

        //Vector3 mouseWorld =
        //    Camera.main.ScreenToWorldPoint(
        //        Input.mousePosition
        //    );

        //mouseWorld.z = 0;

        //// START DRAG
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(mouseWorld,Vector2.zero);

        //    if (hit.collider != null &&
        //       hit.collider.gameObject == gameObject)
        //    {
        //        dragging = true;

        //        offset = transform.position - mouseWorld;
        //    }
        //}

        //// DRAGGING
        //if (dragging)
        //{
        //    transform.position = mouseWorld + offset;

        //}

        //// STOP DRAG
        //if (Input.GetMouseButtonUp(0))
        //{
        //    dragging = false;
        //}
    }

    private void OnMouseEnter()
    {
        isHovering = true;
        outline.SetActive(true);
        Debug.Log("Hover");
    }

    private void OnMouseExit()
    {
        isHovering = false;
        Debug.Log("Exit");
    }

    private void OnMouseDown()
    {
        
        //if(isHovering && timer >= resetTimer)
        //{
        //    GameManager.Instance.IsSelected(sliceIndex);
        //    timer = 0;
        //    GameEvents.OnSecondCut?.Invoke();
        //    GameManager.Instance.cheeseList[0].cheeseSize = cheeseSize;
        //    GameManager.Instance.cheeseList[1].cheeseImage.enabled = false;

        //    if (isOnCutting)
        //    {
        //        GameEvents.OnCheeseCut();
        //    }
        //}

    }

}
