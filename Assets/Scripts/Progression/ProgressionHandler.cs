using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Progression
{
    public class ProgressionHandler
    {
        private const string FILE_NAME = nameof(ProgressData) + ".dat";
       
        private ProgressData progressData = new ProgressData();

        public void UpdateProgress(int level, int score, int streak)
        {
            progressData.level = level;
            progressData.score = score;
            progressData.streak = streak;
        }

        public void SaveProgress()
        {
            IODataHandler.Save(Application.persistentDataPath, FILE_NAME, progressData);
        }

        public ProgressData LoadProgress()
        {
            ProgressData loadedData = IODataHandler.Load<ProgressData>(Application.persistentDataPath, FILE_NAME);
            if (loadedData != null)
            {
                progressData = loadedData;
            }
            return progressData;
        }

        public bool HasSavedData()
        {
            return IODataHandler.Exist(Application.persistentDataPath, FILE_NAME);
        }
    }
}