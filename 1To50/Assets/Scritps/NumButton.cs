using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NumButton : MonoBehaviour
{
    public int myNum;
    public bool secondPress;

    [Space]
    public Text text;

    public NumManager numMan;

    // button stuff
    [Space]
    public Color pressColor = Color.white;
    public Color secondCollor = Color.white;

    public float showScale = 1.2f;
    public float pressScale = 1.1f;

    [Header("Times")]
    public float showTime = 1;
    public float pressTime = .3f;

    Vector3 originalScale;
    Color originalColor;
    Color transparentColor;

    Image image;

    private void Awake()
    {
        originalScale = transform.localScale;

        Button m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(PressButton);

        if (!GetComponent<Image>())
            return;

        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();

        originalColor = image.color;
        transparentColor = originalColor;
        transparentColor.a = 0;
    }

    private void OnEnable()
    {
        Show();
    }

    public void PressButton()
    {
        Debug.Log("1");
        if (!canPress())
            return;

        Debug.Log("2");

        if (secondPress)
            StartCoroutine(SecondPressCoroutine());
        else
            StartCoroutine(FirstPressCoroutine());

        if (myNum == 1)
            numMan.StartPlaying();

        if (myNum == numMan.gridSize()*2)
        {
            numMan.Completed();
        }

        numMan.currentNum++;
    }

    bool inAnimation;
    bool canPress()
    {
        if (numMan.currentNum + 1 == myNum && !inAnimation)
            return true;

        return false;
    }

    public void UpdateText()
    {
        if (!text)
            text= GetComponentInChildren<Text>();

        text.text = myNum.ToString();
    }

    Sequence mSeq;
    void Show()
    {
        transform.localScale = Vector3.zero;

        mSeq = DOTween.Sequence();

        if (image)
        {
            image.color = transparentColor;

            mSeq.Append(image.DOColor(originalColor, showTime * .2f));
        }

        mSeq.Append(transform.DOScale(originalScale * showScale, showTime * .4f));
        mSeq.Append(transform.DOScale(originalScale, showTime * .6f));

    }
    IEnumerator FirstPressCoroutine()
    {
        inAnimation = true;

        mSeq = DOTween.Sequence();
        if (image)
        {
            mSeq.Append(image.DOColor(pressColor, pressTime));
        }

        yield return mSeq.WaitForCompletion();
        originalColor = secondCollor;

        myNum = numMan.GetAvailableNum();
        UpdateText();
        Show();

        secondPress = true;
        inAnimation = false;
    }
    IEnumerator SecondPressCoroutine()
    {
        inAnimation = true;

        mSeq = DOTween.Sequence();
        if (image)
        {
            mSeq.Append(image.DOColor(pressColor, pressTime));
        }

        yield return mSeq.WaitForCompletion();
        inAnimation = false;
    }
}
