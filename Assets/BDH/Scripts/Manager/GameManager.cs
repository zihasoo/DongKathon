using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int CurOrderedCount;

    public int MaxOrderedCount;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
}
