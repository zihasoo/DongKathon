using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CuttingTable : DefaultTable
{
    private IEnumerator currentRoutine;
    [SerializeField]
    private Image gauge;
    [SerializeField]
    private GameObject canvas;

    private void Start()
    {
        offGauge();
    }

    public override bool putDownItem(ItemType givenFood)
    {
        if (currentItem != ItemType.None) return false;

        currentItem = givenFood;
        var obj = Resources.Load<GameObject>($"Prefabs/Ingame/{Table.itemTypeNameMap[currentItem]}");
        Instantiate(obj, pool);
        return true;
    }

    public void startCutting()
    {
        if (currentItem > ItemType.Cucumber) return;
        currentRoutine = cuttingCoroutine();
        StartCoroutine(currentRoutine);
        canvas.SetActive(true);
    }

    public void stopCutting() 
    {
        if (currentRoutine == null) return;
        StopCoroutine(currentRoutine);
        currentRoutine = null;
        offGauge();
    }

    private void finishCutting()
    {
        currentItem = currentItem | (ItemType)isCutOff;
        Destroy(pool.GetChild(0).gameObject);
        var obj = Resources.Load<GameObject>($"Prefabs/Ingame/{Table.itemTypeNameMap[currentItem]}");
        Instantiate(obj, pool);
    }

    private void offGauge()
    {
        gauge.fillAmount = 0;
        canvas.SetActive(false);
    }

    private IEnumerator cuttingCoroutine()
    {
        for (int i = 1; i <= 10; i++)
        {
            gauge.fillAmount = 0.1f * i;
            yield return new WaitForSeconds(0.15f);
        }
        print("자르기 완료");
        finishCutting();
    }
}
