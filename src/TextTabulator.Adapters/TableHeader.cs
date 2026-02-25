using System;

namespace TextTabulator.Adapters
{
    internal class TableHeader : IEquatable<TableHeader>
    {
        public string Name { get; }

        public int Index { get; }

        public TableHeader(string name, int index)
        {
            Name = name;
            Index = index;
        }

        public bool Equals(TableHeader? other)
        {
            return other != null && Name == other.Name && Index == other.Index;
        }

        public override bool Equals(object obj)
        {
            return obj is TableHeader cni && Equals(cni);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Index);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Index)}: {Index}";
        }
    }
}
