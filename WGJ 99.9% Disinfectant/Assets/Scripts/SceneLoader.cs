﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{	
	public AudioClip clip;

    public void loadScene(int index)
    {
        AudioSource _as = this.GetComponent<AudioSource>();
    	_as.PlayOneShot(clip);
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }
}
