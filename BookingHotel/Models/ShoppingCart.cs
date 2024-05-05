﻿namespace BookingHotel.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }
        public void RemoveItem(int roomId)
        {
            Items.RemoveAll(i => i.Id == roomId);
        }
        public void UpdateQuantity(int roomId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Id == roomId);
            if (item != null)
            {
                item.Quantity = quantity;
            }
        }
    }
}
