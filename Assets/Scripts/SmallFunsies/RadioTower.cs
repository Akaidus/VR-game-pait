using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RadioWork;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Radio Station", menuName = "CustomSO/Radio Station")]
public class RadioTower : ScriptableObject
{
    [SerializeField] public int assignedStation = -1;

    [SerializeField] private AudioClip[] allSongs;
    private List<RadioPlayer> radios = new List<RadioPlayer>();

    private int trackedRadio = -1;
    [SerializeField] private int rememberedSongs = 3;


    public void AddListener(RadioPlayer player)
    {
        if (assignedStation < 0)
        {
            Debug.LogError($"Radio tower {name} is not assigned in the RadioStation, and will therefore not play anything");
            return;
        }

        radios.Add(player);

        //Debug.Log("New listener is joining in");

        // If radio has started
        if (RadioStation.instance.isPlayingSong[assignedStation])
        {
            // Load song that was playing
            player.audioPlayer.clip = allSongs[RadioStation.instance.lastSong[assignedStation][RadioStation.instance.lastSong[assignedStation].Count - 1]];
            // Load time to play from
            //Debug.Log($"This listener is continuing from {(trackedRadio >= 0 ? $"a preexisting radio at {radios[trackedRadio].audioPlayer.time}" : $"the radio station at {RadioStation.instance.songProgress[assignedStation]}")}");
            if (trackedRadio >= 0) player.audioPlayer.time = radios[trackedRadio].audioPlayer.time;
            else player.audioPlayer.time = RadioStation.instance.songProgress[assignedStation];
            // Play song
            player.audioPlayer.Play();
        }
        else RadioStartup();

        if (trackedRadio >= 0) return;
        trackedRadio = radios.IndexOf(player);
        player.songTracker = true;
    }
    public void RemoveListener(RadioPlayer player)
    {
        bool setNewTracker = false;
        if (radios.IndexOf(player) == trackedRadio) setNewTracker = true;

        radios.Remove(player);

        if (!setNewTracker) return;
        trackedRadio = -1;
        //Debug.Log($"Remembering time of {player.audioPlayer.time}");
        if (player.audioPlayer.time > 0.2f) RadioStation.instance.songProgress[assignedStation] = player.audioPlayer.time; // Remember current song progress if this is a scene unload basically
        if (radios.Count == 0) return;
        trackedRadio = 0;
        radios[trackedRadio].songTracker = true;
    }

    public void RadioStartup()
    {
        RadioStation.instance.isPlayingSong[assignedStation] = true;
        QueueSong();
    }

    public void QueueSong()
    {
        int nextSong = Random.Range(0, allSongs.Length - RadioStation.instance.lastSong[assignedStation].Count);

        for (int i = 0; i < RadioStation.instance.lastSong[assignedStation].Count; i++)
        {
            if (nextSong >= RadioStation.instance.lastSong[assignedStation][i]) nextSong++;
        }

        foreach (RadioPlayer player in radios)
        {
            player.PlaySong(allSongs[nextSong]);
        }

        RadioStation.instance.lastSong[assignedStation].Add(nextSong);

        if (RadioStation.instance.lastSong[assignedStation].Count > rememberedSongs) RadioStation.instance.lastSong[assignedStation].RemoveAt(0);
    }
}
