namespace TheBookProject.Helpers;

public static class URIConverter
{
    
    public static string ConvertToString(string url)
    {
        Dictionary<string, string> characterKey = new Dictionary<string, string>()
        {
            { ":", "%3A" },
            { "/", "%2F" }
        };

        foreach (KeyValuePair<string, string> keyValuePair in characterKey)
        {
            url = url.Replace(keyValuePair.Key, keyValuePair.Value);
        }
        
        return url;
    }
}