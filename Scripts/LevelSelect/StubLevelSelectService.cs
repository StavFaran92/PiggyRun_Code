using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Shop.Scripts
{
    class StubLevelSelectService : ILevelSelectService
    {
        //-- Singleton
        public static ILevelSelectService mInstance = new StubLevelSelectService();

        //internal static void TryPurchaseItem(Item item)
        //{
        //    if (TrySpendGoldAmount(item.cost))
        //    {
        //        // Can afford cost
        //        BoughtItem(item);
        //    }
        //    else
        //    {
        //        Debug.Log("Cannot afford item!");
        //    }
        //}

        //private static void BoughtItem(Item item)
        //{
        //    Debug.Log("Bought item: " + item.name);

        //    UI_Inventory.GetUIInventory().GetService().AddItemToInventory(item, item.category);
        //}

        //public static bool TrySpendGoldAmount(int goldAmount)
        //{
        //    Debug.Log("Player spent " + goldAmount + " coins");
        //    return true;
        //}

        //internal static void TryEquipItem(Item item)
        //{
        //    UI_Inventory.GetUIInventory().GetService().TryEquipItem(item);
        //}
    }
}
