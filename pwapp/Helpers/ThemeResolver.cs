#if IOS
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
#endif
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWApp.Helpers
{
    public static class ThemeResolver
    {
        static SQLiteConnection _connection;
        static void Init()
        {
            try
            {
                if (_connection != null)
                {
                    return;
                }
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CustomerData.db");
                _connection = new SQLiteConnection(databasePath);
                _connection.CreateTable<bool>();
                _connection.Insert(true);
            }
            catch
            {
                Init();
            }

        }

        public static bool ResolveTheme()
        {
            Init();
            return _connection.Table<bool>().FirstOrDefault();
        }
    }
}
