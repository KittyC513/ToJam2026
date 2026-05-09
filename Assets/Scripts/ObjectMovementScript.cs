using UnityEngine;

public class ObjectMovementScript : MonoBehaviour
{

    public bool isGrabbable = false;
    public bool isSelected = false;
    private Vector2 origPivot = Vector2.zero;
    private Vector2 targetPivot = Vector2.zero;
    public Rigidbody2D thisRB;
    public float forceEmit = 5f;
    public float dampen = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origPivot = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = new Vector3(targetPivot.x, targetPivot.y, 0);
            Vector3 truePos = new Vector3(thisRB.position.x - offset.x,thisRB.position.y - offset.y,0);
            Vector2 direction = (mousePosition - truePos);
            Vector2 dampener = -thisRB.linearVelocity * dampen;


            thisRB.AddForce((direction * forceEmit)+ dampener, ForceMode2D.Force);
        }
    }

    void OnMouseDown()
    {
        if (isGrabbable)
        {
            isSelected = true;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 worldPosition = this.transform.InverseTransformPoint(mousePosition);
            targetPivot.x = worldPosition.x;
            targetPivot.y = worldPosition.y;
        }
    }

    private void OnMouseUp()
    {
        if (isSelected)
        {
            isSelected = false;
            targetPivot = origPivot;
        }
    }

    void OnMouseEnter()
    {
        if(!isSelected)
        {
            Debug.Log("Mouse is here!");
            isGrabbable = true;
        }

    }

    void OnMouseExit()
    {
        if (!isSelected)
        {
            Debug.Log("Mouse is no longer here!");
            isGrabbable = false;
        }
    }

}
