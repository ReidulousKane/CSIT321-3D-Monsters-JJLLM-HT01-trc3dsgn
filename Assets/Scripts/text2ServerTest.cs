using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class text2ServerTest : MonoBehaviour
{
    private bool enterUp = true;
    Transform canvas;
    Transform testInputFieldTransform;
    TMP_InputField testInputField;
    // Button RegisterButton;
    
    // Start is called before the first frame update
    void Start()
    {
    	canvas = this.gameObject.transform;
    	canvas.Find( "Text (TMP)" ).GetComponent<TextMeshProUGUI>().SetText( "Loading..." );
    	testInputField = canvas.Find("testInputField").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
    	if( testInputField.isFocused )
    	{
			if ( Input.GetKeyDown(KeyCode.Return) && enterUp && testInputField.text != "" )
	    	{
	    		string input = testInputField.text;
	    		enterUp = false;
	    		Debug.Log( "Detected key down: Return" );
	    		Debug.Log( input );
	    		StartCoroutine(testServerConnect.PostTestStr( input ));
	    	}
	    	if ( Input.GetKeyDown(KeyCode.Return) && !enterUp )
	    	{
	    		enterUp = true;
	    		Debug.Log( "Detected key up: Return" );
	    		StartCoroutine(testServerConnect.GetTestStr( this.gameObject.transform.Find( "Text (TMP)" )));
	    	}
	   	}
    }

    public void RegisterButtonClicked()
    {
    	// about to test sending all this to localhost
    	string usernameText = canvas.Find( "UserNameInput" ).GetComponent<TMP_InputField>().text;
    	string emailText = canvas.Find( "EmailInput" ).GetComponent<TMP_InputField>().text;
    	string PWText = canvas.Find( "PWInput" ).GetComponent<TMP_InputField>().text;
    	string confirmPWText = canvas.Find( "PWConfirm" ).GetComponent<TMP_InputField>().text;
    	Debug.Log( usernameText + " " + emailText + " " + PWText + " " + confirmPWText );
    }
}