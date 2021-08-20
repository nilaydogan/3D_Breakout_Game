using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectable
{
    public bool isStar = false;
    private Game_Manager gm;

    public int value;

    private void Start()
    {
        gm = FindObjectOfType<Game_Manager>();
    }
    protected override void ApplyEffect()
    {
        if (isStar)
        {
            gm.multiply = 3;
        }
        else
        {
            gm.addScore(value);
        }
    }

}
