
using TMPro;
using UnityEngine;

public class UICoinsText : MonoBehaviour
{

    TMP_Text _text;
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        GameManager.Instance.OnCoinsChanged += HandleCoinsChanged;
        _text.SetText("0");
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnCoinsChanged -= HandleCoinsChanged;
    }

    private void HandleCoinsChanged(int currentCoins)
    {
        _text.SetText(currentCoins.ToString());
    }
}

