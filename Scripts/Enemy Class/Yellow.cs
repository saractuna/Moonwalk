using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow : Enemy
{
    protected override void Start()
    {
        base.Start();
        health = 75f;
        moveSpeed = 3.5f;
        damage = 25f;
    }
}
