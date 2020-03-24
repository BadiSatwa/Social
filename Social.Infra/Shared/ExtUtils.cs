using System;
using System.Text;
using Newtonsoft.Json;

namespace Social.Infra.Shared
{
    public static class ExtUtils
    {
        public static string ToJson<TValue>(this TValue value) => JsonConvert.SerializeObject(value);

        public static byte[] ToBytes(this string value) => Encoding.UTF8.GetBytes(value);

        public static string AsString(this byte[] value) => Encoding.UTF8.GetString(value);

        public static TReturn ToObject<TReturn>(this string value, Type type) where TReturn : class =>
            JsonConvert.DeserializeObject(value, type) as TReturn;

        public static TReturn ToObject<TReturn>(this string value) => JsonConvert.DeserializeObject<TReturn>(value);
    }
}