using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace FreshBox.Models
{
    public class FreshBoxDatabase
    {
        private readonly SQLiteAsyncConnection freshBoxDatabase;

        public FreshBoxDatabase(string dbPath)
        {
            freshBoxDatabase = new SQLiteAsyncConnection(dbPath);
            freshBoxDatabase.CreateTableAsync<FreshBoxes>().Wait();
        }

        public Task<List<FreshBoxes>> GetFreshBoxesAsync()
        {
            return freshBoxDatabase.Table<FreshBoxes>().ToListAsync();
        }

        public Task<int> SaveFreshBoxAsync(FreshBoxes freshbox)
        {
            return freshBoxDatabase.InsertAsync(freshbox);
        }

        public Task<int> UpdateFreshBoxAsync(FreshBoxes freshbox)
        {
            return freshBoxDatabase.UpdateAsync(freshbox);
        }
        public Task<int> DeleteFreshBoxAsync(FreshBoxes freshbox)
        {
            _ = App.ItemDatabase.DeleteItemsByBox(freshbox.Id);
            return freshBoxDatabase.DeleteAsync(freshbox);
        }
    }
}