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
        private const string FileName = "credentials.json";

        
        public async Task<List<Credentials>> ReadCredentialsAsync()
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(FileName);
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
                // Handle exceptions (e.g., file not found)
                return [];
            }
        }

        public void WriteCredentialsAsync(List<Credentials> credentialsList)
        {
            var file =  ApplicationData.Current.LocalFolder.CreateFileAsync(FileName,
                CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();
             FileIO.WriteTextAsync(file, JsonConvert.SerializeObject(credentialsList)).GetAwaiter().GetResult();
        }
    }
}