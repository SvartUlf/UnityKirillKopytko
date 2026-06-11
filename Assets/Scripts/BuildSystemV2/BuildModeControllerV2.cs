using Game.BuildSystem;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.BuildSystemV2
{
    public class BuildModeControllerV2 : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private BuildInputHandler _inputHandler;
        [SerializeField] private BuildPreviewSystem _previewSystem;
        [SerializeField] private BuildPlacementSystem _placementSystem;
        [SerializeField] private BuildPaletteUI _paletteUI;

        [Header("Settings")]
        [SerializeField] private KeyCode _toggleBuildModeKey = KeyCode.B;

        private bool _isBuildMode = false;

        private BuildTileData _activeTile;


        private void Update()
        {
            HandleBuildModeToggle();
            HandlePlacement();
        }

        private void HandleBuildModeToggle()
        {
            if (!Input.GetKeyDown(_toggleBuildModeKey))
            {
                return;
            }

            _isBuildMode = !_isBuildMode;
            _paletteUI.SetVisible(_isBuildMode);
            Debug.Log($"Build Mode:{_isBuildMode}");

            if (_isBuildMode)
            {
                _previewSystem.EnablePreview();
                return;
            }
            _previewSystem.DisablePreview();
            _activeTile = null;
        }

        private void HandlePlacement()
        {
            if (!_activeTile)
            {
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                _previewSystem.HidePreview();
                return;
            }
            else
            {
                _previewSystem.EnablePreview();
            }
            Vector3Int cellPosition = _inputHandler.GetMouseCellPosition();

            if (Input.GetMouseButton(0) && _isBuildMode)
            {
                _placementSystem.PlaceTile(cellPosition, _activeTile);
            }

            if (Input.GetMouseButtonDown(1) && _isBuildMode)
            {
                _placementSystem.RemoveTile(cellPosition);
            }
            _previewSystem.UpdatePreview(cellPosition, _placementSystem.CanPlace(cellPosition));
        }

        internal void SetActiveTile(BuildTileData tileData)
        {
            _activeTile = tileData;

            _previewSystem.SetPreviewSprite(tileData.TileSprite);
        }
    }
}