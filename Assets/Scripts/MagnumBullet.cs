﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnumBullet : MonoBehaviour
{
    public float bulletSpeed = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3.0f);
    }
}
