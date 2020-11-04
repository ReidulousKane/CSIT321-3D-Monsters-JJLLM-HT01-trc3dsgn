using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

using MonsterClass;

public class MonsterCustomisation : MonoBehaviour
{
    Transform canvas;
    // character base model, custom name, body colour?, accessory bool, accessory colour, shoe colour
    TMP_Dropdown characterDropdown;
    TMP_InputField customNameField;
    TMP_Dropdown bodyColourDropdown;
    bool accessory;
    TMP_Dropdown accessoryColourDropdown;
    TMP_Dropdown shoeColourDropdown;

    TMP_Dropdown creationsDropdown;

    string []characters = { "Grunt", "Orbi", "Peanut", "Pod", "Puff", "Spike" };
    string []colours = { "Red", "Green", "Blue" };

    // Start is called before the first frame update
    void Start()
    {
    	accessory = true;
        canvas = this.gameObject.transform;
        characterDropdown = canvas.Find( "CharacterDropdown" ).GetComponent<TMP_Dropdown>();
        customNameField = canvas.Find( "NameInputField (TMP)" ).GetComponent<TMP_InputField>();
        bodyColourDropdown = canvas.Find( "BodyColourDropdown" ).GetComponent<TMP_Dropdown>();
        accessoryColourDropdown = canvas.Find( "AccessoryColourDropdown" ).GetComponent<TMP_Dropdown>();
        shoeColourDropdown = canvas.Find( "ShoeColourDropdown" ).GetComponent<TMP_Dropdown>();

        creationsDropdown = canvas.Find( "CreationsDropdown" ).GetComponent<TMP_Dropdown>();

        // This will fetch characters of a user that has an account and has logged in
        if(ServerConnect.getUser() != "") FetchCharacters();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ValueChanged()
    {
        if( accessory ) accessory = false;
        else accessory = true;
    }

    public void SaveCharacter()
    {
    	MonsterJSON saveMon = new MonsterJSON();
    	saveMon.baseCharacter = characters[characterDropdown.value];
    	saveMon.charName = customNameField.text;
    	saveMon.bodyColour = colours[bodyColourDropdown.value];
    	saveMon.hasAccessory = accessory;
    	saveMon.accessoryColour = colours[accessoryColourDropdown.value];
    	saveMon.shoeColour = colours[shoeColourDropdown.value];
    	Debug.Log( characters[characterDropdown.value] + " "
    		+ customNameField.text + " "
    		+ colours[bodyColourDropdown.value] + " "
    		+ accessory + " "
    		+ colours[accessoryColourDropdown.value] + " "
    		+ colours[shoeColourDropdown.value] );
    	string monster = JsonUtility.ToJson(saveMon);
    	Debug.Log(monster);
    	StartCoroutine( ServerConnect.SaveMonster( monster, creationsDropdown ) );
    }

    public void FetchCharacters()
    {
    	StartCoroutine( ServerConnect.FetchMonsters( creationsDropdown ) );
    }
}