﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour {
    
	void Start () {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene(1);
	}
}
