using System;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Game.BuildSystemV2
{
    public class BuildInputHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Tilemap _tilemap;
        internal Vector3Int GetMouseCellPosition()
        {
            Vector3 worldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            return _tilemap.WorldToCell(worldPos);
        }
    }
}
