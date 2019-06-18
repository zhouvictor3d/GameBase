

namespace GameCore.MonoFactory
{
    public interface IObjectFactory<T>
    {
        T Create();
    }

    public class DefaultObjectFactory<T> : IObjectFactory<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }


}