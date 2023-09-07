using System.Text.Json;

namespace leeyeonjun
{
    public class Utiles
    {
        public static JsonModel GetJsonModel()
        {
            string fileName = $"yeonjunsCode{Path.DirectorySeparatorChar}private{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}leeyeonjun85.json";
            string filePath = Path.Combine(Path.GetPathRoot(Directory.GetCurrentDirectory())!, fileName);
            string jsonString = File.ReadAllText(filePath);
            JsonModel jsonModel = JsonSerializer.Deserialize<JsonModel>(jsonString)!;
            return jsonModel;
        }
    }
}
