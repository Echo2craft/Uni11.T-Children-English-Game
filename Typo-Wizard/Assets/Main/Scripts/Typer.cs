using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class Typer : MonoBehaviour
{
    public TextMeshProUGUI wordOutput = null;
    private string currentString = "";
    private DateTime lastBackspace = DateTime.Now;

    // Start is called before the first frame update
    private void Start()
    {
    }

    public string GetTypedWord()
    {
        return currentString;
    }


    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (!Input.anyKey)
        {
            return;
        }


        if (Input.GetKey(KeyCode.Backspace))
        {
            if (currentString.Length > 0 && DateTime.Now.Subtract(lastBackspace).TotalMilliseconds > 100)
            {
                currentString = currentString.Substring(0, currentString.Length - 1);
                wordOutput.text = currentString;
                lastBackspace = DateTime.Now;
            }
            return;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log($"Casting spell with string: [{currentString}]");
            CastSpell();
            return;
        }

        string stringTyped = string.Join("", Input.inputString.Where(c => char.IsLetter(c)));

        // Validate is key is a letter
        if (stringTyped.Length > 0)
        {
            UpdateText(stringTyped);
        }

    }

    public void CastSpell()
    {
        currentString = currentString.Trim().ToLower(); // Clean it up

        Debug.Log($"Trying to cast spell: [{currentString}]");

        if (Controller.INSTANCE == null)
        {
            Debug.LogWarning("Controller.INSTANCE is null!");
            return;
        }

        if (Controller.INSTANCE.CastSpell(currentString))
        {
            Debug.Log("Spell cast successfully!");
            currentString = "";
            wordOutput.text = currentString;
        }
        else
        {
            Debug.Log("Spell cast failed.");
        }
    }

    public void UpdateText(string stringTyped)
    {
        currentString += stringTyped;
        wordOutput.text = currentString;
    }

}
