using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public Dictionary<string, int> stats = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        stats["logic"] = 2;
        stats["empathy"] = 2;
        stats["charmisa"] = 2;
    }

    public int GetStat(string statName)
    {
        if (stats.ContainsKey(statName))
        {
            return stats[statName];
        }
        return 0;
    }


    public void IncreaseStat(string _statName, int amount)
    {
        if (!stats.ContainsKey(_statName))
        {
            stats[_statName] = 0;
        }
        stats[_statName] += amount;

        Debug.Log($"Increased {_statName} by {amount}, New Value {stats[_statName]}");

    }
}