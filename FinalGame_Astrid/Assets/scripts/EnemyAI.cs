using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
using UnityEngine.Rendering;

public class EnemyAI : MonoBehaviour
{
    //defines diff states and switches between them
    public enum EnemyState { Idle, Patrol, Chase, Attack, Death}
    private EnemyState currentState;

    //references
    private Transform player;
    private NavMeshAgent agent;
    

    //patrol settings
    public Transform[] patrolPoints;
    private int currentPatrolIndex;

    //enemy stats loaded from json
    public string enemyType; // Name of the enemy in the JSON
    private float speed;
    private float detectionRange;
    private float attackRange;
    public float attackCooldown;
    public float attackDamage;


    float lastAttackTime;

    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //load enemy data from JSON
        LoadEnemyData(enemyType);

        //apply loaded stats
        agent.speed = speed;
        lastAttackTime = -attackCooldown;

        currentState = EnemyState.Patrol; //start with patrolling
        MoveToNextPatrolPoint();

        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            /*if(player != null)
            {
                //Debug.Log("player found in scene");
            }
            else
            {
                //Debug.Log("no found player");
            }*/
            
            
        }

        
        
    }


    // Update is called once per frame
    void Update()
    {
       // Debug.Log($"Enemy State: {currentState} | Distance to Player: {Vector3.Distance(transform.position, player.position)} | Speed: {agent.speed} | Has Path: {agent.hasPath}");

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //switch statement is like multiple choice descion maker in programming, instead of a bunch if else statements
        //checks a variable and decides what code to run based off of its value

        //switch statement determines what behavior the enemy should perform based on its currentState
        //switch statement checks current state of enemy and decides which behavior to execute
        switch(currentState)
        { 
            case EnemyState.Idle:
                IdleBehavior();
                //break makes sure program doesnt check other cases once a match is found
                break;

                //moves between waypoints if player is detected it switches to chase
            case EnemyState.Patrol:
                PatrolBehavior();
                //if enemy within detection will switch to chase
                if (distanceToPlayer <= detectionRange) ChangeState(EnemyState.Chase);
                break;

                //moves toward player if close enough switches to attack
                //if player escapes switches back to patrol
            case EnemyState.Chase:
                ChaseBehavior();
                if(distanceToPlayer <= attackRange) ChangeState(EnemyState.Attack);
                else if(distanceToPlayer > detectionRange) ChangeState(EnemyState.Patrol);
                break;

                //attacks player if player moves away switches back to chase
            case EnemyState.Attack:
                AttackBehavior();
                if (distanceToPlayer > attackRange)
                {
                    Debug.Log("Player out of range switching to chase");
                    ChangeState(EnemyState.Chase);
                }
                break;

            case EnemyState.Death:
                break;

        }
    }

    //simply updates enemys current state
    void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    void IdleBehavior()
    {
        //you can add an animation if you want
    }

    void PatrolBehavior()
    {
        if (!agent.enabled || !agent.isOnNavMesh) return;

        //enemy follows fath to target (patrol point)
        //it waits until it reaches patrol point
        //once it reaches the point it moves to next location

        //pathPending is true if unity is still calculating path
        //if false means path has been fully calculated and enemy is actually moving towards target
        //ensures enemy only switches patrol points after reaching the target
        //if enmy is close enough to patrol point, .5 it moves to next one
        if(!agent.pathPending && agent.remainingDistance < .5f)
        {
            MoveToNextPatrolPoint();
        }


    }
    private void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        //.set destination moves to next patrol point
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        //updates the index
        //if currentpatrolindex is 0 it moves it to 1
        //loops back around
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    //when player is in range enemy follows them
    void ChaseBehavior()
    { 
        
        agent.SetDestination(player.position);
        
        
    }

    //if player within attack range enemy attacks
    //uses cooldown to prevent spamming attacks
    void AttackBehavior()
    {
        float currentTime = Time.time;
        //Debug.Log($"Attack Check - Current Time: {currentTime}, Last Attack Time: {lastAttackTime}, Cooldown: {attackCooldown}");

        if (currentTime >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = currentTime;
            //Debug.Log("Enemy attacked player");

          
        }
        else
        {
            Debug.Log("attacking cooldown");
        }

    }

    private void LoadEnemyData(string enemyName)
    {
        //path to json file
        string path = Application.dataPath + "/Data/enemiesText.json";
        Debug.Log("Path" + path);
        if(File.Exists(path)) //check if file exists
        {
            //read json file as text and store as a string
            string json = File.ReadAllText(path); 
            Debug.Log("json");
            //convert json to c# objects
            //stores result
            EnemyDataBase enemyStats = JsonUtility.FromJson<EnemyDataBase>(json);
            Debug.Log("Enemy Start");
            //find the correct enemy in json
            //loops through all enemies
            foreach(EnemyStats enemy in enemyStats.enemiesList)
            {
                Debug.Log($"Checking enemy: {enemy.name}");

                //find the enemy that marches the requested name
                if(enemy.name == enemyName)
                {
                    //Debug.Log($"Enemy {enemy.name} found! Assigning stats...");
                    //health = enemy.health;
                    speed = enemy.speed;
                    detectionRange = enemy.detectionRange;
                    attackRange = enemy.attackRange;
                    attackCooldown = enemy.attackCoolDown;
                    Debug.Log($"Loaded enemy: {enemy.name}");
                    return;
                }
            }

        }
        else
        {
            Debug.Log("enemy json file not found");
        }
    }

}
