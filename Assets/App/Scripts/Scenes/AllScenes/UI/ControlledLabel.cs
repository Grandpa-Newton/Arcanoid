﻿using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.AllScenes.UI
{
    public class ControlledLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;

        private string _text, _data;

        public bool Localized;

        public void SetText(string text)
        {
            _text = text;
            UpdateLabel();
        }

        public void SetData(string data)
        {
            _data = data;
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            label.text = _text + _data;
        }

        public Color color
        {
            get => label.color;
            set => label.color = value;
        }

        public bool enableVertexGradient
        {
            get => label.enableVertexGradient;
            set => label.enableVertexGradient = value;
        }

        public float fontSize
        {
            get => label.fontSize;
            set => label.fontSize = value;
        }
    }
}