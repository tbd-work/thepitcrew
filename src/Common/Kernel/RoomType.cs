using System.Linq;
using System.Reflection;

namespace Common.Kernel
{
    public class RoomType
    {
        public static RoomType SingleRoom = new RoomType("SGL", "Single Occupancy");
        public static RoomType DoubleRoom = new RoomType("DBL", "Double Occupancy");
        public static RoomType KingRoom = new RoomType("KNG", "King Double Occupancy");

        public string Code { get; set; }
        public string Description { get; set; }

        public RoomType() { }

        private RoomType(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public static implicit operator RoomType(string type)
        {
            var nearestRoomType =
                typeof(RoomType).GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Where(f => f.ReflectedType == typeof(RoomType))
                    .Select(m => m.GetValue(null))
                    .Cast<RoomType>()
                    .Where(r => r.Code == type);

            return nearestRoomType.FirstOrDefault();
        }

        public override bool Equals(object obj)
        {
            if (obj is RoomType)
                return (obj as RoomType).Code == Code;

            return base.Equals(obj);
        }

        public bool Equals(string obj)
        {
            return obj == Code;
        }

        public override string ToString()
        {
            return Code;
        }
    }
}