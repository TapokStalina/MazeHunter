using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0;

    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1;

    }
    public void OpenButton(GameObject panel)
    {
        panel.SetActive(true);

    }
    public void CloseButton(GameObject panel)
    {
        panel.SetActive(false);

    }
}
