using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using People.Models;
using SQLite;
using Xamarin.Essentials;

namespace People
{
    public class PersonRepository
    {
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        public PersonRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Person>();
        }

        public async Task AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                // TODO: insert a new person into the Person table
                string password = await SecureStorage.GetAsync(App.PASSWORD_KEY);
                string encryptedName = CryptoHelper.Encrypt(name, password);
                Person person = new Person() { Name = encryptedName };
                    result = await conn.InsertAsync(person);

                StatusMessage = string.Format("{0} record(s) added [Name: {1}])", result, name);
                result++;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public async Task<List<Person>> GetAllPeople()
        {
            // TODO: return a list of people saved to the Person table in the database
            try
            {
                string password = await SecureStorage.GetAsync(App.PASSWORD_KEY);
                var people = await conn.Table<Person>().ToListAsync();
                List<Person> ret = people.ConvertAll((person) =>
                {
                    Person newPerson = new Person()
                    {
                        Name = CryptoHelper.Decrypt(person.Name, password)
                    };

                    return newPerson;
                });

                return ret;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();

        }
    }
}