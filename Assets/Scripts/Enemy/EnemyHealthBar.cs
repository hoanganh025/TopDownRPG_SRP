using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fillHeathBar;
    [SerializeField] private Vector3 offSet;

    private void Start()
    {
        fillHeathBar.color = gradient.Evaluate(1f);
        slider.gameObject.SetActive(false);
    }

    public void UpdateEnemyHealthBar(float _maxHealth, float _currentHealth)
    {
        slider.gameObject.SetActive(true);
        slider.value = _currentHealth / _maxHealth;

        fillHeathBar.color = gradient.Evaluate(slider.normalizedValue);
    }

    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offSet);

    }
}
