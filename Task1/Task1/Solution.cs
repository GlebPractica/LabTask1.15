using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Diagnostics;

namespace Task1
{
    public class Solution
    {
        private string _filePath;

        public Solution(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<Dictionary<DateTime, string>> LoadFromFile()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    return JsonConvert.DeserializeObject<Dictionary<DateTime, string>>(json);
                }
                else
                {
                    File.Create(_filePath).Dispose();
                    return new Dictionary<DateTime, string>();
                }
            }
            catch (Exception ex)
            {
                Debug.Print("!!!Error!!!\n" + ex.Message);
                return null;
            }
        }

        public void SaveNotesFiles(Dictionary<DateTime, string> notes)
        {
            string json = JsonConvert.SerializeObject(notes);
            File.WriteAllText(_filePath, json);
        }
    }
}
