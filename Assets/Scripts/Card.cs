using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    private Image _background;
    [SerializeField]
    private Image _front;
    private string _id;
    [Header("Animation")]
    [Tooltip("Animation duration")]
    [SerializeField]
    private float _animationTime;
    [SerializeField]
    [Tooltip("Animation smoothing, should be kept between 0 and 1")]
    private AnimationCurve _animationCurve;

    Quaternion openedCardRotation = Quaternion.Euler(0, 180, 0);
    Quaternion closedCardRotation = Quaternion.Euler(0, 0, 0);

    public delegate void CardEventHandler(Card card);
    public CardEventHandler OnCardSelected;

    public void Initialize()
    {
        //TODO: import card data once available
    }

    public void Select()
    {
        StartCoroutine(Animation(true, openedCardRotation, OnCardSelected));
    }

    public void Close()
    {
        StartCoroutine(Animation(false, closedCardRotation));
    }

    /// <summary>
    /// Programmatic animation for card flipping
    /// </summary>
    /// <param name="open">should be flipped open or closed</param>
    /// <param name="endRotation">end rotation of the card</param>
    /// <param name="cardEventHandler">trigger specific callback at the end of the animation</param>
    /// <returns></returns>
    private IEnumerator Animation(bool open, Quaternion endRotation, CardEventHandler cardEventHandler = null)
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        float timer = 0;
        Quaternion startRotation = transform.rotation;

        while (timer < _animationTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, _animationCurve.Evaluate(timer));
            yield return waitForEndOfFrame;
            timer += Time.deltaTime;

            if (timer >= _animationTime / 2)
            {
                _background.gameObject.SetActive(!open);
            }
        }

        transform.rotation = endRotation;
        cardEventHandler?.Invoke(this);
    }
}
