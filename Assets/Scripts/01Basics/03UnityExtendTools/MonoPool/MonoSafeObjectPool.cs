
namespace GameCore.Pool
{
    using GameCore.MonoFactory;

    public class MonoSafeObjectPool<T> : Pool<T> where T : IPoolable
    {
        /// <summary>
        /// 初始划
        /// </summary>
        /// <param name="objectFactory"></param>
        protected MonoSafeObjectPool(IObjectFactory<T> objectFactory)
        {
            mFactory = objectFactory;
        }

        public static MonoSafeObjectPool<T> Instance
        {
            get { return null; }
        }

        public override bool Recycle(T obj)
        {
            return true;
        }
    }
}
