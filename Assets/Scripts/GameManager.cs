using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CanvasManager _canvasManager;
    [SerializeField]
    private LevelPresets _presets;

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateLevel()
    {
        List<CardData> set;
    
        if (_presets.TryGetSet(5, out set))
        {
            _canvasManager.GenerateCards(set);
        }
    }
}
