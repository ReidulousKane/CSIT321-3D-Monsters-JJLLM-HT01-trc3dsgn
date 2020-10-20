using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Login : MonoBehaviour
{
    Transform canvas;
    //Transform responseText;
    TMP_InputField UsernameField;
    TMP_InputField PWordField;

    // Start is called before the first frame update
    void Start()
    {
        canvas = this.gameObject.transform;
        //responseText = canvas.Find("loginResponse");
    	UsernameField = canvas.Find("UserField (TMP)").GetComponent<TMP_InputField>();
    	PWordField = canvas.Find("PasswordField (TMP)").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( UsernameField.isFocused && Input.GetKeyDown( KeyCode.Tab ) )
        {
        	UsernameField.DeactivateInputField();
        	PWordField.ActivateInputField();
	    }
	    if ( PWordField.isFocused )
	    {
	    	if ( Input.GetKeyDown( KeyCode.Return ) )
	    	{
	    		LoginButtonClicked();
	    	}
	    }
    }

    public void LoginButtonClicked()
    {
		string Username = UsernameField.text;
		string PWord = PWordField.text;
		Debug.Log(Username + " " + PWord);
        string hash = MD5.Md5Sum(Username + PWord);
        StartCoroutine( ServerConnect.GetAuthenticated( Username, hash ));
    }

    public void UserWantsToRegister()
    {
        SceneManager.LoadScene("Register page");
    }

    public void UseAsGuest()
    {
        SceneManager.LoadScene("Browse page");
    }
}