using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Progression
{
    [Serializable]
    public class LevelData 
    {
        public int cardAmount;
        public float revealTime;
        [Range(1,7)]
        public int columnCount;
    }
}