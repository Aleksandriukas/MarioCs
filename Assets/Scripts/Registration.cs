using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    public InputField username;
    public InputField password;

    public Text errorText;
    public GameObject errorPanel;

    public Button submitButton;


    public void CallRegister()
    {

        StartCoroutine(Register());

    }


    public void BackToLogin()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    IEnumerator Register() {
    
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);

        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        
        yield return www;

        if(www.text == "0"){
            errorPanel.active = false;
            Debug.Log("User created successfully.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            errorPanel.active = true;
            errorText.text = "User login failed. Error #" + www.text;
            Debug.Log("User creation failed. Error #" + www.text);
        }

    }
    
    public void VerifyInputs()
    {
        submitButton.interactable = (username.text.Length >= 8 && password.text.Length >= 8);
    }


}
