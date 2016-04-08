﻿using UnityEngine;
using System.Collections;

public class SlotRemoval : MonoBehaviour
{

	void Start ()
    {
        Transform[] Children = GetComponentsInChildren<Transform>();

        Children[Random.Range(1, 2)].gameObject.SetActive(false);

    }
}
