using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace shopapp.webui.Extentions
{
    public static class TempDataExtention
    {
        public static void Put<T>(this ITempDataDictionary @this,string key,T value)
            where T : class       
        {
            @this[key] = JsonConvert.SerializeObject(value);
        }

        public static T? Get<T>(this ITempDataDictionary @this,string key)
            where T : class       
        {
            object? o;

            @this.TryGetValue(key,out o);

            return o==null?null:JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}