using System;
using System.Web.Script.Serialization;

namespace PlayTogether.Web.Infrastructure
{
    public static class JsonHelper
    {
        public static string ToJson(this object obj)
        {
            if(obj == null) { return String.Empty; }
            return new JavaScriptSerializer().Serialize(obj);
        }

        public static object FromJson(this string str)
        {
            return new JavaScriptSerializer().DeserializeObject(str);
        }

        public static T FromJson<T>(this string str)
        {
            return new JavaScriptSerializer().Deserialize<T>(str);
        }
    }
}
