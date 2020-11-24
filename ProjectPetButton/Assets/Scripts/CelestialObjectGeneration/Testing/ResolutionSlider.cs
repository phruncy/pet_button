using Gebaeckmeeting.ThreeD;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gebaeckmeeting.PetButton
{
    public class ResolutionSlider : MonoBehaviour
    {
        [SerializeField]
        private int _maxResolution = 300;
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private TextMeshProUGUI _valueText;

        protected virtual void Start()
		{
            _slider.maxValue = _maxResolution;
            _slider.minValue = 1;
            _slider.value = 5;
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
            sphere.Resolution = v;
        }
    }

}