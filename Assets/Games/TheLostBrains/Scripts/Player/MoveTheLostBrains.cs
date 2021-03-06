﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTheLostBrains : MonoBehaviour {
    private PlayerTheLostBrains player;
    public Rigidbody2D playerRigidbody2D;
    public Animator animator;
    public float modVelocity = 1f;
    public bool isStopped;
    public LayerMask layerMaskPlatform;
    public static Vector2 externalSpeed;

    void Start() {
        player = GetComponent<PlayerTheLostBrains>();
        externalSpeed = Vector2.zero;
    }

    void Update() {
        float axisX = 0;
        float axisY = 0;
        
        if (player.isSelected) {
            axisX = Input.GetAxis("Horizontal");
            axisY = Input.GetAxis("Vertical");
        }

        float deltaX = axisX * modVelocity * Time.deltaTime;

        animator.SetBool("stop", deltaX == 0 ? true : false);
        animator.SetBool("left", deltaX < 0 ? true : false);
        animator.SetBool("right", deltaX > 0 ? true : false);

        transform.Translate(new Vector3(deltaX, 0, 0));
    }
}
