using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Create Wave Data")]
public class WaveData : ScriptableObject
{
    [Range(1, 10)] public int totalGroups;
    [Range(1, 50)] public int totalEnemies;
    [Range(0, 10)] public float speedMultiplier;
    [Range(1, 20)] public int enemyMaxHp;
}
