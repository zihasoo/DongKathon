using System;
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
        inGameCurTimerRemaining = inGameMaxTimerRemaining;

        while(true)
        {
            inGameCurTimerRemaining -= 0.01f;


            UIManager.instance.inGameTimerText.text = TimeSpan.FromSeconds(inGameCurTimerRemaining).ToString(@"mm\:ss");
            UIManager.instance.inGameTimerGauge.fillAmount = (inGameCurTimerRemaining / inGameMaxTimerRemaining); 

            if (inGameCurTimerRemaining <= 0)
            {
                UIManager.instance.inGameTimerGauge.gameObject.SetActive(false);
                isGameEnd = true;
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
