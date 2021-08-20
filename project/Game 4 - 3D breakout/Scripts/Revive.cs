using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : Collectable
{
    private BreakoutGame bgScript;
    private void Start()
    {
        bgScript = FindObjectOfType<BreakoutGame>();
    }
    protected override void ApplyEffect()
    {
        bgScript.isRevived = true;
        bgScript.revive += 1;
    }
}
