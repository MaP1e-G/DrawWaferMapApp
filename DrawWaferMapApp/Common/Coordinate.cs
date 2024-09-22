using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawWaferMapApp.Common
{
    public class Coordinate
    {
        public int X { set; get; } = 0;
        public int Y { set; get; } = 0;

        public override bool Equals(object obj)
        {
            if (obj is Coordinate other)
            {
                return this.X == other.X && this.Y == other.Y;
            }
                return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();  // 确保哈希表的正确存储和检索。两个相等的对象必须生成相同的哈希码。
        }

        public override string ToString()
        {
            return $"({this.X}, {this.Y})";
        }

        public static bool operator ==(Coordinate point1, Coordinate point2)
        {
            if (ReferenceEquals(point1, point2)) // 如果是同一对象引用
            {
                return true;
            }
            if (point1 is null || point2 is null) // 如果任一为空引用
            {
                return false;
            }
            return point1.Equals(point2); // 比较逻辑相等性
        }

        public static bool operator !=(Coordinate point1, Coordinate point2)
        {
            return !(point1 == point2);
        }
    }
}
