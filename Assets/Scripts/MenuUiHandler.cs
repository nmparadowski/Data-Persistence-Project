using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;


public class MenuUiHandler : MonoBehaviour
{
    private const string ScoreTitle = "Best Score: ";

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TMP_InputField nameInput;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LoadGameData();

        titleText.text = ScoreTitle + GameManager.Instance.lastSavedData.playerName;
        playButton.interactable = false;
        nameInput.onValueChanged.AddListener(OnInputChanged);
        playButton.onClick.AddListener(StartNewGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void OnInputChanged(string input)
    {
        playButton.interactable = !string.IsNullOrEmpty(input);
    }

    private void StartNewGame()
    {
        GameManager.Instance.currentPlayer = nameInput.text;
        SceneManager.LoadScene(1);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
