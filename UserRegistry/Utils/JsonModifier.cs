using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistry.Models;
using Windows.Storage;

namespace UserRegistry.Utils
{
    internal class JsonModifier
    {

        
        public List<T> ReadJsonFile<T>(string fileName)
        {
            try
            {
                StorageFile file = ApplicationData.Current.LocalFolder.GetFileAsync(fileName).GetAwaiter().GetResult();
                string json = FileIO.ReadTextAsync(file).GetAwaiter().GetResult();

                var deserializedJson = JsonConvert.DeserializeObject<List<T>>(json);
                if (deserializedJson != null)
                {
                    return deserializedJson;
                }

                return [];
            }
            catch
            {
                return [];
            }
        }

        public void WriteJsonFile<T>(List<T> credentialsList, string fileName)
        {
            var file =  ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();
             FileIO.WriteTextAsync(file, JsonConvert.SerializeObject(credentialsList)).GetAwaiter().GetResult();
        }
    }
}