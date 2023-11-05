using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace DataBaseTools.Services
{
    public class JsonData
    {
        private static JObject GetEdcoreWorksJsonObject()
        {
            string fileName = $"yeonjunsCode{Path.DirectorySeparatorChar}EdcoreWorks{Path.DirectorySeparatorChar}EDCORE_Data.json";
            string filePath = Path.Combine(Path.GetPathRoot(Directory.GetCurrentDirectory())!, fileName);
            JObject jObject = new();

            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                jObject = JObject.Parse(jsonString);
            }
            else
                MessageBox.Show("정보를 불러올 수 없습니다.");

            return jObject;
        }

        public static string GetEdcoreWorksJsonData(string key1)
        {
            JObject jObject1 = GetEdcoreWorksJsonObject();
            string returnString = string.Empty;

            foreach (System.Collections.Generic.KeyValuePair<string, JToken?> jKeyValue1 in jObject1)
            {
                if (jKeyValue1.Key.Equals(key1))
                {
                    returnString = jKeyValue1.Value!.ToString();
                    break;
                }

                if (jKeyValue1.Value is JObject jObject2)
                {
                    foreach (System.Collections.Generic.KeyValuePair<string, JToken?> jKeyValue2 in jObject2)
                    {
                        if (jKeyValue2.Key.Equals(key1))
                        {
                            returnString = jKeyValue2.Value!.ToString();
                            break;
                        }

                        if (jKeyValue2.Value is JObject jObject3)
                        {
                            foreach (System.Collections.Generic.KeyValuePair<string, JToken?> jKeyValue3 in jObject3)
                            {
                                if (jKeyValue3.Key.Equals(key1))
                                {
                                    returnString = jKeyValue3.Value!.ToString();
                                    break;
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
