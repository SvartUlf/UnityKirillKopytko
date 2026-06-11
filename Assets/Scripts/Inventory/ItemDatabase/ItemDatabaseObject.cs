using System.Collections.Generic;
using UnityEngine;
namespace Game.Inventory {
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/ItemDatabase")]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] Items;
        public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

        public void OnAfterDeserialize()
        {
            for(int i = 0; i < Items.Length; i++)
            {
                Items[i].Id = i;
                GetItem.Add(i, Items[i]);
            }
        }

        public void OnBeforeSerialize()
        {
            GetItem = new Dictionary<int, ItemObject>();
        }
    }
}
