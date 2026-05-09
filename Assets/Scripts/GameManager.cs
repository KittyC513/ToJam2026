using UnityEngine;

public class GameManager : MonoBehaviour
{


    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
