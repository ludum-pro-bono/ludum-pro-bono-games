﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivisionEquationSnakeMath : EquationSnakeMath {
    private int number1;
    private int number2;
    public string representation => $"{number1} / {number2}";
    public int result => number1 / number2;

    public void CreateEquation(int difficulty) {
        int multiplier = Random.Range(1, difficulty);
        number2 = Random.Range(1, difficulty);
        number1 = number2 * multiplier;
    }
}
