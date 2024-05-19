namespace BookingHotel.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id == item.Id);

            Items.Add(item);
        }
        public void RemoveItem(int roomId)
        {
            Items.RemoveAll(i => i.Id == roomId);
        }
    }
}
