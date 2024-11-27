using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RadioWork
{
    public class RadioStation : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField, Multiline] private string noteFromFelix;
#endif

        [SerializeField] private RadioTower[] musicBroadcastingServices;

        public static RadioStation instance { get; private set; }

        [HideInInspector] public List<int>[] lastSong;

        [HideInInspector] public bool[] isPlayingSong;
        [SerializeField] public float[] songProgress;

        private bool hasRunFirst = false;

        private void Awake()
        {
            if (instance != null) Destroy(this);
            instance = this;

            DontDestroyOnLoad(this);

            SceneManager.sceneLoaded += OnSceneLoaded;

            // Create arrays for the music broadcasting
            lastSong = new List<int>[musicBroadcastingServices.Length];
            isPlayingSong = new bool[musicBroadcastingServices.Length];
            songProgress = new float[musicBroadcastingServices.Length];


            // Ensure the broadcasting knows which channel it should tune to, get it to save and load info from there
            for (int i = 0; i < musicBroadcastingServices.Length; i++)
            {
                musicBroadcastingServices[i].assignedStation = i;
                lastSong[i] = new List<int>();
                isPlayingSong[i] = false;
                songProgress[i] = 0;
            }
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //Debug.Log("Loading scene");

            if (!hasRunFirst)
            {
                hasRunFirst = true;
                return;
            }
            // Ensure the broadcasting knows which channel it should tune to, get it to save and load info from there
            for (int i = 0; i < musicBroadcastingServices.Length; i++)
            {
                musicBroadcastingServices[i].assignedStation = i;
            }
        }
    }
}
