﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Simple Dialog Class - Specify Sentences to Display
 */
[System.Serializable]
public class Dialog
{
    [TextArea(3, 10)]
    public string[] Sentences;

    public string ContinueMessage;
}
