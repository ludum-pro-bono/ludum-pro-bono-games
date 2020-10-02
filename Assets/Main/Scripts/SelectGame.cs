﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectGame : MonoBehaviour {
    public GameObject[] cards;
    private float screenHeight;
    private float screenWidth;
    private int cardSelecionado;
    public float velocidade;
    private BoxCollider2D boxCollider2D;
    public Direction direction;
    private float maxX, minX, maxY, minY;
    public float widthCard;
    public float marginStop;
    private string _selectedGameName;
    public string selectedGameName { get { return _selectedGameName; } }
    public enum Direction {
        stop,
        left,
        center,
        right
    }

    void Start() {
        screenHeight = Camera.main.orthographicSize * 2;
        screenWidth = screenHeight / Screen.height * Screen.width;

        maxX = screenWidth / 2;
        minX = -maxX;
        maxY = screenHeight / 2;
        minY = -maxY;

        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update() {
        if (Input.GetKeyDown(Keys.left)) {
            direction = Direction.left;
        }
        if (Input.GetKeyDown(Keys.right)) {
            direction = Direction.right;
        }
        if (Input.GetKeyDown(Keys.start)) OpenGameSelected();

        if (direction != Direction.stop) {
            MoveCard(cardSelecionado);
        }
    }

    private int SelectCardPrev() {
        cardSelecionado -= 1;
        if (cardSelecionado < 0) cardSelecionado = cards.Length - 1;
        return cardSelecionado;
    }

    private int SelectCardNext() {
        cardSelecionado += 1;
        if (cardSelecionado >= cards.Length) cardSelecionado = 0;
        return cardSelecionado;
    }

    private void MoveCard(int numCard) {
        float x = 0;
        Transform card = cards[numCard].transform;

        if (direction == Direction.center) {
            Vector2 position = card.transform.position;

            if (position.x == 0) {
                direction = Direction.stop;
                return;
            }
            else if (position.x > -marginStop && position.x < marginStop) {
                position.x = 0;
                card.position = position;
            }
            else if (position.x < 0) x = velocidade;
            else if (position.x > 0) x = -velocidade;

            card.Translate(new Vector2(x * Time.deltaTime, 0));
        }
        else
        {
            if (direction == Direction.right) x = -velocidade;
            else if (direction == Direction.left) x = velocidade;

            card.Translate(new Vector2(x * Time.deltaTime, 0));
            Vector2 position = card.transform.position;

            if (position.x < minX - (widthCard / 2)) {
                SelectCardPrev();
                Vector3 positionNextCard = cards[cardSelecionado].transform.transform.position;
                position.x = maxX + (widthCard / 2);
                cards[cardSelecionado].transform.transform.position = position;

                if (direction == Direction.center) direction = Direction.stop;
                else direction = Direction.center;
            }
            else if (position.x > maxX + (widthCard / 2)) {
                SelectCardNext();
                Vector3 positionNextCard = cards[cardSelecionado].transform.transform.position;
                position.x = minX - (widthCard / 2);
                cards[cardSelecionado].transform.transform.position = position;

                if (direction == Direction.center) direction = Direction.stop;
                else direction = Direction.center;
            }
        }
    }

    public void OpenGameSelected() {
        _selectedGameName = cards[cardSelecionado].name;
        SceneManager.LoadScene($"Games/{selectedGameName}/Scenes/GameIntroduction"); 
    }
}