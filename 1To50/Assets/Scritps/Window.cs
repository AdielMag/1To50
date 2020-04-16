using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public float scaleShowPunch = 1.2f;

    [Header("Times")]
    public float showTime = 1;

    Vector3 originalScale;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();

        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        Show();
    }

    Sequence mSeq;
    public void Show()
    {
        mSeq = DOTween.Sequence();
        transform.localScale = Vector3.zero;

        mSeq.Append(transform.DOScale(originalScale * scaleShowPunch, showTime * .4f));
        mSeq.Append(transform.DOScale(originalScale, showTime * .6f));
    }

    public void Hide()
    {
        StartCoroutine(HideCoroutine());
    }
    public void Hide(Vector3 showPos)
    {
        StartCoroutine(HideThanShow(showPos));
    }

    IEnumerator HideCoroutine()
    {
        mSeq = DOTween.Sequence();

        mSeq.Append(transform.DOScale(originalScale * scaleShowPunch, showTime * .4f));
        mSeq.Append(transform.DOScale(Vector3.zero, showTime * .6f));

        yield return mSeq.WaitForCompletion();

        gameObject.SetActive(false);
    }
    IEnumerator HideThanShow(Vector3 pos)
    {
        mSeq = DOTween.Sequence();

        mSeq.Append(transform.DOScale(originalScale * scaleShowPunch, showTime * .4f));
        mSeq.Append(transform.DOScale(Vector3.zero, showTime * .6f));

        yield return mSeq.WaitForCompletion();

        transform.position = pos;

        Show();
    }

    private void OnDisable()
    {
        mSeq.Kill();
        StopAllCoroutines();
    }
}
