//using MessagePack;
using System;
namespace CasCap.Models
{
    //[MessagePackObject(true)]//this attribute is not needed for MessagePack serialisation
    public class MyObject
    {
        public DateTime utcNow { get; set; } = DateTime.UtcNow;
        public string str { get; set; }
        public MyEnum myenum { get; set; }
        public int val1 { get; set; }
        public int val2 { get; set; }

        public override string ToString()
        {
            return $"{nameof(str)}={str}, {nameof(myenum)}={myenum}, {nameof(val1)}={val1}, {nameof(val2)}={val2}, {nameof(utcNow)}={utcNow:HH:mm:ss.fff}";
            //return $"{nameof(str)}={str}, {nameof(val1)}={val1}, {nameof(val2)}={val2}";
        }
    }
}