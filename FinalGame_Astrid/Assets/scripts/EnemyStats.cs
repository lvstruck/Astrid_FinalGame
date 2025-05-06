using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    //template for enemy states
    //will be used inside json file
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public string name;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    public int health;
    public float speed;
    public float detectionRange;
    public float attackRange;
    public float attackCoolDown;
}
[System.Serializable]
public class EnemyDataBase
{
    public List<EnemyStats> enemiesList = new List<EnemyStats>();
}
