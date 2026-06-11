using UnityEngine;
using UnityEngine.Tilemaps;
namespace Game.BuildSystem
{

    public class BuildModeController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera _camera;
        [SerializeField] private Tilemap _previewTilemap;

        [Header("Settings")]
        [SerializeField] private KeyCode _toggleBuildModeKey = KeyCode.B;
        [SerializeField] private KeyCode _toggleTileKey = KeyCode.T;

        [Header("Preview Color")]
        [SerializeField] private Color _canPlaceColor = new Color(0, 200,0,100);
        [SerializeField] private Color _canNotPlaceColor = new Color(200, 0, 0, 100);

        private bool _isBuildMode;
        private BuildActivePalette _activePalette;
        private BuildTileData _activeTile;
        private int _currentTile = 0;

        private Vector3Int _lastPreviewCellPosition;
        private Tile _previewTile;

        //[SerializeField] private InputComponent inputComponent;

        private void Start()
        {
            _activePalette = GetComponent<BuildActivePalette>();
            Initialize();
        }

        private void Initialize()
        {
            SetActivePanel(_activePalette);

            _previewTile = ScriptableObject.CreateInstance<Tile>();
            _previewTilemap.color = Color.white;
            _previewTilemap.GetComponent<TilemapRenderer>().enabled = true;
            SetActiveTile(_activePalette.PaletteData.Tiles[0]);
        }
        private void Update()
        {
            HandleBuildModeToggle();
            HandleTileToggle();
            HandleBuildInput();

            if (!_isBuildMode)
            {
                ClearPreview();
            }
            else
            {
                HandlePreview();
            }
        }

        private void HandlePreview()
        {
            if(!_activePalette || !_activeTile || !_previewTilemap)
            {
                return;
            }
            Tilemap tilemap = _activePalette.TargetTileMap;
            Vector3Int cell = GetMouseCellPosition(tilemap);
            _previewTilemap.SetTile(_lastPreviewCellPosition, null);
            _previewTilemap.SetTile(cell, _previewTile);
            _previewTilemap.SetTileFlags(cell, TileFlags.None);

            bool canPlace = !tilemap.HasTile(cell);
            _previewTilemap.SetColor(cell, canPlace ? _canPlaceColor : _canNotPlaceColor);
            _lastPreviewCellPosition = cell;
        }

        private void ClearPreview()
        {
            if (_previewTilemap)
            {
                _previewTilemap.SetTile(_lastPreviewCellPosition, null);
            }
        }
        private void SetActivePanel(BuildActivePalette palette)
        {
            _activePalette = palette;
            BuildPallete paletteData = _activePalette.PaletteData;
            if(paletteData.Tiles.Length <= 0)
            {
                return;
            }
            _activeTile = paletteData.Tiles[0];
            _currentTile = 0;
        }

        private void SetActiveTile(BuildTileData tileData)
        {
            _activeTile = tileData;

            if (_previewTile && _activeTile)
            {
                _previewTile.sprite = _activeTile.TileSprite;
            }
        }
        private void HandleBuildModeToggle()
        {
            if (Input.GetKeyDown(_toggleBuildModeKey))
            {
                _isBuildMode = !_isBuildMode;
                Debug.Log($"Is Build Mode Active: {_isBuildMode}");
            }
        }
        private void HandleTileToggle()
        {
            if (Input.GetKeyDown(_toggleTileKey) && _isBuildMode)
            {
                BuildTileData[] tiles = _activePalette.PaletteData.Tiles;
                _currentTile++;
                if (_currentTile == tiles.Length)
                {
                    _currentTile = 0;
                }
                SetActiveTile(tiles[_currentTile]);
                Debug.Log($"Is Build Mode Active: {_isBuildMode}");
            }
        }

        private void HandleBuildInput()
        {
            if(!_activePalette || !_activeTile || !_isBuildMode)
            {
                return;
            }

            if (Input.GetMouseButton(0))
            {
                PlaceTile();
            }
            if (Input.GetMouseButtonDown(1))
            {
                RemoveTile();
            }
        }

        private void PlaceTile()
        {
            Tilemap tilemap = _activePalette.TargetTileMap;
            Vector3Int cellPosition = GetMouseCellPosition(tilemap);
            if (tilemap.HasTile(cellPosition))
            {
                return;
            }
            tilemap.SetTile(cellPosition, _activeTile.Tile);
        }

        private void RemoveTile()
        {
            Tilemap tilemap = _activePalette.TargetTileMap;
            Vector3Int cellPosition = GetMouseCellPosition(tilemap);
            if (!tilemap.HasTile(cellPosition))
            {
                return;
            }
            tilemap.SetTile(cellPosition, null);
        }

        private Vector3Int GetMouseCellPosition(Tilemap tilemap)
        {
            Vector3 worldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            return tilemap.WorldToCell(worldPos);
        }
    }
}