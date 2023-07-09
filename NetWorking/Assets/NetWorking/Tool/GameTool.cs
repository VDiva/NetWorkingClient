using System.IO;
using Newtonsoft.Json;
using System.Text;
namespace NetWorking.Tool
{
    public static class GameTool
    {
        public static UnityEngine.Vector3 GameVector3ToMonoVector3(this GameData.Vector3 self)
        {
            return new UnityEngine.Vector3(self.X, self.Y, self.Z);
        }
        
        public static GameData.Vector3 MonoVector3ToGameVector3(this UnityEngine.Vector3 self)
        {
            return new GameData.Vector3 { X = self.x, Y = self.y, Z = self.z };
        }
        
        
        public static byte[] Packet(ref byte[] cache)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(cache.Length);
                bw.Write(cache);
                bw.Close();
                return ms.ToArray();    
            }
        }
        
        
        public static byte[] UnPacket(ref byte[] cache)
        {
            if (cache.Length < 4)
            {
                return null;
            }

            using (MemoryStream ms = new MemoryStream(cache))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    //包的长度（ReadInt32:读取一个4字节的带符号整数，并把流的位置向前移4个字节）
                    int length = br.ReadInt32();
                    //包的长度减去流的当前位置，用于判断包是否完整
                    int remainLength = (int)(ms.Length - ms.Position);
                    if (length > remainLength)
                    {
                        return null;
                    }

                    //读取数据,并且流的位置position向前移动length长度的字节
                    byte[] data = br.ReadBytes(length);

                    return data;
                }
            }
        }

        
        public static T DeSerialization<T>(byte[] data)
        {
            T da =JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(UnPacket(ref data)));
            return da;
        }

        public static byte[] Serialization(object data)
        {
            byte[] bytes =Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            return Packet(ref bytes);
        }
    }
}