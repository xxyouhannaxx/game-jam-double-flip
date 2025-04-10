using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Card;

public class CanvasManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private Card _cardPrefab;
    [SerializeField]
    private Transform _cardsPanel;
    private List<Card> _cards = new List<Card>();
    [Header("HUD")]
    [SerializeField]
    private TextMeshProUGUI _level;
    [SerializeField]
    private TextMeshProUGUI _score;
    [SerializeField]
    private TextMeshProUGUI _streak;

    /// <summary>
    /// Use template to generate a new card set
    /// </summary>
    /// <param name="set">Set of cards</param>
    /// <param name="selectionCallback">a callback on card selection</param>
    public void GenerateCards(List<CardData> set, CardEventHandler selectionCallback = null)
    {
        ClearCards();

        for (int i = 0; i < set.Count; i++)
        {
            Card card = Instantiate(_cardPrefab, _cardsPanel.transform);
            card.Initialize(set[i]);
            card.OnCardSelected += selectionCallback;
            _cards.Add(card);
        }
    }

    public void ClearCards()
    {
        foreach (Card card in _cards)
        {
            Destroy(card.gameObject);
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
}
