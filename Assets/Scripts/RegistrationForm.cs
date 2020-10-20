using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;

public class RegistrationForm : MonoBehaviour
{
    Transform canvas;
    //Transform responseText;
    TMP_InputField UsernameField;
    TMP_InputField EmailField;
    TMP_InputField PWordField;
    TMP_InputField ConfirmPWordField;

    // Start is called before the first frame update
    void Start()
    {
    	canvas = this.gameObject.transform;
    	//responseText = canvas.Find("RegResponse");
    	UsernameField = canvas.Find("UserNameField (TMP)").GetComponent<TMP_InputField>();
    	EmailField = canvas.Find("EmailField (TMP)").GetComponent<TMP_InputField>();
    	PWordField = canvas.Find("PWField (TMP)").GetComponent<TMP_InputField>();
    	ConfirmPWordField = canvas.Find("ConfirmPWField (TMP)").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( UsernameField.isFocused && Input.GetKeyDown( KeyCode.Tab ) )
        {
        	UsernameField.DeactivateInputField();
        	EmailField.ActivateInputField();
        }
        if ( EmailField.isFocused && Input.GetKeyDown( KeyCode.Tab ) )
        {
        	EmailField.DeactivateInputField();
        	PWordField.ActivateInputField();
        }
        if ( PWordField.isFocused && Input.GetKeyDown( KeyCode.Tab ) )
        {
        	PWordField.DeactivateInputField();
        	ConfirmPWordField.ActivateInputField();
        }
        if ( ConfirmPWordField.isFocused )
        {
    		if ( Input.GetKeyDown( KeyCode.Return ) )
	    	{
	    		RegisterButtonClicked();
	    	}
	    }
    }

    public void RegisterButtonClicked()
    {
    	string usernameText = UsernameField.text;
    	string emailText = EmailField.text;
    	string PWText = PWordField.text;
    	string confirmPWText = ConfirmPWordField.text;
    	if ( PWText == confirmPWText )
    	{
    		Debug.Log( usernameText + " " + emailText + " " + PWText + " " + confirmPWText );
	        string pwhash = MD5.Md5Sum(usernameText + PWText);
	        StartCoroutine( ServerConnect.PostUserRegistration( usernameText, emailText, pwhash ));
    	}
    	else Debug.Log( "Passwords do not match!" );
    }

    public void AlreadyRegistered()
    {
        SceneManager.LoadScene("Login page");
    }

    public void UseAsGuest()
    {
        SceneManager.LoadScene("Browse page");
    }
}
