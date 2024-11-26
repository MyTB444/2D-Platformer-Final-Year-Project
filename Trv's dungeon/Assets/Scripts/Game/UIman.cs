using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIman : MonoBehaviour
{
    [SerializeField] private Image[] _liveImages;
    [SerializeField] private TextMeshProUGUI _gameoverText;
    [SerializeField] private TextMeshProUGUI _gamewinText;
    public void DamageUpdate(int i)
    {
        _liveImages[i - 1].enabled = false;
    }
    public void GameOverSequence()
    {
        _gameoverText.gameObject.SetActive(true);
    }
    public void GameWinSequence()
    {
        _gamewinText.gameObject.SetActive(true);
    }
}
