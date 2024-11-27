using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RadioWork
{
    public class RadioStation : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField, Multiline] private string noteFromFelix;
#endif

        [SerializeField] private RadioTower[] musicBroadcastingServices;

        private static RadioStation instance;

        [HideInInspector] public List<int>[] lastSong;

        [HideInInspector] public bool[] isPlayingSong;
        [HideInInspector] public float[] songProgress;

        private void Awake()
        {
            Debug.Log("Started waking radio station");
            if (instance != null) Destroy(this);
            instance = this;

            DontDestroyOnLoad(this);

            // Create arrays for the music broadcasting
            lastSong = new List<int>[musicBroadcastingServices.Length];
            isPlayingSong = new bool[musicBroadcastingServices.Length];
            songProgress = new float[musicBroadcastingServices.Length];

            // Ensure the broadcasting knows which channel it should tune to, get it to save and load info from there
            for (int i = 0; i < musicBroadcastingServices.Length; i++)
            {
                musicBroadcastingServices[i].theStation = this;
                musicBroadcastingServices[i].assignedStation = i;
                lastSong[i] = new List<int>();
                isPlayingSong[i] = false;
                songProgress[i] = 0;
            }
            Debug.Log("Station fully awoken");
        }
    }
}
