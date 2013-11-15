using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace SharpTools.Serialization
{
    /// <summary>
    /// 序列化管理类，支持Xml,Json,二进制序列化,依赖Newtonsoft.Json库
    /// </summary>
    public class SerializationManager
    {
        #region Json
        /// <summary>
        ///  Json序列化设置器
        /// </summary>
        private static readonly JsonSerializerSettings settings;

        static SerializationManager()
        {
            settings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat //设置日期类型的序列化格式为：'2013-11-15T16:35:00' 
            };
        }
        /// <summary>
        /// 反序列化Json字符串为.net类型
        /// </summary>
        /// <typeparam name="T">任意类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>泛型类型</returns>
        public static T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
        /// <summary>
        /// 反序列化Json字符串为.net类型,非泛型
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>object类型</returns>
        public static object DeserializeJson(string json, Type targetType)
        {
            return JsonConvert.DeserializeObject(json, targetType, settings);
        }
        /// <summary>
        /// 反序列化Json字符串为.net类型,非泛型
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns>object类型</returns>
        public static object DeserializeJson(string json)
        {
            return JsonConvert.DeserializeObject(json, settings);
        }
        /// <summary>
        /// 把.net对象序列化为json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }
        #endregion

        #region XML
            
        #endregion

        #region Binary
        #endregion
    }
}
