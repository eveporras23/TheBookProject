using Newtonsoft.Json.Linq;

namespace TheBookProject.Helpers;

public static class JsonKeys
{
    public static object AccessJsonKeys(JObject json, string key)
    {
        if (json.TryGetValue(key, out JToken value))
        {
            return value;
        }
        else
        {
           return null; 
        }
    }
}