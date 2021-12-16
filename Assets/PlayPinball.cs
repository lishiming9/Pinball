using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Bounce;

public class PlayPinball : MonoBehaviour
{
    public static GameObject playBall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void Play(string json)
    {
        PinBallResult result = JsonUtility.FromJson<PinBallResult>(json);
        print("playing");
        GameObject ball = Instantiate(PlayPinball.playBall);
        ball.transform.position = result.startPos;
    }
}
