using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats 
{
    //template for enemy states
    //will be used inside json file
    public string name;
    public int health;
    public float speed;
    public float detectionRange;
    public float attackRange;
    public float attackCoolDown;
    public int attackDamage;
}
[System.Serializable]
public class EnemyDataBase
{
    public List<EnemyStats> enemiesList = new List<EnemyStats>();
}
