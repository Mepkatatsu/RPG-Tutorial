using FastCampus.UIs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBattleUI_New : MonoBehaviour
{
    private Slider _hpSlider;

    [SerializeField]
    private GameObject _damageTextPrefab;

    public float MinimumValue
    {
        get => _hpSlider.minValue;
        set
        {
            _hpSlider.minValue = value;
        }
    }

    public float MaximumValue
    {
        get => _hpSlider.maxValue;
        set
        {
            _hpSlider.maxValue = value;
        }
    }

    public float Value
    {
        get => _hpSlider.value;
        set
        {
            _hpSlider.value = value;
        }
    }

    private void Awake()
    {
        _hpSlider = GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        GetComponent<Canvas>().enabled = true;
    }

    private void OnDisable()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void CreateDamageText(int damage)
    {
        if (_damageTextPrefab != null)
        {
            GameObject damageTextGO = Instantiate(_damageTextPrefab, transform);
            DamageText_New damageText = damageTextGO.GetComponent<DamageText_New>();
            if (damageText == null)
            {
                Destroy(damageTextGO);
            }

            damageText.Damage = damage;
        }
    }
}
