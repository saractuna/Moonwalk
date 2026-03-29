using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour // I don't think I need this anymore but just in case..
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
