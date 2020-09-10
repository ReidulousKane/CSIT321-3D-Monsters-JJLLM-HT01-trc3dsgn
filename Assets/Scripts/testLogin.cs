using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class testLogin : MonoBehaviour
{
    Transform canvas;
    Transform responseText;
    TMP_InputField testUsernameField;
    TMP_InputField testPWordField;

    // Start is called before the first frame update
    void Start()
    {
        canvas = this.gameObject.transform;
        responseText = canvas.Find("loginResponse");
    	testUsernameField = canvas.Find("UsernameLogin").GetComponent<TMP_InputField>();
    	testPWordField = canvas.Find("PWordLogin").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (testUsernameField.isFocused && Input.GetKeyDown( KeyCode.Tab ) )
        {
        	testUsernameField.DeactivateInputField();
        	testPWordField.ActivateInputField();
	    }
	    if ( testPWordField.isFocused )
	    {
	    	if ( Input.GetKeyDown( KeyCode.Return ) )
	    	{
	    		LoginButtonClicked();
	    	}
	    }
    }

    public void LoginButtonClicked()
    {
		string testUsername = testUsernameField.text;
		string testPWord = testPWordField.text;
		Debug.Log(testUsername + " " + testPWord);
        string hash = MD5.Md5Sum(testUsername + testPWord);
        StartCoroutine(testServerConnect.GetAuthenticatedTest(testUsername, hash, responseText));
    }
}