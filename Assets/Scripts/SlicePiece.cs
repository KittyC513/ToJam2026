using UnityEngine;

public class SlicePiece : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public GameObject outline;

    public float resetTimer = 0.3f;

    public bool isHovering;

    public float timer = 0.3f;

    public int sliceIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outline.SetActive(false);
        GameManager.Instance.sliceList.Add(this);

        sliceIndex = GameManager.Instance.sliceList.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isSelected && !isHovering)
        {
            outline.SetActive(false);
        }
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
        
        if(isHovering && timer >= resetTimer)
        {
            GameManager.Instance.IsSelected(sliceIndex);
            timer = 0;
        }
    }
}
