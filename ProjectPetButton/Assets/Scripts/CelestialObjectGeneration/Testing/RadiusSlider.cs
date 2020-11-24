using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gebaeckmeeting.ThreeD
{
    public class RadiusSlider : MonoBehaviour
    {
        [SerializeField]
        private float _maxRadius = 50;
        [SerializeField]
        private float _minRadius = 0.1f;
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private TextMeshProUGUI _valueText;

        protected virtual void Start()
        {
            _slider.maxValue = _maxRadius;
            _slider.minValue = _minRadius;
            _slider.value = 1;
            _valueText.text = _slider.value.ToString();
            _slider.onValueChanged.AddListener(updateValue);
        }

        protected virtual void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(updateValue);
        }

        private void updateValue(float value)
        {
            int v = (int)value;
            _valueText.text = v.ToString();
            Sphere sphere = FindObjectOfType<Sphere>();
            if (sphere == null)
                return;
            sphere.Radius = value;
        }
    }
}