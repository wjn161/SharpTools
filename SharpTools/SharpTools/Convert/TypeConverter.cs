namespace SharpTools.Convert
{
    using System;

    /// <summary>
    /// 各个值类型之间的转换类，使用相对安全的类型转换方式,默认会调用类型自带的TryParse方法来完成转换，若转换失败则会返回默认值
    /// </summary>
    public class TypeConverter
    {
        /// <summary>
        /// object类型转换为string类型
        /// 会先判断object是否为null，防止object为null的时候ToString方法报错
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <param name="defaultValue">如果object为null时返回的默认值</param>
        /// <returns>string字符串</returns>
        public static string ToString(object obj, string defaultValue)
        {
            return obj == null ? defaultValue : obj.ToString();
        }

        public static string ToString(object obj)
        {
            return ToString(obj, string.Empty);
        }

        public static int ToInt32(object obj, int defaultValue)
        {
            return obj == null ? defaultValue : ToInt32(obj.ToString(), defaultValue);
        }

        public static int ToInt32(string str, int defaultValue)
        {
            int result;
            return int.TryParse(str, out result) ? result : defaultValue;
        }

        public static short ToInt16(object obj, short defaultValue)
        {
            return obj == null ? defaultValue : ToInt16(obj.ToString(), defaultValue);
        }

        public static short ToInt16(string str, short defaultValue)
        {
            short result;
            return short.TryParse(str, out result) ? result : defaultValue;
        }

        public static long ToInt64(object obj, long defaultValue)
        {
            return obj == null ? defaultValue : ToInt64(obj.ToString(), defaultValue);
        }

        public static long ToInt64(string str, long defaultValue)
        {
            long result;
            return long.TryParse(str, out result) ? result : defaultValue;
        }

        public static uint ToUInt32(object obj, uint defaultValue)
        {
            return obj == null ? defaultValue : ToUInt32(obj.ToString(), defaultValue);
        }

        public static uint ToUInt32(string str, uint defaultValue)
        {
            uint result;
            return uint.TryParse(str, out result) ? result : defaultValue;
        }

        public static ushort ToUInt16(object obj, ushort defaultValue)
        {
            return obj == null ? defaultValue : ToUInt16(obj.ToString(), defaultValue);
        }

        public static ushort ToUInt16(string str, ushort defaultValue)
        {
            ushort result;
            return ushort.TryParse(str, out result) ? result : defaultValue;
        }

        public static ulong ToUInt64(object obj, ulong defaultValue)
        {
            return obj == null ? defaultValue : ToUInt64(obj.ToString(), defaultValue);
        }

        public static ulong ToUInt64(string str, ulong defaultValue)
        {
            ulong result;
            return ulong.TryParse(str, out result) ? result : defaultValue;
        }

        public static decimal ToDecimal(object obj, decimal defaultValue)
        {
            return obj == null ? defaultValue : ToDecimal(obj.ToString(), defaultValue);
        }

        public static decimal ToDecimal(string str, decimal defaultValue)
        {
            decimal result;
            return decimal.TryParse(str,out result) ? result : defaultValue;
        }

        public static double ToDouble(object obj, double defaultValue)
        {
            return obj == null ? defaultValue : ToDouble(obj.ToString(), defaultValue);
        }

        public static double ToDouble(string str, double defaultValue)
        {
            double result;
            return double.TryParse(str, out result) ? result : defaultValue;
        }

        public static float ToSingle(object obj, float defaultValue)
        {
            return obj == null ? defaultValue : ToSingle(obj.ToString(), defaultValue);
        }

        public static float ToSingle(string str, float defaultValue)
        {
            float result;
            return float.TryParse(str, out result) ? result : defaultValue;
        }

        public static DateTime ToDateTime(string str, DateTime defaultValue)
        {
            DateTime minValue;
            return DateTime.TryParse(str,out minValue) ? minValue : defaultValue;
        }

        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            return obj == null ? defaultValue : ToDateTime(obj.ToString(), defaultValue);
        }

        public static bool ToBoolean(string str, bool defaultValue)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            if (str == "0")
            {
                return false;
            }
            if (str == "1")
            {
                return true;
            }
            if (str.ToLower() == "false")
            {
                return false;
            }
            if (str.ToLower() == "true")
            {
                return true;
            }
            bool result;
            return bool.TryParse(str, out result) ? result : defaultValue;
        }
        public static bool ToBoolean(object obj, bool defaultValue)
        {
            return obj == null ? defaultValue : ToBoolean(obj.ToString(), defaultValue);
        }
    }
}