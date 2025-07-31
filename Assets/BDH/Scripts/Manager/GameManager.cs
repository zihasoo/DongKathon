using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int curOrderedCount;
    public int maxOrderedCount;

    public int inGameScore;

    public float inGameCurTimerRemaining = 0f;
    public float inGameMaxTimerRemaining = 300f;

    public bool isGameEnd = false;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    void Start()
    {
        StartCoroutine("GameTimer");
    }


    IEnumerator GameTimer()
    {
        while(true)
        {
            inGameCurTimerRemaining += Time.deltaTime;

            UIManager.instance.ScoreText.text = inGameCurTimerRemaining.ToString();
            //UIManager.instance.inGameTimerGauge.fillAmount = 

            if(inGameCurTimerRemaining >= inGameMaxTimerRemaining)
            {
                isGameEnd = true;
                break;
            }
            yield return null;
        }
    }
}
