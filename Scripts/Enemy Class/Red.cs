using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : Enemy
{
    protected override void Start()
    {
        base.Start();
        health = 150f;
        moveSpeed = 1.5f;
        damage = 20f;
    }
}