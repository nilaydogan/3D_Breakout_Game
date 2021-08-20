using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : Collectable
{
    public float shrinkRate;
    private GameObject paddle;
    public float deactiveTime;
    bool isApplied = false;

    private Paddle paddleScript;
    private void Start()
    {
        paddle = GameObject.FindGameObjectWithTag("Paddle");
        paddleScript = FindObjectOfType<Paddle>();
    }
    protected override void ApplyEffect()
    {
        if (!isApplied)
        {
            paddle.transform.localScale -= new Vector3(shrinkRate, 0, 0);
            paddleScript.max += (shrinkRate / 2);
            paddleScript.shrink_DeactiveTime = deactiveTime;
            paddleScript.shrinkRate = shrinkRate;
        }
    }
}