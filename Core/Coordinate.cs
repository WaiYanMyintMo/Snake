using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Core
{
    public readonly struct Coordinate : IEquatable<Coordinate>
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }
        
        public bool Equals(Coordinate other) => X == other.X && Y == other.Y;

        public override bool Equals(object obj) => obj is Coordinate coordinate && Equals(coordinate);

        public static bool operator ==(Coordinate one, Coordinate other) => one.Equals(other);

        public static bool operator !=(Coordinate one, Coordinate other) => !one.Equals(other);

        public override int GetHashCode() => (X, Y).GetHashCode();
    }
}
