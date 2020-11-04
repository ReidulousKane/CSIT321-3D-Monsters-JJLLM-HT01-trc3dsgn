using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    public void WelcomeOnClick()
    {
    	SceneManager.LoadScene("Login page");
    }

    public void UseAsGuest()
    {
        SceneManager.LoadScene("Browse page");
    }
}
