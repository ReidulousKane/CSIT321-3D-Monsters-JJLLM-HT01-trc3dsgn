using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class text2ServerTest : MonoBehaviour
{
    private bool enterUp = true, testInputFocus = false;
    Transform canvas;
    Transform testInputFieldTransform;
    TMP_InputField testInputField;
    
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
    	if ( testInputField.isFocused ) testInputFocus = true;
		if ( Input.GetKeyDown(KeyCode.Return) && enterUp && testInputField.text != "" && testInputFocus)
    	{
    		string input = testInputField.text;
    		enterUp = false;
    		StartCoroutine(testServerConnect.PostTestStr( input ));
    	}
    	if ( Input.GetKeyDown(KeyCode.Return) && !enterUp )
    	{
    		enterUp = true;
            testInputFocus = false;
    		StartCoroutine( testServerConnect.GetTestStr());
    	}
    }
}
