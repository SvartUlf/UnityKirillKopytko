using UnityEngine;
using UnityEngine.Tilemaps;
namespace Game.BuildSystem
{
    [CreateAssetMenu(fileName = "TileData", menuName = "Build Menu/TileData")]
    public class BuildTileData : ScriptableObject
    {
        [field: SerializeField] public string TileName {  get; private set; }
        [field: SerializeField] public TileBase Tile { get; private set; }
        [field: SerializeField] public Sprite TileSprite { get; private set; }
    }
}