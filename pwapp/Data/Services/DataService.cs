using PWApp.Data.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWApp.Data.Services
{
    public class DataService
    {
        private SQLiteConnection _connection;
        public DataService()
        {
            Init();
        }
        private void Init()
        {
            try
            {
                if (_connection != null)
                {
                    return;
                }
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CustomerData.db");
                _connection = new SQLiteConnection(databasePath);
                _connection.CreateTable<CustomerModel>();
            }
            catch
            {
                Init();
            }

        }

        public int AddCard(CustomerModel newCardData)
        {
            var result = _connection.Table<CustomerModel>().ToList();
            return _connection.Insert(newCardData);
        }

        public int RemoveCardById(Guid cardGuid) => _connection.Delete(cardGuid);

        public List<CustomerModel> GetAllMyCards(bool isMine, string currentUserId) => _connection.Table<CustomerModel>().ToList().Where(x => x.CreateBy.Equals(currentUserId) && x.IsMyData == isMine).ToList();

        public int ChangeCard(CustomerModel newCardData) => _connection.Update(newCardData);

        public int RemoveCard(CustomerModel cardData) => _connection.Delete(cardData);

        public CustomerModel FindCardById(string id) => _connection.Table<CustomerModel>().FirstOrDefault(item => item.Id == id);
    }
}
