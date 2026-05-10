using UnityEngine;
using UnityEngine.Events;

public class SlotScript : MonoBehaviour
{
    public UnityEvent<int> slotOpened;

    public GameObject heldItem = null;
    public GameObject wantsToBeHeld;
    // Swiss = 1 // Brie = 2
    public int cheeseType = 1;
    public int selfIndex;
    public bool isWheel = false;

    public ShelfSlots ShelfSlots;
    public CircleCollider2D selfCollider;

    // To do: Invoke listener event when a slot opens, throwing an int for the keypoint type. Add logic to object script to grab item name. Actual slotting of the object.

    void Start()
    {
        if (this.gameObject.GetComponent<CircleCollider2D>() != null)
        {
            selfCollider = this.gameObject.GetComponent<CircleCollider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (heldItem == null)
        {

        }
        
    }

}
