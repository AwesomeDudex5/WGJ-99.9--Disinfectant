using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{	
	public AudioClip clip;
    public void loadScene(int index)
    {
    	GameObject.Find("Audio Source").GetComponent<AudioSource>().PlayOneShot(clip);
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }
}
