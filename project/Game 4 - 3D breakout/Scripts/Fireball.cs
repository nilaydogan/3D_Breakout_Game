using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Collectable
{
    private Paddle paddleScript;
    private Animator paddleAnim;
    public float deactivationTime;
    private void Start()
    {
        paddleScript = FindObjectOfType<Paddle>();
        paddleAnim = GameObject.FindGameObjectWithTag("Paddle").GetComponent<Animator>();
    }
    protected override void ApplyEffect()
    {
        paddleScript.fire_DeactiveTime = deactivationTime;
        paddleAnim.SetTrigger("Start");
        paddleScript.canShoot = true;
    }
}
