using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1000)]
public class Login : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI bestScoreText;
    [SerializeField] private TMPro.TMP_InputField usernameInputField;

    private void Awake()
    {
        usernameInputField.onEndEdit.AddListener((string e) => 
        {
            DataPersistance.Instance.SavePlayerName(e);
        });
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var data = DataPersistance.Instance.PlayerData;

        usernameInputField.text = $"{data.PlayerName}";
        bestScoreText.SetText($"Best Score: {data.BestScore}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
