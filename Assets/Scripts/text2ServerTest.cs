using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class text2ServerTest : MonoBehaviour
{
    private bool enterUp = true;
    // Start is called before the first frame update
    void Start()
    {
    	this.gameObject.transform.Find( "Text (TMP)" ).GetComponent<TextMeshProUGUI>().SetText( "Loading..." );        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Return) && enterUp && this.gameObject.transform.GetChild(0).GetChild(0).Find( "Text" ).GetComponent<TextMeshProUGUI>().text != "" )
    	{
    		enterUp = false;
    		Debug.Log( "Detected key down: Return" );
    		string input = this.gameObject.transform.GetChild(0).GetChild(0).Find( "Text" ).GetComponent<TextMeshProUGUI>().text;
    		Debug.Log( input );
    		StartCoroutine(testServerConnect.PostTestStr( input ));
    	}
    	if ( Input.GetKeyDown(KeyCode.Return) && !enterUp )
    	{
    		enterUp = true;
    		Debug.Log( "Detected key up: Return" );
    		StartCoroutine(testServerConnect.GetTestStr( this.gameObject.transform.Find( "Text (TMP)" )/*.GetComponent<TextMeshProUGUI>()*/));
    	} 
    }
}
