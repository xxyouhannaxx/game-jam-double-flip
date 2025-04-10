using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Card;

public class CanvasManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private Card _cardPrefab;
    [SerializeField]
    private RectTransform _gridContainer;
    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup;
    private List<Card> _cards = new List<Card>();
    [Header("HUD")]
    [SerializeField]
    private TextMeshProUGUI _level;
    [SerializeField]
    private TextMeshProUGUI _score;
    [SerializeField]
    private TextMeshProUGUI _streak;
    public Action ResetLevelsCallback;
    private float _maxColumns = 7;

    private void Start()
    {
        _maxColumns = _gridLayoutGroup.constraintCount;
    }

    /// <summary>
    /// Use template to generate a new card set
    /// </summary>
    /// <param name="set">Set of cards</param>
    /// <param name="selectionCallback">a callback on card selection</param>
    public void GenerateCards(List<CardData> set, int columnCount, float revealTime, CardEventHandler selectionCallback = null)
    {
        ClearCards();

        for (int i = 0; i < set.Count; i++)
        {
            Card card = Instantiate(_cardPrefab, _gridLayoutGroup.transform);
            card.Initialize(set[i], revealTime);
            card.OnCardSelected += selectionCallback;
            _cards.Add(card);
        }

        //Calculating the size of the grid cell
        _gridLayoutGroup.constraintCount = columnCount;
        float rowCount = Mathf.Ceil(set.Count / (float)columnCount);
        float scaleFactorX = (_gridContainer.sizeDelta.x - (columnCount * _gridLayoutGroup.spacing.x + _gridLayoutGroup.padding.left + _gridLayoutGroup.padding.right)) / columnCount;
        float scaleFactorY = (_gridContainer.sizeDelta.y - (rowCount * _gridLayoutGroup.spacing.y + _gridLayoutGroup.padding.top + _gridLayoutGroup.padding.bottom)) / rowCount;
        _gridLayoutGroup.cellSize = new Vector2(scaleFactorX, scaleFactorY);
    }

    public void ClearCards()
    {
        foreach (Card card in _cards)
        {
            card.Dispose();
        }
        _cards.Clear();
    }

    public void UpdateScore(int score)
    {
        _score.text = "Score:" + score.ToString();
    }

    public void UpdateStreak(int streak)
    {
        _streak.text = "Streak:" + streak.ToString();
    }

    public void UpdateLevel(int level)
    {
        _level.text = level.ToString();
    }

    public void ResetLevels()
    {
        ResetLevelsCallback?.Invoke();
    }
}
