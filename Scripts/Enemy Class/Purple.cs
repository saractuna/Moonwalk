using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple : Enemy
{
    protected override void Start()
    {
        base.Start();
        health = 100f;
        moveSpeed = 2.5f;
        damage = 10f;
    }
}
