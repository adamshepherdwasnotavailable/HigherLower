using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipPattern
{
    public int leftRightCount;
    public int middleCount;

    public PipPattern(int leftRight, int middle)
    {
        this.leftRightCount = leftRight;
        this.middleCount = middle;
    }
}
