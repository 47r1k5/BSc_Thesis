using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //TODO
    //Info still not implemented

    public void StartSimulation()
    {
        SceneManager.LoadSceneAsync("Solar System Simulation");
    }
    public void ViewCelestialBodyCards()
    {
        SceneManager.LoadSceneAsync("Cards");
    }
    public void Info()
    {
        SceneManager.LoadSceneAsync("Info");
    }
    public void CloseApp()
    {
        Application.Quit();
    }
}
