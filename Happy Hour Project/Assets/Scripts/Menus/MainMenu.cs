using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

public KeyCode exitKey = KeyCode.Escape;

//moves the player into the main game
public void PlayGame()
{
    SceneManager.LoadScene("Level"); 
}
//stops the application
public void QuitGame(){
	Application.Quit();

}
//brings the player to the main menu
public void MoveToMenu()
{
    SceneManager.LoadScene("MainMenu"); 
}
//shows the credits page
public void Credits()
{
    SceneManager.LoadScene("Credits"); 
}
//opens the options page
public void Options()
{
    SceneManager.LoadScene("OptionsMenu(Home)"); 
}

//brings the player to the main menu
   public void Escape()
     {
       if(Input.GetKey(exitKey))
        {
            SceneManager.LoadScene("MainMenu"); 
        }
     }
}
