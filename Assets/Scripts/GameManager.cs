using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CanvasManager _canvasManager;
    [SerializeField]
    private LevelPresets _presets;

    private List<Card> selectedCards = new List<Card>();
    private int _score = 0;
    private int _streak = 0;
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }


    public void CreateLevel()
    {
        List<CardData> set;

        if (_presets.TryGetSet(5, out set))
        {
            _canvasManager.GenerateCards(set, CompareCards);
        }
    }

    public void CompareCards(Card card)
    {
        //first card in the list
        if (selectedCards.Count == 0)
        {
            selectedCards.Add(card);
        }
        //compare cards
        else
        {
            Card previous = selectedCards[selectedCards.Count - 1];

            if (previous.id == card.id)
            {
                card.DisableCard();
                previous.DisableCard();
                Score(1 + _streak);
                _streak++;
            }
            else
            {
                card.Close();
                previous.Close();
                //reset streak
                _streak = 0;
            }

            selectedCards.Remove(previous);
        }
    }

    public void Score(int value)
    {
        _score += value;
    }
}
