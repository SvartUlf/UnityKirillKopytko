using System;
using UnityEngine;
namespace Game.Inventory {
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public ItemDatabaseObject Database;
        public Inventory Container;

        public void AddItem(Item item, int amount)
        {
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if (Container.Items[i].Id == item.Id)
                {
                    Container.Items[i].AddAmount(amount);
                    return;
                }
                SetEmptySlot(item, amount);
            }
        }

        public void RemoveItem(Item item)
        {
            for(int i = 0;i < Container.Items.Length; i++)
            {
                if (Container.Items[i].Item == item)
                {
                    Container.Items[i].UpdateSlot(-1, null, 0);
                }
            }
        }

        internal void MoveItem(InventorySlot item1, InventorySlot item2)
        {
            if (item1.Id != item2.Id)
            {
                InventorySlot tempItem = new InventorySlot(item2.Id, item2.Item, item2.Amount);
                item2.UpdateSlot(item1.Id, item1.Item, item1.Amount);
                item1.UpdateSlot(tempItem.Id, tempItem.Item, tempItem.Amount);
            }
            else
            {
                item2.AddAmount(item1.Amount);
                item1.UpdateSlot(-1, null, 0);
            }
        }
        internal void SplitItem(InventorySlot item1, InventorySlot item2)
        {
            if ((item1.Id == item2.Id || item2.Id < 0) && item1.Amount > 1)
            {
                InventorySlot tempItem = new InventorySlot(item2.Id, item2.Item, item2.Amount);
                int amount1 = item1.Amount;
                int amount2 = item2.Amount;
                int splitAmount = amount1 / 2;
                amount1 -= splitAmount;
                amount2 += splitAmount;
                item1.Amount = amount1;
                tempItem.Amount = amount2;
                tempItem.Id = item1.Id;
                tempItem.Item = item1.Item;
                item2.UpdateSlot(item1.Id, item1.Item, item1.Amount);
                item1.UpdateSlot(tempItem.Id, tempItem.Item, tempItem.Amount);
            }
        }

        private void SetEmptySlot(Item item, int amount)
        {
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if (Container.Items[i].Id < 0)
                {
                    Container.Items[i].UpdateSlot(item.Id, item, amount);
                }
            }
        }
    }

    [Serializable]
    public class Inventory
    {
        public InventorySlot[] Items = new InventorySlot[10];
    }

    [Serializable]
    public class InventorySlot
    {
        public int Id = -1;
        public Item Item;
        public int Amount;

        public InventorySlot()
        {
            Id = -1;
            Item = null;
            Amount = 0;
        }

        public InventorySlot(int id, Item item, int amount)
        {
            Id = id;
            Item = item;
            Amount = amount;
        }

        public void AddAmount(int value)
        {
            Amount += value;
        }

        public void UpdateSlot(int id, Item item, int amount)
        {
            Id = id;
            Item = item;
            Amount = amount;
        }
    }
}
