using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace Utiles
{
    public class MyUtiles
    {
        public static JObject GetJsonModel()
        {
            string fileName = $"yeonjunsCode{Path.DirectorySeparatorChar}private{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}leeyeonjun85.json";
            string filePath = Path.Combine(Path.GetPathRoot(Directory.GetCurrentDirectory())!, fileName);
            string jsonString = File.ReadAllText(filePath);
            JObject jobject = JObject.Parse(jsonString);

            //JsonModel jsonModel = JsonSerializer.Deserialize<JsonModel>(jsonString)!;
            return jobject;
        }
    }
}
