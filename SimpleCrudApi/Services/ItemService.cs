using SimpleCrudApi.Models;

namespace SimpleCrudApi.Services
{
    public class ItemService
    {
        private readonly List<Item> _items = new List<Item>();
        private int _nextId = 1;

        public List<Item> GetAll() => _items;

        public Item? GetById(int id) => _items.FirstOrDefault(i => i.Id == id);

        public Item Add(Item newItem)
        {
            newItem.Id = _nextId++;
            _items.Add(newItem);
            return newItem;
        }

        public bool Update(int id, Item updatedItem)
        {
            var existingItem = GetById(id);
            if (existingItem == null) return false;

            existingItem.Name = updatedItem.Name;
            existingItem.Price = updatedItem.Price;
            return true;
        }

        public bool Delete(int id)
        {
            var item = GetById(id);
            if (item == null) return false;

            _items.Remove(item);
            return true;
        }
    }
}
