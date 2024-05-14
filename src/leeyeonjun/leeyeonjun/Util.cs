using Newtonsoft.Json.Linq;
using System.Reflection;

namespace leeyeonjun
{
    public class Util
    {
        public static string version = "24-04-08 001";
        public static string connectionString = string.Empty;

        private static JObject GetJsonObject()
        {
            string fileName = $"private{Path.DirectorySeparatorChar}leeyeonjun_data.json";
            string rootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.Parent.FullName;
            string filePath = Path.Combine(rootPath, fileName);
            JObject jObject = new();

            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                jObject = JObject.Parse(jsonString);
            }
            else
            {
            }

            return jObject;
        }

        public static string GetJsonData(string key1, string? key2 = null, string? key3 = null)
        {
            JObject jObject1 = GetJsonObject();
            string returnString = string.Empty;

            foreach (KeyValuePair<string, JToken?> jKeyValue1 in jObject1)
            {
                if (jKeyValue1.Key.Equals(key1))
                {
                    if (jKeyValue1.Value is JValue jValue1)
                    {
                        returnString = jValue1.ToString();
                        break;
                    }
                    else if (jKeyValue1.Value is JObject jObject2)
                    {
                        foreach (KeyValuePair<string, JToken?> jKeyValue2 in jObject2)
                        {
                            if (jKeyValue2.Key.Equals(key2))
                            {
                                if (jKeyValue2.Value is JValue jValue2)
                                {
                                    returnString = jValue2.ToString();
                                    break;
                                }
                                else if (jKeyValue2.Value is JObject jObject3)
                                {
                                    foreach (KeyValuePair<string, JToken?> jKeyValue3 in jObject3)
                                    {
                                        if (jKeyValue3.Key.Equals(key3))
                                        {
                                            returnString = jKeyValue3.Value!.ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return returnString;
        }
    }
}
