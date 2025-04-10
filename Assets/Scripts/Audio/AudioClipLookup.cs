using Progression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = nameof(AudioClipLookup), menuName = "Lookup/" + nameof(AudioClipLookup))]
    public class AudioClipLookup : ScriptableObject
    {
        public List<AudioData> data = new List<AudioData>();
    }
}