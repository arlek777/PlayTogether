using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace PlayTogether.Web.Infrastructure.Extensions
{
    public static class JsonExtensions
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

        public static ICollection<T> FromJsonList<T>(this string str)
        {
            return new JavaScriptSerializer().Deserialize<ICollection<T>>(str);
        }
    }
}
