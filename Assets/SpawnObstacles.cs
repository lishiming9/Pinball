using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject obstacle;
    public float scale = 1;
    public Vector3[] positions;
    public float[] elasticity;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 scaleVector = new Vector3(scale, scale, 0);
        for (int i = 0; i < positions.Length; i++)
        {
            GameObject obstacleIns = Instantiate(obstacle, positions[i], Quaternion.identity, gameObject.transform);
            //ball.transform.SetParent(gameObject.transform);
            obstacleIns.transform.localPosition = positions[i];
            obstacleIns.transform.localScale = scaleVector;
            obstacleIns.GetComponent<Obstacle>().Elastic = (elasticity[i]);
            obstacleIns.GetComponent<Obstacle>().Index = i;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
