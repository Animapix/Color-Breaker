
using System.Collections.Generic;

namespace Color_Breaker
{
    public interface INodeTree
    {
        void Add(Node node);
        List<T> GetNodes<T>();
    }
}
