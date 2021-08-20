using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Collectable
{
    public float speedRate;
    public float deactiveTime;
    bool isApplied = false;

    private Paddle paddleScript;

    private void Start()
    {
        paddleScript = FindObjectOfType<Paddle>();
    }
    protected override void ApplyEffect()
    {
        if (!isApplied)
        {
            paddleScript.moveSpeed += speedRate;
            paddleScript.speedUp_DeactiveTime = deactiveTime;
            paddleScript.speedRate = speedRate;
        }

    }
}
