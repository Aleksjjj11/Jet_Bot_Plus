using System.Collections.Generic;
using System.IO;
using Jet_Bot.Core.Accounts;
using Newtonsoft.Json;

namespace Jet_Bot.Core
{
    public class DataStorage_Accounts
    {
        public static void SaveUserAccounts(IEnumerable<UserAccount> accounts, string filePath)
        {
            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static IEnumerable<UserAccount> LoadUserAccounts(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<UserAccount>>(json);
        }
        
        public static bool SaveExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}