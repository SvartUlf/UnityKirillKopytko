using System;
using Unity.Mathematics;
using UnityEngine;
namespace Game.Inventory
{
    public enum ItemType
    {
        Food,
        Equipment,
        Default
    }

    public enum Attributes
    {
        Agility,
        Stamina,
        Strength,
        Intellegence,
        Health,
        Default
    }
    public abstract class ItemObject : ScriptableObject
    {
        public int Id;
        public string Name;
        public Sprite UiDisplay;
        [TextArea(15, 20)]
        public string Description;
        public ItemType Type;
        public ItemAttributes[] ItemAttributesConfig;

        public Item CreateItem()
        {
            Item newItem = new Item(this);
            return newItem;
        }
    }

    [Serializable]
    public class Item
    {
        public int Id;
        public string Name;
        public ItemAttributes[] ItemAttributesConfig;

        public Item(ItemObject item)
        {
            Name = item.Name;
            Id = item.Id;
            ItemAttributesConfig = new ItemAttributes[item.ItemAttributesConfig.Length];
            for (int i = 0; i < ItemAttributesConfig.Length; i++)
            {
                ItemAttributesConfig[i] = new ItemAttributes(item.ItemAttributesConfig[i].min, item.ItemAttributesConfig[i].max)
                {
                    Attributes = item.ItemAttributesConfig[i].Attributes
                };
            }
        }
    }

    [Serializable]
    public class ItemAttributes
    {
        public Attributes Attributes;
        public int value;
        public int min;
        public int max;

        public ItemAttributes(int _min, int _max)
        {
            min = _min;
            max = _max;
        }
        public void GenerateValues ()
        {
            value = UnityEngine.Random.Range(min, max + 1);
        }
    }
}