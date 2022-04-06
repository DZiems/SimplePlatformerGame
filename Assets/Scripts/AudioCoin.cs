using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCoin : MonoBehaviour
{
    [SerializeField] private AudioClip[] _coinSounds;
    
    private AudioSource _audioSource;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.OnCoinsChanged += PlayCoinSound;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCoinsChanged -= PlayCoinSound;
    }

    private void PlayCoinSound(int coins)
    {
        if (_coinSounds.Length > 0)
        {
            int ind = UnityEngine.Random.Range(0, _coinSounds.Length);
            _audioSource.PlayOneShot(_coinSounds[ind]);
        }
    }

}
