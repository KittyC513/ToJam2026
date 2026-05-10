using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class CustomerRequestController : MonoBehaviour
{
    // x = cheese type / y = cheese weight
    public static Vector2 currentRequest;

    public static float leeway;

    [SerializeField]
    // x = min / y = max
    private Vector2 brieLimits;
    [SerializeField]
    // x = min / y = max
    private Vector2 swissLimits;

    [SerializeField]
    private bool requestedCheese;

    void Start()
    {
        newRequestTest();
    }

    void Update()
    {
        
    }

    void newRequestTest()
    {
        float coinFlip = Random.value;
        Debug.Log("RandomResult: " + coinFlip);

        if (coinFlip > .5f) 
        { 
            // Swiss
            requestedCheese = true;
        }
        else
        { 
            // Brie
            requestedCheese= false;
        }

        if (requestedCheese)
        {
            currentRequest = new Vector2(requestedCheese ? 1.0f : 0.0f, Random.Range(swissLimits.x, swissLimits.y));
        }
        else if (!requestedCheese)
        {
            currentRequest = new Vector2(requestedCheese ? 1.0f : 0.0f, Random.Range(brieLimits.x, brieLimits.y));
        }
        Debug.Log("Run Test");

        Debug.Log("Type: " + currentRequest.x + " Weight: " + currentRequest.y);

    }

}
