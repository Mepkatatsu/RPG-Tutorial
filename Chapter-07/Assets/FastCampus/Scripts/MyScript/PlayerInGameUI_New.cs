using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInGameUI_New : MonoBehaviour
{
    public StatsObject_New _playerStats;


    public Text _levelText;
    public Image _healthSlider;
    public Image _manaSlider;

    // Start is called before the first frame update
    void Start()
    {
        _levelText.text = _playerStats._level.ToString("n0");

        _healthSlider.fillAmount = _playerStats.HealthPercentage;
        _manaSlider.fillAmount = _playerStats.HealthPercentage;
    }

    private void OnEnable()
    {
        _playerStats.OnChangedStats += OnChangedStats;
    }

    private void OnDisable()
    {
        _playerStats.OnChangedStats -= OnChangedStats;
    }

    private void OnChangedStats(StatsObject_New statsObject)
    {
        _levelText.text = _playerStats._level.ToString("n0");

        _healthSlider.fillAmount = _playerStats.HealthPercentage;
        _manaSlider.fillAmount = _playerStats.HealthPercentage;
    }
}
