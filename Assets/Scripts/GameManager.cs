using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private CanvasManager _canvasManager;
    [SerializeField]
    private LevelPresets _presets;

    private Queue<Card> selectedCards = new Queue<Card>();
    private int _score = 0;
    private int _streak = 0;
    private int _level = 0;
    private int progression = 0;

    public delegate void UpdateCallbackHandler(int value);
    public UpdateCallbackHandler OnScoreUpdated;
    public UpdateCallbackHandler OnLevelUpdated;
    public UpdateCallbackHandler OnStreakUpdated;


    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
        OnScoreUpdated += _canvasManager.UpdateScore;
        OnLevelUpdated += _canvasManager.UpdateLevel;
        OnStreakUpdated += _canvasManager.UpdateStreak;
        //initial setup
        OnScoreUpdated?.Invoke(_score);
        OnLevelUpdated?.Invoke(_level);
        OnStreakUpdated?.Invoke(_streak);
    }

    private void OnDestroy()
    {
        OnScoreUpdated -= _canvasManager.UpdateScore;
        OnLevelUpdated -= _canvasManager.UpdateLevel;
        OnStreakUpdated -= _canvasManager.UpdateStreak;
    }

    public void CreateLevel()
    {
        List<CardData> set;

        if (_presets.TryGetNextLevelSet(out set, _level))
        {
            _canvasManager.GenerateCards(set, CompareCards);
        }

        progression = set.Count / 2;
        _level++;

        OnLevelUpdated?.Invoke(_level);
    }

    /// <summary>
    /// Game logic to reward player, update streak etc...
    /// </summary>
    /// <param name="card">Currenty selected card</param>
    public void CompareCards(Card card)
    {
        //first card in the list
        if (selectedCards.Count == 0)
        {
            selectedCards.Enqueue(card);
        }
        //compare cards
        else
        {
            Card previous = selectedCards.Dequeue();

            if (previous.id == card.id)
            {
                card.DisableCard();
                previous.DisableCard();
                Success(1 + _streak);
            }
            else
            {
                card.Close();
                previous.Close();
                //reset streak
                _streak = 0;
                OnStreakUpdated?.Invoke(_streak);
            }
        }
    }

    /// <summary>
    /// Calculate progress on success
    /// </summary>
    /// <param name="value">potential card based value, currently hard coded to one but can be later adjusted to per card type</param>
    public void Success(int value)
    {
        _score += value;
        _streak++;
        progression--;

        OnScoreUpdated?.Invoke(_score);
        OnStreakUpdated?.Invoke(_streak);

        if (progression == 0)
        {
            CreateLevel();
        }
    }
}
