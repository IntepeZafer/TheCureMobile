using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [Header("Assets")]
    public GameObject[] npcPrefabs; 
    public Transform[] spawnPoints; 

    [Header("Settings")]
    public float walkSpeed = 2f;      
    public float minSpawnTime = 2f;   
    public float maxSpawnTime = 5f;   
    public float npcLifeTime = 30f;   
    private List<GameObject> activeNPCs = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    void Update()
    {
        for(int i = activeNPCs.Count - 1; i >= 0; i--)
        {
            GameObject npc = activeNPCs[i];
            if(npc == null)
            {
                activeNPCs.RemoveAt(i);
                continue;
            }
            npc.transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
        }
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
            SpawnNPC();
        }
    }

    void SpawnNPC()
    {
        if(npcPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            return;
        }
        int npcIndex = Random.Range(0, npcPrefabs.Length);
        int pointIndex = Random.Range(0, spawnPoints.Length);

        Transform point = spawnPoints[pointIndex];
        GameObject selectedNPC = npcPrefabs[npcIndex];
        GameObject newNPC = Instantiate(selectedNPC, point.position, point.rotation);
        activeNPCs.Add(newNPC);
        Destroy(newNPC, npcLifeTime);
    }
}