using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using DG.Tweening;

public class Bounce : MonoBehaviour
{

    public float gravity = -9.8f;
    //public float bounceTime = 0.5f;
    public Vector2 elasticity = new Vector2(0.8f, 1.2f);
    public Vector2 angleDeviation = new Vector2(-0.2f, 0.2f);

    private Vector3 velocity = new Vector3(0f, 0f, 0f);
    private string log = "";
    private string decimalPlaces = "F3";
    private PinBallResult result;

    [Serializable]
    public class PinBallResult
    {
        public Vector3 startPos;
        public List<Hit> hits;
        public Vector3 endPos;
        public PinBallResult()
        {
            hits = new List<Hit>();
        }

        public Vector3 StartPos { get => startPos; set => startPos = value; }
        public List<Hit> Hits { get => hits; set => hits = value; }
        public Vector3 EndPos { get => endPos; set => endPos = value; }
    }

    [Serializable]
    public struct Hit
    {
        public Hit(int i, Vector3 v)
        {
            index = i;
            velocity = v;
        }
        public int index;
        public Vector3 velocity;
    }
    // Start is called before the first frame update
    void Start()
    {
        result = new PinBallResult();
        result.StartPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocity.y = velocity.y + gravity * Time.deltaTime;
      
        transform.position = transform.position + velocity;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "DeadPool")
        {
            result.EndPos = gameObject.transform.position;
            print(JsonUtility.ToJson(result));
            PlayPinball.Play(JsonUtility.ToJson(result));
            Destroy(gameObject);
        }
        else
        {
            Vector3 normal = Vector3.Normalize(gameObject.transform.position - other.gameObject.transform.position);
            Bouncing(other.gameObject, normal);
        }
    }


    void Bouncing(GameObject other, Vector3 normal)
    {
        
        log += "obstacle index " + other.GetComponent<Obstacle>().Index;
        float degree = (float)NextGaussianDouble(angleDeviation.x, angleDeviation.y);
        
        float radian = degree * Mathf.Deg2Rad;
        
        velocity = Vector3.Reflect(velocity, normal);
        velocity.x = velocity.x * (float)Mathf.Cos(radian) - velocity.y * (float)Mathf.Sin(radian);
        velocity.y = velocity.x * (float)Mathf.Sin(radian) + velocity.y * (float)Mathf.Cos(radian);
        //randomV.x = (float)NextGaussianDouble(-1, 1);
        //velocity.x = velocity.x + randomV.x * sideV;
        velocity = velocity * other.GetComponent<Obstacle>().Elastic;
        Hit hit = new Hit(other.GetComponent<Obstacle>().Index, velocity);
        List<Hit> hits = result.Hits;
        hits.Add(hit);
        result.Hits = hits;
        //print(randomV.x);
    }  

    public static double NextGaussianDouble(float minValue, float maxValue)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0);

        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }
}