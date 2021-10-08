using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace FreshBox.Models
{
    public class ItemDatabase
    {
        private readonly SQLiteAsyncConnection itemDatabase;

        public ItemDatabase(string dbPath)
        {
            itemDatabase = new SQLiteAsyncConnection(dbPath);
            itemDatabase.CreateTableAsync<Item>().Wait();
        }

        public Task<List<Item>> GetItemsAsync()
        {
            return itemDatabase.Table<Item>().ToListAsync();
        }

        public Task<List<Item>> GetItemsByBox(string freshBoxId)
        {
            AsyncTableQuery<Item> itemList = itemDatabase.Table<Item>().Where(x => x.FridgeId.Equals(freshBoxId));
            return itemList.ToListAsync();
        }
        public Task<List<Item>> GetItemsByDate(DateTime date)
        {
            AsyncTableQuery<Item> itemList = itemDatabase.Table<Item>().Where(x => x.ExpiryDate == date);
            return itemList.ToListAsync();
        }

        public Task<List<Item>> GetItemsSortedByDate()
        {
            AsyncTableQuery<Item> itemList = itemDatabase.Table<Item>().OrderBy(x => x.ExpiryDate);
            return itemList.ToListAsync();
        }

        public Task<int> SaveItemAsync(Item item)
        {
            return itemDatabase.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(Item item)
        {
            return itemDatabase.UpdateAsync(item);
        }

        public Task<int> DeleteItemAsync(Item item)
        {
            return itemDatabase.DeleteAsync(item);
        }

        public async Task<int> DeleteItemsByBox(string freshBoxId)
        {
            var query = itemDatabase.Table<Item>().Where(x => x.FridgeId.Equals(freshBoxId));
            List<Item> list = await query.ToListAsync();
            int rowsDeleted = 0;
            foreach(Item item in list)
            {
                await DeleteItemAsync(item);
                rowsDeleted++;
            }
            return rowsDeleted;
        }

    }
}