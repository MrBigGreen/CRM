using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Offertech.Util
{
    /// <summary>
    /// 序列化操作
    /// </summary>
    public class Serialize
    {
        /// <summary>
        /// 将对象序列化到字节流中
        /// </summary>
        /// <param name="instance">对象</param>        
        public static byte[] ToBytes(object instance)
        {
            if (instance == null)
                return null;
            BinaryFormatter serializer = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, instance);
                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// 将字节流反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类名</typeparam>
        /// <param name="buffer">字节流</param>        
        public static T FromBytes<T>(byte[] buffer) where T : class
        {
            if (buffer == null)
                return default(T);
            BinaryFormatter serializer = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;
                object result = serializer.Deserialize(stream);
                if (result == null)
                    return default(T);
                return (T)result;
            }
        }



        /// <summary>
        /// 类属性值互转
        /// </summary>
        /// <typeparam name="T">转出对象</typeparam>
        /// <param name="source">待转入的对象</param>
        /// <returns></returns>
        public static T CopySameFieldsObject<T>(Object source)
        {
            Type _SrcT = source.GetType();
            Type _DestT = typeof(T);

            // 构造一个要转换对象实例
            Object _Instance = _DestT.InvokeMember("", BindingFlags.CreateInstance, null, null, null);

            // 这里指定搜索所有公开和非公开的字段
            FieldInfo[] _SrcFields = _SrcT.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            // 将源头对象的每个字段的值分别赋值到转换对象里，因为假定字段都一样，这里就不做容错处理了
            foreach (FieldInfo field in _SrcFields)
            {
                FieldInfo info = _DestT.GetField(field.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (info != null)
                    info.SetValue(_Instance, field.GetValue(source));
            }

            return (T)_Instance;
        }


        public static PropertyInfo[] GetPropertyInfos(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public static void AutoMapping<S, T>(S s, T t)
        {
            // get source PropertyInfos
            PropertyInfo[] pps = GetPropertyInfos(s.GetType());
            // get target type
            Type target = t.GetType();

            foreach (var pp in pps)
            {
                PropertyInfo targetPP = target.GetProperty(pp.Name);
                object value = pp.GetValue(s, null);

                if (targetPP != null && value != null)
                {
                    targetPP.SetValue(t, value, null);
                }
            }
        }


    }
}
