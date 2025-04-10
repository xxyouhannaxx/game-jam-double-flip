using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private Card _cardPrefab;
    [SerializeField]
    private Transform _cardsPanel;
    private List<Card> _cards = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateCards(List<CardData> set, CardEventHandler selectionCallback = null)
    {
        for (int i = 0; i < set.Count; i++)
        {
            Card card = Instantiate(_cardPrefab, _cardsPanel.transform);
            card.Initialize(set[i]);
            card.OnCardSelected += selectionCallback;
            _cards.Add(card);
        }
    }
}
