using Game.BuildSystem;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

namespace Game.BuildSystemV2
{
    public class BuildPaletteUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private BuildPallete _palette;
        [SerializeField] private BuildTileButton _prefabButton;
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _rootPanel;


        private BuildModeControllerV2 _controller;

        private void Start()
        {
            GenerateButton();
            _rootPanel.SetActive(false);
        }

        internal void SetVisible(bool value)
        {
            _rootPanel.SetActive(value);
        }

        private void GenerateButton()
        {
            foreach (BuildTileData tileData in _palette.Tiles)
            {
                BuildTileButton button = Instantiate(_prefabButton, _container);
                button.Initialize(tileData, _controller);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(_container.GetComponent<RectTransform>());
        }
        private void Awake()
        {
            _controller = GetComponent<BuildModeControllerV2>();
        }
    }
}