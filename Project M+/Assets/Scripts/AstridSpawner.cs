using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstridSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float secondsBetweenAsteroids;
    [SerializeField] Vector2 forceRange;

    private Camera mainCamera;
    private float timer;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnAsteroid();

            timer += secondsBetweenAsteroids;
        }
    }
    private void SpawnAsteroid()
    {
        int side = Random.Range(0, 4);

        Vector2 spownPoint=Vector2.zero; ;
        Vector2 direction = Vector2.zero;


        switch(side)
        {
            case 0:
                spownPoint.x = 0;
                spownPoint.y = Random.value;
                direction = new Vector2(1f, Random.Range(-1, 1));
                break;
            case 1:
                spownPoint.x = 1;
                spownPoint.y = Random.value;
                direction = new Vector2(-1f, Random.Range(-1, 1));
                break;
            case 2:
                spownPoint.x = Random.value;
                spownPoint.y = 0;
                direction = new Vector2(Random.Range(-1f, 1f),1f);
                break;
            case 3:
                spownPoint.x = Random.value;
                spownPoint.y = 1;
                direction = new Vector2(Random.Range(-1f, 1f), -1f);
                break;

        }

        Vector3 worldSpown = mainCamera.ViewportToWorldPoint(spownPoint);
        worldSpown.z = 0;
        GameObject selectedAstrid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
        GameObject asteroidInstance = Instantiate(selectedAstrid, worldSpown,Quaternion.Euler(0f,0f,Random.Range(0f,360f)));

        Rigidbody rb = asteroidInstance.GetComponent<Rigidbody>();

        rb.velocity = direction.normalized*Random.Range( forceRange.x,forceRange.y);
    }
}
