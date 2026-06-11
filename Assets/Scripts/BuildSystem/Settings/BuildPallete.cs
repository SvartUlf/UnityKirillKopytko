using UnityEngine;

namespace Game.BuildSystem
{
	[CreateAssetMenu(fileName = "Palette", menuName = "Build Menu/Palette")]
	public class BuildPallete: ScriptableObject
    {
        [field: SerializeField] public string PalleteName { get; private set; }
        [field: SerializeField] public BuildTileData[] Tiles { get; private set; }
    }
}