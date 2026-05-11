using Unity.Mathematics;
using UnityEngine;

public class KeepUpright : MonoBehaviour
{
    public GameObject parent;
    private Vector2 initialPosition;
    private Quaternion initialRotation;

    public Transform leftBeam;
    public Transform rightBeam;

    private float leftOffset;
    private float rightOffset;

    public float inputLeft = 0;
    public float inputRight = 0;

    private int rotMax = 110;
    private int rotMin = 70;

    public GameObject leftCup;
    public GameObject rightCup;

    public bool isBalanced = false;

    void Awake()
    {
        initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBalanced && this.transform.rotation.z < rotMax && this.transform.rotation.z > rotMin)
        {
            this.transform.Rotate(0, 0, 0);
        }
    }
}
