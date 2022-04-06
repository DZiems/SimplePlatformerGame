using TMPro;
using UnityEngine;

public class UILivesText : MonoBehaviour
{
    TMP_Text _text;
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        GameManager.Instance.OnLivesChanged += HandleLivesChanged;
        _text.SetText(GameManager.Instance.Lives.ToString());
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLivesChanged -= HandleLivesChanged;
    }

    private void HandleLivesChanged(int livesRemaining)
    {
        _text.SetText(livesRemaining.ToString());
    }
}
