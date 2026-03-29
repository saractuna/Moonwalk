using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green : Enemy
{
    protected override void Start()
    {
        base.Start();
        health = 50f;
        moveSpeed = 3f;
        damage = 15f;
    }
}
