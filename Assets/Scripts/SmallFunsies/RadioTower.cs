using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RadioWork;

[CreateAssetMenu(fileName = "Radio Station", menuName = "CustomSO/Radio Station")]
public class RadioTower : ScriptableObject
{
    [HideInInspector] public RadioStation theStation; // Saves certain data between scenes, though not between gameplay sessions
    [HideInInspector] public int assignedStation = -1;

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

        // If radio has started
        if (theStation.isPlayingSong[assignedStation])
        {
            // Load song that was playing
            player.audioPlayer.clip = allSongs[theStation.lastSong[assignedStation][theStation.lastSong[assignedStation].Count - 1]];
            // Load time to play from
            if (trackedRadio >= 0) player.audioPlayer.time = radios[trackedRadio].audioPlayer.time;
            else player.audioPlayer.time = theStation.songProgress[assignedStation];
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
        theStation.songProgress[assignedStation] = player.audioPlayer.time; // Remember current song progress if this is a scene unload basically
        if (radios.Count == 0) return;
        trackedRadio = 0;
        radios[trackedRadio].songTracker = true;
    }

    public void RadioStartup()
    {
        theStation.isPlayingSong[assignedStation] = true;
        QueueSong();
    }

    public void QueueSong()
    {
        int nextSong = Random.Range(0, allSongs.Length - theStation.lastSong[assignedStation].Count);

        for (int i = 0; i < theStation.lastSong[assignedStation].Count; i++)
        {
            if (nextSong >= theStation.lastSong[assignedStation][i]) nextSong++;
        }

        foreach (RadioPlayer player in radios)
        {
            player.PlaySong(allSongs[nextSong]);
        }

        theStation.lastSong[assignedStation].Add(nextSong);

        if (theStation.lastSong[assignedStation].Count > rememberedSongs) theStation.lastSong[assignedStation].RemoveAt(0);
    }
}
