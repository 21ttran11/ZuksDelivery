using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fishSpawners;

    [SerializeField]
    private GameObject[] fish;

    [SerializeField]
    private int timeBetweenFish;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(timeBetweenFish);
        SpawnFish();
    }

    private void SpawnFish()
    {
        int spawnerIndex = Random.Range(0, fishSpawners.Length);
        GameObject selectedFish = SelectFish();
        Instantiate(selectedFish, fishSpawners[spawnerIndex].transform); 
        Debug.Log("Fish Spawned");
    }

    private GameObject SelectFish()
    {
        int fishIndex = Random.Range(0, fish.Length);
        return fish[fishIndex];
    }
}
