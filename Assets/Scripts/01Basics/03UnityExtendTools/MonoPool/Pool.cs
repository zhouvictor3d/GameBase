

namespace GameCore.Pool
{
    using GameCore.MonoFactory;
    using System.Collections.Generic;

    // 可以使用对象池
    public interface IPoolable
    {
        void OnRecycled();

        bool IsRecycled { get; set; }
    }

    public interface IPool<T>
    {
        T Allocate();
        bool Recycle(T obj);
    }

    /// <summary>
    /// 当前可用对象
    /// </summary>
    public interface ICountObserveAble
    {
        int CurCount { get; }
    }

   
    /// <summary>
    /// 对象池Pool
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Pool<T> : IPool<T>, ICountObserveAble
    {
        protected IObjectFactory<T> mFactory;

        protected readonly Stack<T> mCacheStack = new Stack<T>();

        /// <summary>
        /// default is 10
        /// </summary>
        protected int mMaxCount = 10;

        public int CurCount
        {
            get { return mCacheStack.Count; }
        }

        /// <summary>
        /// 创建一个新的 T 对象
        /// </summary>
        /// <returns></returns>
        public T Allocate()
        {
            if (mCacheStack.Count == 0)
            {
                return mFactory.Create();
            }
            else
            {
                return mCacheStack.Pop();
            }
        }

        /// <summary>
        /// 标注循环可用
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public abstract bool Recycle(T obj);
    }
}