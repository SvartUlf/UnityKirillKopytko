using UnityEditor;
using UnityEngine;
namespace Game.Inventory
{
    public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
    {
        public ItemObject Item;

        public void OnAfterDeserialize()
        {
            GetComponentInChildren<SpriteRenderer>().sprite = Item.UiDisplay;
            EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
        }

        public void OnBeforeSerialize()
        {
            throw new System.NotImplementedException();
        }
    }
}