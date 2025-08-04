using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int curOrderedCount;
    public int maxOrderedCount;

    public int inGameScore;
    public int failedScore;

    public int completeCount;
    public int failedCount;

    public float inGameCurTimerRemaining = 0f;
    public float inGameMaxTimerRemaining = 300f;

    public bool isGameEnd = false;

    public GameObject uiManager;

    public Stage curStage;

    [SerializeField]private string InGameSceneName;

    public Item standardItem;

    private void Awake()
    {
        Debug.Log(SceneManager.GetActiveScene().name);

        if (instance == null)
            instance = this;
    }

    IEnumerator CreateOrderRoutine()
    {
        while (true)
        {
            UIManager.instance.GetOrder(standardItem);
            yield return new WaitForSeconds(standardItem.cookingTime - 1);
        }
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
                StopCoroutine("CreateOrderRoutine");
                UIManager.instance.inGameTimerGauge.gameObject.SetActive(false);
                UIManager.instance.StartGameEnd();
                isGameEnd = true;
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void GameStartBtn()
    {
        StartCoroutine(GameStart());
    }

    public IEnumerator GameStart()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene(InGameSceneName);

        while(SceneManager.GetActiveScene().name != InGameSceneName)
        {
            yield return null;
        }
        curStage = GameObject.Find("StageInfo").GetComponent<Stage>();
        
        if (curStage == null || curStage.ratingScore.Length != 3)
        {
            Debug.Log("현재 스테이즈 정보가 잘못되거나 없습니다");
            yield break ;
        }
        
        GameObject tmpUI = GameObject.Find("InGameUI");
        
        if (tmpUI == null)
        {
            UIManager tmpInstance = Instantiate(uiManager).GetComponent<UIManager>();
            UIManager.instance = tmpInstance;
        }
        else
        {
            UIManager.instance = tmpUI.GetComponent<UIManager>();
        }
        UIManager.instance.Initialized();
        
        StartCoroutine("GameTimer");
        StartCoroutine("CreateOrderRoutine");
    }
}
