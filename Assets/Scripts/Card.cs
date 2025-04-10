using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Card;

public class Card : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    [SerializeField]
    private Image _background;
    [SerializeField]
    private Image _front;
    [SerializeField]
    private TextMeshProUGUI _title;
    public int id { get; private set; }
    [Header("Flip Animation")]
    [Tooltip("Animation duration")]
    [SerializeField]
    private float _flipAnimationTime;
    [SerializeField]
    [Tooltip("Animation smoothing, should be kept between 0 and 1")]
    private AnimationCurve _animationCurve;
    [Header("Scale Animation")]
    [SerializeField]
    private Vector3 _scaleMaxVector = Vector3.zero;
    [SerializeField]
    [Tooltip("Animation smoothing, should be kept between 0 and 1")]
    private AnimationCurve _scaleAnimationCurve;
    [SerializeField]
    private float _scaleAnimationTime;

    private Quaternion openedCardRotation = Quaternion.Euler(0, 180, 0);
    private Quaternion closedCardRotation = Quaternion.Euler(0, 0, 0);
    private Coroutine _flipAnimation = null;
    private Coroutine _scaleAnimation = null;

    public delegate void CardEventHandler(Card card);
    public CardEventHandler OnCardSelected;

    public void Initialize(CardData data)
    {
        id = data.id;
        _front.sprite = data.frontSprite;
        _title.text = data.title;
        _button.interactable = true;
    }

    public void Dispose()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
    public void Select()
    {
        if (_flipAnimation == null)
        {
            _flipAnimation = StartCoroutine(FlipAnimation(true, openedCardRotation, OnCardSelected));
        }
    }

    public void MatchCard()
    {
        _button.interactable = false;
        _scaleAnimation = StartCoroutine(MatchAnimation(_scaleMaxVector, true));
    }

    public void Close()
    {
        if (_flipAnimation == null)
        {
            _flipAnimation = StartCoroutine(FlipAnimation(false, closedCardRotation));
        }
    }

    /// <summary>
    /// Programmatic animation for card flipping
    /// </summary>
    /// <param name="open">should be flipped open or closed</param>
    /// <param name="endRotation">end rotation of the card</param>
    /// <param name="cardEventHandler">trigger specific callback at the end of the animation</param>
    private IEnumerator FlipAnimation(bool open, Quaternion endRotation, CardEventHandler cardEventHandler = null)
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        float timer = 0;
        Quaternion startRotation = transform.rotation;

        while (timer < _flipAnimationTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, _animationCurve.Evaluate(timer / _flipAnimationTime));
            yield return waitForEndOfFrame;
            timer += Time.deltaTime;

            if (timer >= _flipAnimationTime / 2)
            {
                _background.gameObject.SetActive(!open);
            }
        }

        transform.rotation = endRotation;
        _flipAnimation = null;
        cardEventHandler?.Invoke(this);
    }

    /// <summary>
    /// Programmatic animation for scaling
    /// </summary>
    /// <param name="endScale">scale at end of animation</param>
    /// <param name="replay">replay animation back to default scale</param>
    private IEnumerator MatchAnimation(Vector3 endScale, bool replay = false)
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        //Scale up
        float timer = 0;
        Vector3 startScale = transform.localScale;

        while (timer < _flipAnimationTime / 2)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, _scaleAnimationCurve.Evaluate(timer / (_flipAnimationTime / 2)));
            yield return waitForEndOfFrame;
            timer += Time.deltaTime;
        }

        transform.localScale = endScale;

        if (replay)
        {
            _scaleAnimation = StartCoroutine(MatchAnimation(startScale));
        }
        else
        {
            _scaleAnimation = null;
        }
    }
}