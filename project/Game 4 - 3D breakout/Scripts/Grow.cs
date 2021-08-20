using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Grow : Collectable
{
    public float growRate;
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
            paddle.transform.localScale += new Vector3(growRate, 0, 0);
            paddleScript.max -= (growRate/2 + 1f);
            paddleScript.grow_DeactiveTime = deactiveTime;
            paddleScript.growRate = growRate;
        }
        
    }
}
