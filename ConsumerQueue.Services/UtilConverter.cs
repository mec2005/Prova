using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace ConsumerQueue.Services.Utilidades
{
    public static class UtilConverter
    {
        private static BinaryFormatter bf;

        public static byte[] ObjectToByteArray<T>(T obj)
        {
            if (obj == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                bf = null;
                return ms.ToArray();
            }               
        }
        
        public static T ByteArrayToObject<T>(byte[] arrBytes)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                bf = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                T obj = (T)bf.Deserialize(memStream);
                bf = null;
                return obj;
            }            
        }
    }
}
