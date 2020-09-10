using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class testRegistrationForm : MonoBehaviour
{
    Transform canvas;
    Transform responseText;
    TMP_InputField testUsernameField;
    TMP_InputField testEmailField;
    TMP_InputField testPWordField;
    TMP_InputField testConfirmPWordField;

    // Start is called before the first frame update
    void Start()
    {
    	canvas = this.gameObject.transform;
    	responseText = canvas.Find("RegResponse");
    	testUsernameField = canvas.Find("UserNameInput").GetComponent<TMP_InputField>();
    	testEmailField = canvas.Find("EmailInput").GetComponent<TMP_InputField>();
    	testPWordField = canvas.Find("PWInput").GetComponent<TMP_InputField>();
    	testConfirmPWordField = canvas.Find("PWConfirm").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (testUsernameField.isFocused && Input.GetKeyDown( KeyCode.Tab ) )
        {
        	testUsernameField.DeactivateInputField();
        	testEmailField.ActivateInputField();
        }
        if (testEmailField.isFocused && Input.GetKeyDown( KeyCode.Tab ) )
        {
        	testEmailField.DeactivateInputField();
        	testPWordField.ActivateInputField();
        }
        if (testPWordField.isFocused && Input.GetKeyDown( KeyCode.Tab ) )
        {
        	testPWordField.DeactivateInputField();
        	testConfirmPWordField.ActivateInputField();
        }
        if ( testConfirmPWordField.isFocused )
        {
    		if ( Input.GetKeyDown( KeyCode.Return ) )
	    	{
	    		RegisterButtonClicked();
	    	}
	    }
    }

    public void RegisterButtonClicked()
    {
    	// about to test sending all this to localhost
    	string usernameText = testUsernameField.text;
    	string emailText = testEmailField.text;
    	string PWText = testPWordField.text;
    	string confirmPWText = testConfirmPWordField.text;
    	Debug.Log( usernameText + " " + emailText + " " + PWText + " " + confirmPWText );
        string hash = MD5.Md5Sum(usernameText + PWText);
        StartCoroutine(testServerConnect.PostTestUserRegistration(usernameText, emailText, hash, responseText));
    }
}
