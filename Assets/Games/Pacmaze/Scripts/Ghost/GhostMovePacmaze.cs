﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovePacmaze : MonoBehaviour {
    [SerializeField] private GhostMoveDeltaPacmaze[] ghostMoveDeltaList;
    [SerializeField] private float speedModule = 0.03f;
    private int currentIndexGhostMoveDelta = 0;
    private Vector2 nextPosition;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
        UpdateNextPosition();
    }

    private void Update() {
        (int deltaX, int deltaY) = Delta();
        float x = deltaX > 0 ? speedModule : deltaX < 0 ? -speedModule : 0;
        float y = deltaY > 0 ? speedModule : deltaY < 0 ? -speedModule : 0;
        transform.Translate(new Vector2(x, y) * Time.deltaTime);
        
        if (FinishStep(deltaX, deltaY)) {
            transform.position = new Vector2(nextPosition.x, nextPosition.y);
            currentIndexGhostMoveDelta += 1;
            if (currentIndexGhostMoveDelta >= ghostMoveDeltaList.Length) {
                currentIndexGhostMoveDelta = 0;
            }
            UpdateNextPosition();
        }
    }

    private bool FinishStep(int deltaX, int deltaY) {
        float x1 = transform.position.x;
        float y1 = transform.position.y;
        float x2 = nextPosition.x;
        float y2 = nextPosition.y;
        if (deltaX > 0 && x1 > x2) return true;
        if (deltaX < 0 && x1 < x2) return true;
        if (deltaY > 0 && y1 > y2) return true;
        if (deltaY < 0 && y1 < y2) return true;
        return false;
    }

    private void UpdateNextPosition() {
        (int deltaX, int deltaY) = Delta();
        Vector2 position = transform.position;
        nextPosition = new Vector2(position.x + deltaX, position.y + deltaY);
    }

    public (int, int) Delta() {
        if (ghostMoveDeltaList.Length > 0) {
            GhostMoveDeltaPacmaze ghostMoveDelta = ghostMoveDeltaList[currentIndexGhostMoveDelta];
            int deltaX = ghostMoveDelta.axis == GhostMoveAxesPacmaze.X ? ghostMoveDelta.delta : 0;
            int deltaY = ghostMoveDelta.axis == GhostMoveAxesPacmaze.Y ? ghostMoveDelta.delta : 0;
            animator.SetInteger("deltaX", deltaX);
            animator.SetInteger("deltaY", deltaY);
            return (deltaX, deltaY);
        } else {
            return (0, 0);
        }
    }
}
