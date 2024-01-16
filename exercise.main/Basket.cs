﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace exercise.main
{
    public class Basket
    {
        private List<Item> _inventory = new List<Item>();
        private int _capacity = 10;
        private List<string> _existingItems = new List<string>() { "BGLO", "BGLP", "BGLE", "BGLS", "COFB", "COFW", "COFC", "COFL","FILB", "FILE", "FILC" , "FILX", "FILS" ,"FILH" };
         
        public List<Item> Inventory { get { return _inventory; } }

        public Basket(int capacity = 10)
        {
            _capacity = capacity;

        }

        public bool AddItem(string SKU)
        {
            if (!_existingItems.Contains(SKU) || _inventory.Count() >= _capacity) return false;
            Item newItem;
            switch (SKU.Substring(0, 3).ToUpper())
            {
                case "BGL" :
                    newItem = new Bagel(SKU);
                    break;
                case "COF" :
                    newItem = new Coffee(SKU);
                    break;
                case "FIL":
                    return false;
                    break;
                default:
                    return false;
            }
            _inventory.Add(newItem);
            return true;
        }

        public bool RemoveItem(int id)
        {
            if (!_inventory.Any(x => x.ID == id)) return false;
            _inventory.RemoveAll(x => x.ID == id);
            return true;
        }

        public float TotalCost()
        {
            float result = 0;

            foreach (string item in _existingItems)
            {
                if (item.Substring(0, 3).ToUpper() != "BGL") continue;
                int num = _inventory.Count(x => x.SKU == item);
                while (num >= 12)
                {
                    Console.WriteLine(item + num);
                    result += 3.99f;
                    num -= 12;
                }
                if (num >= 6 && item != "BGLP")
                {    
                    result += 2.49f;
                    num -= 6;
                }
                for (int i = 0; i < num; i++)
                {
                    result += GetPrice(item);
                }

            }

            return result;
        }

        /*            
         *            
         *            IEnumerable<char> values = hand.SelectMany(x => x.Value).Distinct();
            if (values.Count() > 2) return 0;
            foreach (char card in values)
            {
                if (hand.Count(x => x.Value == card.ToString()) == 3) return 600 + core.GetValueOfCard(card.ToString());
            }*/



        public bool AddFilling(int id, string SKU)
        {
            if (!_existingItems.Contains(SKU) || !_inventory.Any(x => x.ID == id && x.Name == "Bagel")) return false;
            _inventory.Find(x => x.ID == id)!.AddFilling(SKU);
            return true;

        }

        public int ChangeCapacity(int newCapacity)
        {
            if (newCapacity < 0) return _capacity;
            _capacity = newCapacity;
            return _capacity;
        }
        public float GetPrice(string SKU)
        {
            if (!_existingItems.Contains(SKU)) return 0;
            Item newItem;
            switch (SKU.Substring(0, 3).ToUpper())
            {
                case "BGL":
                    newItem = new Bagel(SKU);
                    break;
                case "COF":
                    newItem = new Coffee(SKU);
                    break;
                case "FIL":
                    newItem = new Filling(SKU);
                    break;
                default:
                    return 0;
            }
            return newItem.TotalPrice();
        }
    }
}
