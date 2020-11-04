using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

using MonsterClass;

public static class ServerConnect : object
{
	private static string secretKey = "gamesvideoyeahfuck"; // sKey 4 server
    public static string UserRegistrationURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/Registration.php?";
    public static string LoginURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/Login.php?";
    public static string PWChangeURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/PasswordChange.php?";
    public static string SaveMonsterURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/SaveMonster.php?";
    public static string FetchMonsterURL = "http://3dmonstersuow.heliohost.org/3DMonstersPHP/FetchMonsters.php?";

    private static string currentUser;
    private static string currentProfile;
    private static List<MonsterJSON> currentCreationList;

    public static string getUser(){ return currentUser; }
    public static void setUser( string user ){ currentUser = user; }
    public static string getProfile(){ return currentProfile; }
    public static void setProfile( string profile ){ currentProfile = profile; }
    public static List<MonsterJSON> getCreations(){ return currentCreationList; }
    public static void setCreations( List<MonsterJSON> creationList ){ currentCreationList = creationList; }
    public static void addCreations( MonsterJSON newCreation ){ currentCreationList.Add( newCreation ); }


    // remember to use StartCoroutine when calling this function!
    public static IEnumerator PostUserRegistration(string User, string Email, string PWHash)
    {
        //This connects to a server side php script that will add the String and score to a MySQL DB.
        //supplied with user information for registration
        string hash = MD5.Md5Sum(User + Email + PWHash + secretKey);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("UserName", User));
        formData.Add(new MultipartFormDataSection("Email", Email));
        formData.Add(new MultipartFormDataSection("PWHash", PWHash));
        formData.Add(new MultipartFormDataSection("hash", hash));

        // Post the URL to the site and create a download object to get the result.
        UnityWebRequest monster_post = UnityWebRequest.Post(UserRegistrationURL, formData);
        yield return monster_post.SendWebRequest(); // Wait until the download is done
 
        if (monster_post.error != null)
        {
            Debug.Log("There was an error registering the new user: " + monster_post.error);
        }
        else
        {
            Debug.Log( monster_post.downloadHandler.text );
        }
    }

    // remember to use StartCoroutine when calling this function!
    public static IEnumerator GetAuthenticated(string User, string PWHash)
    {
        //This connects to a server side php script that will add the String and score to a MySQL DB.
        // Supply it with a string representing the players String and the players score.
        string hash = MD5.Md5Sum(User + PWHash + secretKey);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("UserName", User));
        formData.Add(new MultipartFormDataSection("PWHash", PWHash));
        formData.Add(new MultipartFormDataSection("hash", hash));
 
        // Post the URL to the site and create a download object to get the result.
        UnityWebRequest monster_post = UnityWebRequest.Post(LoginURL, formData);
        yield return monster_post.SendWebRequest(); // Wait until the download is done
 
        if (monster_post.error != null)
        {
            Debug.Log("There was an error posting the string: " + monster_post.error);
        }
        else
        {
            Debug.Log( monster_post.downloadHandler.text );
            setUser(User);
            setProfile("Default");
            Debug.Log( "Welcome " + getUser() );
            SceneManager.LoadScene("Browse page");
        }
    }

    // remember to use StartCoroutine when calling this function!
    public static IEnumerator PostChangePassword(string User, string OldPWHash, string NewPWHash)
    {
        //This connects to a server side php script that will add the String and score to a MySQL DB.
        // Supply it with a string representing the players String and the players score.
        string hash = MD5.Md5Sum(User + OldPWHash + NewPWHash + secretKey);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("UserName", User));
        formData.Add(new MultipartFormDataSection("OldPWHash", OldPWHash));
        formData.Add(new MultipartFormDataSection("NewPWHash", NewPWHash));
        formData.Add(new MultipartFormDataSection("hash", hash));
 
        // Post the URL to the site and create a download object to get the result.
        UnityWebRequest monster_post = UnityWebRequest.Post(PWChangeURL, formData);
        yield return monster_post.SendWebRequest(); // Wait until the download is done
 
        if (monster_post.error != null)
        {
            Debug.Log("There was an error changing your password: " + monster_post.error);
        }
        else
        {
            Debug.Log( monster_post.downloadHandler.text );
        }
    }

    // remember to use StartCoroutine when calling this function!
    // this is only ever called after a user is logged in, there is no way to save a monster
    // without having logged in, however, there should be a way to output monsters made by guests.
    public static IEnumerator SaveMonster( string monsterJSON, TMP_Dropdown creationsDropdown )
    {
        //This connects to a server side php script that will add the String and score to a MySQL DB.
        // Supply it with a string representing the players String and the players score.
        string hash = MD5.Md5Sum(currentUser + currentProfile + monsterJSON + secretKey);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("User", currentUser));
        formData.Add(new MultipartFormDataSection("Profile", currentProfile));
        formData.Add(new MultipartFormDataSection("Monster", monsterJSON));
        formData.Add(new MultipartFormDataSection("hash", hash));

        UnityWebRequest monster_post = UnityWebRequest.Post(SaveMonsterURL, formData);
        yield return monster_post.SendWebRequest();
 
        if (monster_post.error != null)
        {
            Debug.Log("There was an error saving the model: " + monster_post.error);
        }
        else
        {
            Debug.Log( monster_post.downloadHandler.text );
            MonsterJSON savedCreation = JsonUtility.FromJson<MonsterJSON>(monsterJSON);
            List<string> savedName = new List<string>();
            savedName.Add(savedCreation.charName);
            creationsDropdown.AddOptions(savedName);
        }
    }

    // remember to use StartCoroutine when calling this function!
    public static IEnumerator FetchMonsters( TMP_Dropdown creationsDropdown )
    {
        creationsDropdown.ClearOptions();
        // loads model that matches the current user, profile and specified monster name
        string hash = MD5.Md5Sum(currentUser + currentProfile + secretKey);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("User", currentUser));
        formData.Add(new MultipartFormDataSection("Profile", currentProfile));
        formData.Add(new MultipartFormDataSection("hash", hash));

        UnityWebRequest monster_post = UnityWebRequest.Post(FetchMonsterURL, formData);
        yield return monster_post.SendWebRequest();

        if (monster_post.error != null)
        {
            Debug.Log("There was an error loading your creations: " + monster_post.error);
        }
        else
        {
            Debug.Log( monster_post.downloadHandler.text );
            string []creations = monster_post.downloadHandler.text.Split('\t');
            List<string> creationNames = new List<string>();
            List<MonsterJSON> creationImport = new List<MonsterJSON>();
            foreach (string json in creations)
            {
                if("END!" == json) break;
                MonsterJSON loadedMonster = JsonUtility.FromJson<MonsterJSON>(json);
                Debug.Log(loadedMonster.charName);
                creationNames.Add(loadedMonster.charName);
                Debug.Log(creationNames[0]);
                creationImport.Add(loadedMonster);
            }
            creationsDropdown.AddOptions(creationNames);
            setCreations(creationImport);
        }
    }
}