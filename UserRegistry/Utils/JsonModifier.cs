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

        
        public async Task<List<Credentials>> ReadCredentialsAsync(string fileName)
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                string json = await FileIO.ReadTextAsync(file);

                var deserializedJson = JsonConvert.DeserializeObject<List<Credentials>>(json);
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

        public void WriteCredentialsAsync<T>(List<T> credentialsList, string fileName)
        {
            var file =  ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();
             FileIO.WriteTextAsync(file, JsonConvert.SerializeObject(credentialsList)).GetAwaiter().GetResult();
        }
    }
}