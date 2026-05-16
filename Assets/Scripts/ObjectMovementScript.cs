using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectMovementScript : MonoBehaviour
{

    public float currentWedgeSize;

    public bool isGrabbable = false;
    public bool isSelected = false;
    private Vector2 origPivot = Vector2.zero;
    private Vector2 targetPivot = Vector2.zero;
    public Rigidbody2D thisRB;
    public float forceEmit = 5f;
    public float dampen = 1f;

    public bool isSold = false;

    [Tooltip("Swiss = 1 // Brie = 2 // Weight1 = 50 // Weight2 = 100 // weight3 = 250 // weight4 = 500")]
    // Swiss = 1 // Brie = 2 // Weight1 = 50 // Weight2 = 100 // weight3 = 250 // weight4 = 500
    public int objectType;
    //
    public bool submitToTaker = false;
    public bool beingSlotted = false;
    public SlotScript controlTaken;


    // -----
    void Start()
    {
        origPivot = transform.localPosition;
    }

    // -----
    void Update()
    {
        if (isSelected)
        {

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           // Vector3 offset = new Vector3(targetPivot.x, targetPivot.y, 0);
            Vector3 truePos = new Vector3(thisRB.position.x,thisRB.position.y,0);
            Vector2 direction = (mousePosition - truePos);
            Vector2 dampener = -thisRB.linearVelocity * dampen;


            thisRB.AddForce((direction * forceEmit)+ dampener, ForceMode2D.Force);
        }
        else if (controlTaken != null && !isSelected && beingSlotted)
        {
            Debug.Log("5 Alert");
            Vector2 targetPos = controlTaken.gameObject.transform.position;
            
            Vector2 dampener = -thisRB.linearVelocity * dampen;
            Vector2 currentPos = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 threshHold = new Vector2(.01f, .01f);
            Vector2 threshHoldTwo = new Vector2(.3f,.3f);

            thisRB.AddForce(((targetPos - currentPos) * forceEmit) + dampener, ForceMode2D.Force);

            if (targetPos.x - currentPos.x < threshHold.x && targetPos.x - currentPos.x > -threshHold.x && targetPos.y - currentPos.y < threshHold.y && targetPos.y - currentPos.y > -threshHold.y)
            {
                targetPos = currentPos;
                isGrabbable = true;
                thisRB.constraints = RigidbodyConstraints2D.FreezeAll;
                beingSlotted = false;
            }
            else if (targetPos.x - currentPos.x < threshHoldTwo.x && targetPos.x - currentPos.x > -threshHoldTwo.x && targetPos.y - currentPos.y < threshHoldTwo.y && targetPos.y - currentPos.y > -threshHoldTwo.y)
            {
                thisRB.MovePosition(targetPos);
            }


        }

    }

    void OnMouseDown()
    {
        if (isGrabbable)
        {
            takeHold();
        }
    }

    private void OnMouseUp()
    {
        if (beingSlotted == true)
        {
            Debug.Log("LUP1 Alert");
            isSelected = false;
            submitToTaker = true;
            targetPivot = origPivot;
        }

        else if (isSelected)
                {
            Debug.Log("LUP3 Alert");
            isSelected = false;
            targetPivot = origPivot;
                }
    }

    void OnMouseEnter()
    {
        if(!isSelected)
        {
//            Debug.Log("Mouse is here!");
            isGrabbable = true;
        }

    }

    void OnMouseExit()
    {
        if (!isSelected)
        {
//            Debug.Log("Mouse is no longer here!");
            isGrabbable = false;
        }
    }

    void takeHold()
    {
        isSelected = true;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       // Vector3 worldPosition = this.transform.InverseTransformPoint(mousePosition);
        targetPivot.x = mousePosition.x;
        targetPivot.y = mousePosition.y;

        thisRB.constraints = RigidbodyConstraints2D.None;

        if (controlTaken != null)
        {
            if(controlTaken.objectType == 25)
            {
                controlTaken.wasCloned = false;
                controlTaken.controllerCut.newRemoved = true;
            }
            controlTaken.objectLeft(this.gameObject);
            controlTaken = null;

        }

        submitToTaker = false;
        beingSlotted = false;
    }

    public void GetSlotted(SlotScript slot)
    {
        Debug.Log("9 Alert");
        isSelected = false;
        controlTaken = slot;
    }

    public void SelfDestruct()
    {
        DestroyImmediate(this.gameObject);
    }

}
