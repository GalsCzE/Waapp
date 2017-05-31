using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Waapp
{
    public class database
    {
       private SQLiteAsyncConnection databze;
        public database(string dbPath)
        {
            databze = new SQLiteAsyncConnection(dbPath);
            databze.CreateTableAsync<Weather>().Wait();
        }
        public Task<List<Weather>> GetItemsAsync()
        {
            return databze.Table<Weather>().ToListAsync();
        }

        public Task<List<Weather>> GetItemsNotDoneAsync(string x, string y)
        {
            return databze.QueryAsync<Weather>("SELECT * FROM [Weather] WHERE [Sirka] = " + x + " AND [Delka] = " + y);
        }

        public Task<List<Weather>> GetCategories(string s, string d)
        {
            return databze.Table<Weather>().Where(i => i.Sirka == s && i.Delka == d).ToListAsync();
        }

        public Task<int> SaveItemAsync(Weather item)
        {
            if (item.ID != 0)
            {
                return databze.UpdateAsync(item);
            }
            else
            {
                return databze.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Weather item)
        {
            return databze.DeleteAsync(item);
        }
    }
}
