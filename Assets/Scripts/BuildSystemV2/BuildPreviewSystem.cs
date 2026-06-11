using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildPreviewSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tilemap _previewTilemap;

    [Header("Color")]
    [SerializeField] private Color _canPlaceColor = new Color(0, 200, 0, 100);
    [SerializeField] private Color _canNotPlaceColor = new Color(200, 0, 0, 100);

    private Tile _previewTile;
    private Vector3Int _lastCellPosition;

    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        _previewTile = ScriptableObject.CreateInstance<Tile>();
        _previewTilemap.color = Color.white;
        _previewTilemap.GetComponent<TilemapRenderer>().enabled = true;
    }

    internal void EnablePreview()
    {
        _previewTilemap.gameObject.SetActive(true);
    }
    internal void DisablePreview()
    {
        HidePreview();
        _previewTile = ScriptableObject.CreateInstance<Tile>();
    }
    internal void HidePreview()
    {
        ClearPreview();
        _previewTilemap.gameObject.SetActive(false);
    }

    private void ClearPreview()
    {
        _previewTilemap.SetTile(_lastCellPosition, null);
    }

    internal void UpdatePreview(Vector3Int cellPosition, bool canPlace)
    {
        if(_lastCellPosition == cellPosition)
        {
            return;
        }

        ClearPreview();
        _previewTilemap.SetTile(cellPosition, _previewTile);
        _previewTilemap.SetTileFlags(cellPosition, TileFlags.None);
        _previewTilemap.SetColor(cellPosition, canPlace ? _canPlaceColor : _canNotPlaceColor);
        _lastCellPosition = cellPosition;
    }

    internal void SetPreviewSprite(Sprite icon)
    {
        _previewTile.sprite = icon;
    }
}
