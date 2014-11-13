﻿using System;
using Newtonsoft.Json;
using TweetinviCore.Enum;
using TweetinviCore.Extensions;

namespace TweetinviLogic.JsonConverters
{
    public class JsonLanguageConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return LanguageExtension.GetLangFromDescription((string) reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Language);
        }
    }
}