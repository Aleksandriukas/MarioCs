using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public Text errorText;
    public GameObject errorPanel;

    public Button submitButton;

    public void GoToRegister()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }


    public void CallLogin()
    {

        StartCoroutine(LoginPlayer());

    }

    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);

        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);

        yield return www;

        if (www.text[0] == '0')
        {

            errorPanel.active = false;

            UnityEngine.SceneManagement.SceneManager.LoadScene(2);

        }

        else
        {

            //I need to pannel appear
            errorPanel.active = true;
            errorText.text = "User login failed. Error #" + www.text;
            Debug.Log("User login failed. Error #" + www.text);
        }

    }

    public void VerifyInputs()
    {
        submitButton.interactable = (username.text.Length >= 8 && password.text.Length >= 8);
    }

}
