using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    public GameObject pinball;
    public float spawnTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("Spawn", spawnTime, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        GameObject ball = Instantiate(pinball, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        ball.transform.SetParent(gameObject.transform);
        ball.transform.localPosition = Vector3.zero;
    }
}
