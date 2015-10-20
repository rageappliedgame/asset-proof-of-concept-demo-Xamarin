using Xamarin.Forms;

[assembly: Dependency(typeof(DataStorage.WinPhone.DataStorage_WinPhone))]

namespace DataStorage.WinPhone
{
    public class DataStorage_WinPhone //: IDataStorage, IDataArchive
    {
        public DataStorage_WinPhone()
        {
            //asset_proof_of_concept_Xamarin.A
        }
        //        public async Task SaveTextAsync(string filename, string text)
        //         { 
        // 			StorageFolder localFolder = ApplicationData.Current.LocalFolder; 
        // 			IStorageFile file = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting); 
        // 			using (StreamWriter streamWriter = new StreamWriter(file.Path)) 
        // 			{ 
        // 				await streamWriter.WriteAsync(text); 
        // 			} 
        //         } 
        // 

        // 		public async Task<string> LoadTextAsync(string filename)
        // 		{ 
        // 			StorageFolder localFolder = ApplicationData.Current.LocalFolder; 
        // 			IStorageFile file = await localFolder.GetFileAsync(filename); 
        // 			string text; 
        // 

        // 			using (StreamReader streamReader = new StreamReader(file.Path)) 
        // 			{ 
        // 				text = await streamReader.ReadToEndAsync(); 
        // 			} 
        // 			return text; 
        // 		} 

    }
}
