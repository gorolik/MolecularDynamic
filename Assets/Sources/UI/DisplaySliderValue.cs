using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Behaviour.UI
{
    public class DisplaySliderValue : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Slider _slider;

        private void Start() => 
            OnValueChanged(_slider.value);

        private void OnEnable() => 
            _slider.onValueChanged.AddListener(OnValueChanged);

        private void OnDisable() => 
            _slider.onValueChanged.RemoveListener(OnValueChanged);

        private void OnValueChanged(float value) => 
            _text.text = value.ToString();
    }
}
