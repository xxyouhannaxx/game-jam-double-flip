using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public const string MATCH_ID = "match";
        public const string MISMATCH_ID = "mismatch";
        public const string LEVEL_UP_ID = "level";

        [SerializeField]
        private List<AudioSource> _sources = new List<AudioSource>();
        [SerializeField]
        private AudioClipLookup _lookup;

        /// <summary>
        /// using this function as a pseudo-extendable pool
        /// </summary>
        /// <param name="audioClip">clip to play</param>
        /// <param name="isMandatory"> do not skip this audio even if all players are busy</param>
        public void Play(AudioClip audioClip, bool isMandatory = false)
        {
            AudioSource source = _sources.Find(x => !x.isPlaying);

            if (isMandatory && source == null)
            {
                source = Instantiate(_sources[0], transform);
                _sources.Add(source);
            }

            if (source != null)
            {
                source.clip = audioClip;
                source.Play();
            }
        }

        public void PlayAudioById(string id, bool isMandatory = false)
        {
            if (_lookup == null)
            {
                Debug.LogError($"{nameof(AudioManager)} {nameof(PlayAudioById)} was called with id {id} but is missing a {nameof(AudioClipLookup)}");
                return;
            }

            AudioData data = _lookup.data.Find(data => data.id == id);

            if (data == null)
            {
                Debug.LogError($"{nameof(AudioManager)} {nameof(PlayAudioById)} was called with id {id} that was not found in the associated {nameof(AudioClipLookup)}");
                return;
            }

            Play(data.clip, isMandatory);
        }

    }
}
