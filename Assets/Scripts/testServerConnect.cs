using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public static class testServerConnect : object
{
	private static string secretKey = "gamesvideoyeahfuck"; // secret key for apache
    // public static string testStringURL = "http://localhost/testConnect.php?";
    // public static string testReturnURL = "http://localhost/testReturn.php";
    // public static string testUserRegistrationURL = "http://localhost/testRegistration.php?";
    // public static string testLoginURL = "http://localhost/testLogin.php?";
    public static string testStringURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/testConnect.php?";
    public static string testReturnURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/testReturn.php";
    public static string testUserRegistrationURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/testRegistration.php?";
    public static string testLoginURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/testLogin.php?";

    // remember to use StartCoroutine when calling this function!
    public static IEnumerator PostTestStr(string testString)
    {
        //This connects to a server side php script that will add the testString and score to a MySQL DB.
        // Supply it with a string representing the players testString and the players score.
        string hash = MD5.Md5Sum(testString + secretKey);
 
        string post_str = "testString=" + UnityWebRequest.EscapeURL(testString) + "&hash=" + hash;
        Debug.Log(post_str);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("testString", testString));
        formData.Add(new MultipartFormDataSection("hash", hash));
 
        // Post the URL to the site and create a download object to get the result.
        UnityWebRequest hs_post = UnityWebRequest.Post(testStringURL, formData);
        yield return hs_post.SendWebRequest(); // Wait until the download is done
 
        if (hs_post.error != null)
        {
            Debug.Log("There was an error posting the string: " + hs_post.error);
        }
        else
        {
        	Debug.Log("Success!");
        	Debug.Log( hs_post.downloadHandler.text );
        }
    }
 
    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    public static IEnumerator GetTestStr(Transform gOb)
    {
        gOb.GetComponent<TextMeshProUGUI>().SetText( "Loading String..." );
        UnityWebRequest hs_get = UnityWebRequest.Get(testReturnURL);
        yield return new WaitForSeconds(3.75f);
        yield return hs_get.SendWebRequest();
 
        if (hs_get.error != null)
        {
            Debug.Log(hs_get.error);
        }
        else
        {
        	Debug.Log(hs_get.downloadHandler.text);
            gOb.GetComponent<TextMeshProUGUI>().SetText( hs_get.downloadHandler.text ); 
        }
    }

    // remember to use StartCoroutine when calling this function!
    public static IEnumerator PostTestUserRegistration(string testUser, string testEmail, string testPWHash, Transform gOb)
    {
        //This connects to a server side php script that will add the testString and score to a MySQL DB.
        // Supply it with a string representing the players testString and the players score.
        string hash = MD5.Md5Sum(testUser + testEmail + testPWHash + secretKey);
 
        string post_str = "testUser=" + UnityWebRequest.EscapeURL(testUser) + "testEmail=" + UnityWebRequest.EscapeURL(testEmail) + "testPwordHash=" + UnityWebRequest.EscapeURL(testPWHash) + "&hash=" + hash;
        Debug.Log(post_str);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("testUserName", testUser));
        formData.Add(new MultipartFormDataSection("testEmail", testEmail));
        formData.Add(new MultipartFormDataSection("testPwordHash", testPWHash));
        formData.Add(new MultipartFormDataSection("hash", hash));
 
        // Post the URL to the site and create a download object to get the result.
        UnityWebRequest hs_post = UnityWebRequest.Post(testUserRegistrationURL, formData);
        yield return hs_post.SendWebRequest(); // Wait until the download is done
 
        if (hs_post.error != null)
        {
            Debug.Log("There was an error posting the string: " + hs_post.error);
        }
        else
        {
            Debug.Log("Success!\n" + hs_post.downloadHandler.text );
            gOb.GetComponent<TextMeshProUGUI>().SetText( hs_post.downloadHandler.text ); 
        }
    }

    // remember to use StartCoroutine when calling this function!
    public static IEnumerator GetAuthenticatedTest(string testUser, string testPWHash, Transform gOb)
    {
        //This connects to a server side php script that will add the testString and score to a MySQL DB.
        // Supply it with a string representing the players testString and the players score.
        string hash = MD5.Md5Sum(testUser + testPWHash + secretKey);
 
        string post_str = "testUser=" + UnityWebRequest.EscapeURL(testUser) + "&testPwordHash=" + UnityWebRequest.EscapeURL(testPWHash) + "&hash=" + hash;
        Debug.Log(post_str);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("testUserName", testUser));
        formData.Add(new MultipartFormDataSection("testPWHash", testPWHash));
        formData.Add(new MultipartFormDataSection("hash", hash));
 
        // Post the URL to the site and create a download object to get the result.
        UnityWebRequest hs_post = UnityWebRequest.Post(testLoginURL, formData);
        yield return hs_post.SendWebRequest(); // Wait until the download is done
 
        if (hs_post.error != null)
        {
            Debug.Log("There was an error posting the string: " + hs_post.error);
        }
        else
        {
            Debug.Log("Success!\n" + hs_post.downloadHandler.text );
            gOb.GetComponent<TextMeshProUGUI>().SetText( hs_post.downloadHandler.text ); 
        }
    }
}