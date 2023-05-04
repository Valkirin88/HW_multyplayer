
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OpenGame : MonoBehaviour
{
    [SerializeField]
    private Button _signInButton;

    [SerializeField]
    private Canvas _gameCanvas;

    private void Start()
    {
        _signInButton.onClick.AddListener(OpenGameCanvas);
    }

    private void OpenGameCanvas()
    {
        _gameCanvas.enabled = true;
        this.gameObject.SetActive(false);
    }
}
