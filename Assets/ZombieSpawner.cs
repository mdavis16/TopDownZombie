using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> spawnedZombies = new List<GameObject>();

    public Transform[] spawnPoints;
    public GameObject zombie;

    public float minSpawnTime;
    public float maxSpawnTime;

    public int numOfZombiesToSpawn;

    private int totalZombiesSpawned;

    int spawnIndex;

    public int round =1;

    private bool startNewRound = false;

    private bool checkForEndRound;
    public float timeBetweenRounds;

    public int zombiesToAddPerRound =5;

    public LootManager lootManager;

    public GameObject[] specialZombies;

    public int specialZombieRound;

    public int numOfSpecialZombiesToSpawn;

  [SerializeField]
    private GameObject[] generatedZombies;

    int generatedZombieIndex = 0;

    private Stats stats;

    public Gore goreManager;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("StatsCollection").GetComponent<Stats>();
        generatedZombies = GenerateZombies();
        StartCoroutine(StartRound());
    }

    private void Update()
    {
        if (startNewRound)
        {

            StartCoroutine(StartNewRound(timeBetweenRounds));
            startNewRound = false;
        }

        if(checkForEndRound)
        {
            if (spawnedZombies.Count == 0)
            {
                startNewRound = true;
                checkForEndRound = false;
              
            }
        }
    }

    private void SpawnZombie(GameObject type,Vector3 pos)
    {

        var z =Instantiate(type, pos, Quaternion.identity);
        spawnedZombies.Add(z);
        totalZombiesSpawned++;
    }

    private IEnumerator StartRound()
    {

        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        SpawnZombie(generatedZombies[generatedZombieIndex],spawnPoints[spawnIndex].position);

        generatedZombieIndex++; 

        if (spawnIndex == spawnPoints.Length-1)
            spawnIndex =0;

        else
            spawnIndex++;

        if (totalZombiesSpawned < numOfZombiesToSpawn)
        {
            StartCoroutine(StartRound());
        }

        else {

            checkForEndRound = true;
        }

  

    }

    public void RemoveZombie(GameObject z)
    {
        if (spawnedZombies.Contains(z))
        {
            spawnedZombies.Remove(z);
        }
    }


    private IEnumerator StartNewRound(float t)
    {
     
        yield return new WaitForSeconds(t);
        round++;
        totalZombiesSpawned = 0;
        numOfZombiesToSpawn += zombiesToAddPerRound;
        generatedZombieIndex = 0;
        generatedZombies = GenerateZombies();
        StartCoroutine(StartRound());
        lootManager.SpawnFreshLoot();
        stats.numOfRoundsSurvived++;
        goreManager.FadeGore();
    }

    private GameObject [] GenerateZombies()
    {
        GameObject[] tempz = new GameObject[numOfZombiesToSpawn];
        int x =0;

        if (round % specialZombieRound == 0)
        {
           int len = numOfZombiesToSpawn - numOfSpecialZombiesToSpawn;

            

            for (int i = 0; i < len ; i++)
            {
                tempz[x] = zombie;
                x++;
            }

            for (int i = 0; i < numOfSpecialZombiesToSpawn; i++)
            {
                tempz[x] = specialZombies[Random.Range(0, specialZombies.Length)];
                x++;
            }


            numOfSpecialZombiesToSpawn++;

        }

        else
        {
            for (int i = 0; i < numOfZombiesToSpawn; i++)
            {
                tempz[x] = zombie;
                x++;
            }
        }

        for (int i = 0; i < tempz.Length - 1; i++)
        {
            int j = Random.Range(i, tempz.Length);
            var temp = tempz[i];
            tempz[i] = tempz[j];
            tempz[j] = temp;
        }
        return tempz;
    }


}
