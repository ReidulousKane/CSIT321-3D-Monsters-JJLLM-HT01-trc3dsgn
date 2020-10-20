using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

public static class ServerConnect : object
{
	private static string secretKey = "gamesvideoyeahfuck"; // sKey 4 server
    // public static string StringURL = "http://localhost/Connect.php?";
    // public static string ReturnURL = "http://localhost/Return.php";
    // public static string UserRegistrationURL = "http://localhost/Registration.php?";
    // public static string LoginURL = "http://localhost/Login.php?";
    // public static string PWChangeURL = "http://localhost/PasswordChange.php?";
    public static string StringURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/Connect.php?";
    public static string ReturnURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/Return.php";
    public static string UserRegistrationURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/Registration.php?";
    public static string LoginURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/Login.php?";
    public static string PWChangeURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/PasswordChange.php?";

    // remember to use StartCoroutine when calling this function!
    public static IEnumerator PostUserRegistration(string User, string Email, string PWHash)
    {
        //This connects to a server side php script that will add the String and score to a MySQL DB.
        // Supply it with a string representing the players String and the players score.
        string hash = MD5.Md5Sum(User + Email + PWHash + secretKey);
 
        string post_str = "User=" + UnityWebRequest.EscapeURL(User) + "&Email=" + UnityWebRequest.EscapeURL(Email) + "&PwordHash=" + UnityWebRequest.EscapeURL(PWHash) + "&hash=" + hash;
        Debug.Log(post_str);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("UserName", User));
        formData.Add(new MultipartFormDataSection("Email", Email));
        formData.Add(new MultipartFormDataSection("PWHash", PWHash));
        formData.Add(new MultipartFormDataSection("hash", hash));

        // Post the URL to the site and create a download object to get the result.
        UnityWebRequest hs_post = UnityWebRequest.Post(UserRegistrationURL, formData);
        yield return hs_post.SendWebRequest(); // Wait until the download is done
 
        if (hs_post.error != null)
        {
            Debug.Log("There was an error registering the new user: " + hs_post.error);
        }
        else
        {
            Debug.Log( hs_post.downloadHandler.text );
        }
    }

    // remember to use StartCoroutine when calling this function!
    public static IEnumerator GetAuthenticated(string User, string PWHash)
    {
        //This connects to a server side php script that will add the String and score to a MySQL DB.
        // Supply it with a string representing the players String and the players score.
        string hash = MD5.Md5Sum(User + PWHash + secretKey);
 
        string post_str = "User=" + UnityWebRequest.EscapeURL(User) + "&PwordHash=" + UnityWebRequest.EscapeURL(PWHash) + "&hash=" + hash;
        Debug.Log(post_str);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("UserName", User));
        formData.Add(new MultipartFormDataSection("PWHash", PWHash));
        formData.Add(new MultipartFormDataSection("hash", hash));
 
        // Post the URL to the site and create a download object to get the result.
        UnityWebRequest hs_post = UnityWebRequest.Post(LoginURL, formData);
        yield return hs_post.SendWebRequest(); // Wait until the download is done
 
        if (hs_post.error != null)
        {
            Debug.Log("There was an error posting the string: " + hs_post.error);
        }
        else
        {
            Debug.Log( hs_post.downloadHandler.text );
            SceneManager.LoadScene("Browse page");
        }
    }

    // remember to use StartCoroutine when calling this function!
    public static IEnumerator PostChangePassword(string User, string OldPWHash, string NewPWHash)
    {
        //This connects to a server side php script that will add the String and score to a MySQL DB.
        // Supply it with a string representing the players String and the players score.
        string hash = MD5.Md5Sum(User + OldPWHash + NewPWHash + secretKey);
 
        string post_str = "User=" + UnityWebRequest.EscapeURL(User) + "OldPWHash=" + UnityWebRequest.EscapeURL(OldPWHash) + "NewPWHash=" + UnityWebRequest.EscapeURL(NewPWHash) + "&hash=" + hash;
        Debug.Log(post_str);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("UserName", User));
        formData.Add(new MultipartFormDataSection("OldPWHash", OldPWHash));
        formData.Add(new MultipartFormDataSection("NewPWHash", NewPWHash));
        formData.Add(new MultipartFormDataSection("hash", hash));
 
        // Post the URL to the site and create a download object to get the result.
        UnityWebRequest hs_post = UnityWebRequest.Post(PWChangeURL, formData);
        yield return hs_post.SendWebRequest(); // Wait until the download is done
 
        if (hs_post.error != null)
        {
            Debug.Log("There was an error changing your password: " + hs_post.error);
        }
        else
        {
            Debug.Log( hs_post.downloadHandler.text );
        }
    }
}