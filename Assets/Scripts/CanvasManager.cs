using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    //temporary value for testing
    [SerializeField]
    private int cardCount;
    [SerializeField]
    private Card _cardPrefab;
    [SerializeField]
    private Transform _cardsPanel;
    private List<Card> _cards = new List<Card>();
 
    // Start is called before the first frame update
    void Start()
    {
        GenerateCards();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateCards()
    {
        for (int i = 0; i < cardCount; i++)
        {
            Card card = Instantiate(_cardPrefab, _cardsPanel.transform);
            _cards.Add(card);
        }
    }
}
