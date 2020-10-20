using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangePassword : MonoBehaviour
{
    Transform canvas;
    Transform responseText;
    TMP_InputField testUsernameField;
    TMP_InputField testOldPWField;
    TMP_InputField testNewPWField;
    TMP_InputField testConfirmNewPWField;

    // Start is called before the first frame update
    void Start()
    {
        canvas = this.gameObject.transform;
    	responseText = canvas.Find("ChangePWResponse");
    	testUsernameField = canvas.Find("UserNameCPW").GetComponent<TMP_InputField>();
    	testOldPWField = canvas.Find("OldPWInput").GetComponent<TMP_InputField>();
    	testNewPWField = canvas.Find("NewPWInput").GetComponent<TMP_InputField>();
    	testConfirmNewPWField = canvas.Find("NewPWConfirm").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (testUsernameField.isFocused && Input.GetKeyDown( KeyCode.Tab ) )
        {
        	testUsernameField.DeactivateInputField();
        	testOldPWField.ActivateInputField();
        }
        if (testOldPWField.isFocused && Input.GetKeyDown( KeyCode.Tab ) )
        {
        	testOldPWField.DeactivateInputField();
        	testNewPWField.ActivateInputField();
        }
        if (testNewPWField.isFocused && Input.GetKeyDown( KeyCode.Tab ) )
        {
        	testNewPWField.DeactivateInputField();
        	testConfirmNewPWField.ActivateInputField();
        }
        if ( testConfirmNewPWField.isFocused )
        {
    		if ( Input.GetKeyDown( KeyCode.Return ) )
	    	{
	    		ChangePasswordButtonClicked();
	    	}
	    }
    }

    public void ChangePasswordButtonClicked()
    {
    	string usernameText = testUsernameField.text;
    	string oldPWText = testOldPWField.text;
    	string newPWText = testNewPWField.text;
    	string confirmNewPWText = testNewPWField.text;
    	Debug.Log( usernameText + " " + oldPWText + " " + newPWText + " " + confirmNewPWText );
    	if ( newPWText == confirmNewPWText )
    	{
    		string oldPWhash = MD5.Md5Sum( usernameText + oldPWText );
	        string newPWhash = MD5.Md5Sum( usernameText + newPWText );
	        StartCoroutine( testServerConnect.PostTestChangePassword( usernameText, oldPWhash, newPWhash ));
    	}
    }
}
