using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumManager : MonoBehaviour
{
    public enum Size {five,four,three}
    public Size gridSizeEnum;

    public bool isPlaying;
    public int currentNum = 0;

    [Space]
    public Text timerIndictor;
    public Text completedTime;
    [Space]
    public GameObject numberButtonPrfab;

    [Space]
    public List<int> availableNumbers;

    public void MakeNumbers()
    {
        Clear();

        SetAvailableNumbersList(true);

        for (int i = 0; i < gridSize(); i++)
            {
                NumButton obj = Instantiate(numberButtonPrfab, transform).GetComponent<NumButton>();

                obj.myNum = GetAvailableNum();
                obj.UpdateText();
                obj.numMan = this;
            }

        SetAvailableNumbersList(false);

        isPlaying = false;
        currentNum = 0;
        timer = 0;
        UpadteTimerIndicator();
    }
    void Clear()
    {
        int childCount = transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);
    }
    void SetAvailableNumbersList(bool firstTime)
    {
        availableNumbers.Clear();

        if (firstTime)
            for (int i = 1; i < gridSize()+1; i++)
                availableNumbers.Add(i);
        else
            for (int i = gridSize() + 1; i < (gridSize()*2) + 1; i++)
                availableNumbers.Add(i);
    }

    public int GetAvailableNum()
    {
        int random = Random.Range(0, availableNumbers.Count - 1);

        int targetNum = availableNumbers[random];

        availableNumbers.RemoveAt(random);

        return targetNum;
    }
    public int gridSize()
    {
        if (gridSizeEnum == Size.five)
            return 25;

        if (gridSizeEnum == Size.four)
            return 16;

        if (gridSizeEnum == Size.three)
            return 9;

        return 25;
    }

    public void StartPlaying()
    {
        isPlaying = true;
    }
    public void Completed()
    {
        isPlaying = false;
        completedTime.text = "Your time is: " + timer.ToString(".000");
        completedTime.transform.parent.gameObject.SetActive(true);
    }

    float timer;
    private void Update()
    {
        if (!isPlaying)
            return;

        timer += Time.deltaTime;

        UpadteTimerIndicator();
    }

    void UpadteTimerIndicator()
    {
        if (timer < 1)
            timerIndictor.text = "Time: " + "0" + timer.ToString(".000");
        else
            timerIndictor.text = "Time: " + timer.ToString(".000");
    }
}
