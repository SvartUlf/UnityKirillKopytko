using UnityEngine;
namespace Game.Inventory
{
    [CreateAssetMenu(fileName = "FoodItem", menuName = "Inventory/Item/FoodItem")]
    public class FoodItem : ItemObject
    {
        public void Awake()
        {
            Type = ItemType.Food;
        }

    }
}
