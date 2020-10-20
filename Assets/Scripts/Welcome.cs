using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WelcomeOnClick()
    {
    	SceneManager.LoadScene("Login page");
    }

    public void UseAsGuest()
    {
        SceneManager.LoadScene("Browse page");
    }
}
