using Game.BuildSystem;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Game.BuildSystemV2 {
    public class BuildPlacementSystem : MonoBehaviour
    {
        [SerializeField] private Tilemap _targetTilemap;
        internal bool CanPlace(Vector3Int cellPosition)
        {
            return !_targetTilemap.HasTile(cellPosition);
        }

        internal void PlaceTile(Vector3Int cellPosition, BuildTileData tile)
        {
            if (!CanPlace(cellPosition))
            {
                return;
            }
            _targetTilemap.SetTile(cellPosition, tile.Tile);
        }

        internal void RemoveTile(Vector3Int cellPosition)
        {
            if (!_targetTilemap.HasTile(cellPosition))
            {
                return;
            }
            _targetTilemap.SetTile(cellPosition, null);
        }
    }
}
