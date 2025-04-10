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
    private int level = 0;
    private int progression = 0;
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }


    public void CreateLevel()
    {
        List<CardData> set;

        if (_presets.TryGetNextLevelSet(out set, level))
        {
            _canvasManager.GenerateCards(set, CompareCards);
        }

        progression = set.Count / 2;
        level++;
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
        _streak++;
        progression--;

        if (progression == 0)
        {
            CreateLevel();
        }
    }
}
