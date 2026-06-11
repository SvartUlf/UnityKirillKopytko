using Game.BuildSystem;
using Game.BuildSystemV2;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildTileButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    private BuildTileData _buildTileData;
    private BuildModeControllerV2 _controller;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void Initialize(BuildTileData buildTileData, BuildModeControllerV2 controller)
    {
        _buildTileData = buildTileData;
        _controller = controller;

        _icon.sprite = buildTileData.TileSprite;
        _button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        _controller.SetActiveTile(_buildTileData);
    }
}
