using System.Collections;
using UnityEngine;

public class CuttingTable : DefaultTable
{
    private IEnumerator currentRoutine;

    public override bool putDownItem(ItemType givenFood)
    {
        if (currentItem != ItemType.None) return false;

        currentItem = givenFood;
        var obj = Resources.Load<GameObject>($"Prefabs/{Table.itemTypeNameMap[currentItem]}");
        Instantiate(obj, transform);
        return true;
    }

    public void startCutting()
    {
        if (currentItem == ItemType.None) return;
        currentRoutine = cuttingCoroutine();
        StartCoroutine(currentRoutine);
    }

    public void stopCutting() 
    {
        if (currentRoutine == null) return;
        StopCoroutine(currentRoutine);
        currentRoutine = null;
    }

    private void finishCutting()
    {
        currentItem = currentItem | (ItemType)isCutOff;
        Destroy(transform.GetChild(0).gameObject);
        var obj = Resources.Load<GameObject>($"Prefabs/{Table.itemTypeNameMap[currentItem]}");
        Instantiate(obj, transform);
    }

    private IEnumerator cuttingCoroutine()
    {
        print("3");
        yield return new WaitForSeconds(1);
        print("2");
        yield return new WaitForSeconds(1);
        print("1");
        yield return new WaitForSeconds(1);
        print("자르기 완료");
        finishCutting();
    }
}
