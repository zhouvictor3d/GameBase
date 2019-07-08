

namespace VC.Common.IO
{
    using UnityEngine;
    using System.IO;
    using System.Collections.Generic;

    public class FilePath
    {
        private static string mPersistentDataPath;
        private static string mStreamingAssetsPath;

        private static string mPersistentDataPath4Res;
        private static string mPersistentDataPath4Photo;

        //外部目录
        public static string PersistentDataPath
        {
            get
            {
                if (null == mPersistentDataPath)
                {
                    mPersistentDataPath = Application.persistentDataPath + "/";
                }
                return mPersistentDataPath;
            }
        }

        //内部目录
        public static string StreamingAssetsPath
        {
            get
            {
                if (null == mStreamingAssetsPath)
                {
#if UNITY_IPHONE && !UNITY_EDITOR
					mStreamingAssetsPath = Application.streamingAssetsPath + "/";
#elif UNITY_ANDROID && !UNITY_EDITOR
					mStreamingAssetsPath = Application.streamingAssetsPath + "/";
#elif (UNITY_STANDALONE_WIN) && !UNITY_EDITOR
					mStreamingAssetsPath = Application.streamingAssetsPath + "/";
#elif UNITY_STANDALONE_OSX && !UNITY_EDITOR
					mStreamingAssetsPath = Application.streamingAssetsPath + "/";
#else
                    mStreamingAssetsPath = Application.streamingAssetsPath + "/";
#endif
                }
                return mStreamingAssetsPath;
            }
        }

        //外部资源目录
        public static string PersistentDataPath4Res
        {
            get
            {
                if (null == mPersistentDataPath4Res)
                {
                    mPersistentDataPath4Res = PersistentDataPath + "Res/";

                    if (!Directory.Exists(mPersistentDataPath4Res))
                    {
                        Directory.CreateDirectory(mPersistentDataPath4Res);
#if UNITY_IPHONE && !UNITY_EDITOR
						UnityEngine.iOS.Device.SetNoBackupFlag(mPersistentDataPath4Res);
#endif
                    }
                }

                return mPersistentDataPath4Res;
            }
        }

        // 外部头像缓存目录
        public static string PersistentDataPath4Photo
        {
            get
            {
                if (null == mPersistentDataPath4Photo)
                {
                    mPersistentDataPath4Photo = PersistentDataPath + "Photos\\";
                    IOExtension.CreateDirectory(mPersistentDataPath4Photo);
                }
                return mPersistentDataPath4Photo;
            }
        }

    }
}