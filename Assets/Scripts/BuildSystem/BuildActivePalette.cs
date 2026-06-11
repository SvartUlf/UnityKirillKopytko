using Game.BuildSystem;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Game.BuildSystem
{
    public class BuildActivePalette : MonoBehaviour
    {
        [field: SerializeField] public BuildPallete PaletteData { get; private set; }
        [field: SerializeField] public Tilemap TargetTileMap { get; private set; }
    }
}