using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(LevelPresets), menuName = "Levels/" + nameof(LevelPresets))]
public class LevelPresets : ScriptableObject
{
    public List<CardData> cards = new List<CardData>();
    public List<int> levels = new List<int>();

    public bool TryGetSet(int pairCount, out List<CardData> set)
    {
        set = new List<CardData>();
        List<CardData> pool = new List<CardData>(cards);

        if (pairCount == 0 || cards.Count < pairCount)
        {
            Debug.LogError($"{nameof(LevelPresets)} could not generate a set for {pairCount} amount of cards while having {cards.Count} cards in it's selection");
            return false;
        }

        for (int i = 0; i < pairCount; i++)
        {
            int index = Random.Range(0, pool.Count);
            set.Add(pool[index]);
            set.Add(pool[index]);
            pool.Remove(pool[index]);
        }

        set.Shuffle();
        return true;
    }

    public bool TryGetNextLevelSet(out List<CardData> set, int level = 0)
    {
        set = new List<CardData>();

        if (levels.Count == 0)
        {
            Debug.LogError($"{nameof(LevelPresets)} has no levels, please ensure that there's at least 1 level in the list");
            return false;
        }

        if (level >= levels.Count)
        {
            level = levels.Count - 1;
        }

        return TryGetSet(levels[level], out set);
    }
}
