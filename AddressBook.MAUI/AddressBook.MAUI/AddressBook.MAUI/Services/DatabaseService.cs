using System;
using AddressBook.MAUI.Models;
using SQLite;

namespace AddressBook.MAUI.Services
{
	public class DatabaseService
	{
        public DatabaseService()
        {
        }

        public const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.SharedCache;

        static SQLiteConnection _instance;
        public static SQLiteConnection Instance
        {
            get
            {
                if (_instance == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MAUISQLite.db3");
                    _instance = new SQLiteConnection(dbPath);
                    _instance.CreateTable<LoginModel>();
                    _instance.CreateTable<UserData>();
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        static object locker = new object();


        public static int SaveLoginDetail(LoginModel item)
        {
            lock (locker)
            {
                return DatabaseService.Instance.Insert(item);
            }
        }

        public static List<LoginModel> GetAll()
        {
            lock (locker)
            {
                return DatabaseService.Instance.Table<LoginModel>().ToList();
            }
        }

        public static int SaveItem(UserData item)
        {
            lock (locker)
            {
                if (item.ID != 0)
                {
                    DatabaseService.Instance.Update(item);
                    return item.ID;
                }
                else
                {
                    return DatabaseService.Instance.Insert(item);
                }
            }
        }

        public static List<UserData> GetAllItem(string Id)
        {
            lock (locker)
            {
                return DatabaseService.Instance.Table<UserData>().Where(x => x.LoginID == Id).ToList();
            }
        }

        public static int DeleteItem(int id)
        {
            lock (locker)
            {
                return DatabaseService.Instance.Delete<UserData>(id);
            }
        }
    }
}

