using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Radio Station", menuName = "CustomSO/Radio Station")]
public class RadioTracker : ScriptableObject
{
    [SerializeField] private AudioClip[] allSongs;
    private List<RadioPlayer> radios = new List<RadioPlayer>();

    private int trackedRadio = -1;
    private List<int> lastSong = new List<int>();
    [SerializeField] private int rememberedSong = 3;

    public void AddListener(RadioPlayer player)
    {
        radios.Add(player);

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
        if (radios.Count == 0) return;
        trackedRadio = 0;
        radios[trackedRadio].songTracker = true;
    }

    public void QueueSong()
    {
        int nextSong = Random.Range(0, allSongs.Length - lastSong.Count);

        for (int i = 0; i < lastSong.Count; i++)
        {
            if (nextSong >= lastSong[i]) nextSong++;
        }

        foreach (RadioPlayer player in radios)
        {
            player.PlaySong(allSongs[nextSong]);
        }

        lastSong.Add(nextSong);

        if (lastSong.Count > rememberedSong) lastSong.RemoveAt(0);
    }
}
