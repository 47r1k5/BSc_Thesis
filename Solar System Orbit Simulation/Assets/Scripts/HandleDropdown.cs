using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandleDropdown : MonoBehaviour
{
    private TMP_Dropdown CardTypeDropdown;
    [SerializeField] private GameObject cardSpawner;

    private SpawnCards spawnCards;
    void Start()
    {
        //Fetch the Dropdown GameObject
        CardTypeDropdown = GetComponent<TMP_Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        CardTypeDropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(CardTypeDropdown);
        });
        spawnCards = cardSpawner.GetComponent<SpawnCards>();
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(TMP_Dropdown change)
    {
        foreach (var card in GameObject.FindGameObjectsWithTag("Card"))
        {
            Destroy(card);
        }
        switch (change.value)
        {
            case 1:
                spawnCards.LoadStarCards();
                break;
            case 2:
                spawnCards.LoadPlanetCards();
                break;
            case 3:
                spawnCards.LoadMoonCards();
                break;
            default:
                spawnCards.LoadAllCards();
                break;
        }
    }
}
