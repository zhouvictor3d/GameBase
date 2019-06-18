

namespace VC.Common.IO
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /*
     * IO的封装
     * 
     */
    public static class IOExtension
    {
        #region 路径操作
        public static string CreateDirectory(this string dirFullPath)
        {
            if (!Directory.Exists(dirFullPath))
            {
                Directory.CreateDirectory(dirFullPath);
            }
            return dirFullPath;
        }
        public static void DeleteDirectory(this string dirFullPath)
        {
            if (Directory.Exists(dirFullPath))
            {
                Directory.Delete(dirFullPath, true);
            }
        }
        public static void EmptyDirectory(this string dirFullPath)
        {
            if (Directory.Exists(dirFullPath))
            {
                Directory.Delete(dirFullPath, true);
            }

            Directory.CreateDirectory(dirFullPath);
        }
        
        #region 未经过测试
        public static string GetDirectoryName(string fileName)
        {
            fileName = MakePathStandard(fileName);
            return fileName.Substring(0, fileName.LastIndexOf('/'));
        }

        public static List<string> GetDirSubDirNameList(this string dirABSPath)
        {
            var di = new DirectoryInfo(dirABSPath);

            var dirs = di.GetDirectories();

            return dirs.Select(d => d.Name).ToList();
        }

        public static string GetPathParentFolder(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            return Path.GetDirectoryName(path);
        }

        #endregion

        #endregion

        #region 文件操作
        public static bool DeleteFile(this string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                File.Delete(fileFullPath);
                return true;
            }
            return false;
        }
        public static string CombinePath(this string selfPath, string toCombinePath)
        {
            return Path.Combine(selfPath, toCombinePath);
        }

        #region 未经过测试
        public static void SaveText(this string text, string path)
        {
            path.DeleteFile();
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (var sr = new StreamWriter(fs))
                {
                    sr.Write(text); //开始写入值
                }
            }
        }

        public static string ReadText(this FileInfo file)
        {
            return ReadText(file.FullName);
        }

        public static string ReadText(this string fileFullPath)
        {
            var result = string.Empty;
            using (var fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }

        public static string GetFileName(string name)
        {
            if (name == null)
            {
                return null;
            }

            int endIndex = name.LastIndexOf('.');
            if (endIndex > 0)
            {
                return name.Substring(0, endIndex);
            }
            return name;
        }

        public static string GetFileName(string path, char separator = '/')
        {
            path = IOExtension.MakePathStandard(path);
            return path.Substring(path.LastIndexOf(separator) + 1);
        }

        public static string GetFileNameWithoutExtention(string fileName, char separator = '/')
        {
            return GetFilePathWithoutExtention(GetFileName(fileName, separator));
        }

        public static string GetFilePathWithoutExtention(string fileName)
        {
            if (fileName.Contains("."))
            {
                return fileName.Substring(0, fileName.LastIndexOf('.'));
            }
            return fileName;
        }

        public static string GetFileExtendName(this string absOrAssetsPath)
        {
            var lastIndex = absOrAssetsPath.LastIndexOf(".");

            if (lastIndex >= 0)
            {
                return absOrAssetsPath.Substring(lastIndex);
            }

            return string.Empty;
        }

        /// <summary>
        /// 使路径标准化，去除空格并将所有'\'转换为'/'
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MakePathStandard(string path)
        {
            return path.Trim().Replace("\\", "/");
        }

        public static string GetDirPath(this string absOrAssetsPath)
        {
            var name = absOrAssetsPath.Replace("\\", "/");
            var lastIndex = name.LastIndexOf("/");
            return name.Substring(0, lastIndex + 1);
        }

        public static string GetLastDirName(this string absOrAssetsPath)
        {
            var name = absOrAssetsPath.Replace("\\", "/");
            var dirs = name.Split('/');

            return absOrAssetsPath.EndsWith("/") ? dirs[dirs.Length - 2] : dirs[dirs.Length - 1];
        }

        public static List<string> GetDirSubFilePathList(this string dirABSPath, bool isRecursive = true, string suffix = "")
        {
            var pathList = new List<string>();
            var di = new DirectoryInfo(dirABSPath);

            if (!di.Exists)
            {
                return pathList;
            }

            var files = di.GetFiles();
            foreach (var fi in files)
            {
                if (!string.IsNullOrEmpty(suffix))
                {
                    if (!fi.FullName.EndsWith(suffix, System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        continue;
                    }
                }

                pathList.Add(fi.FullName);
            }

            if (isRecursive)
            {
                var dirs = di.GetDirectories();
                foreach (var d in dirs)
                {
                    pathList.AddRange(GetDirSubFilePathList(d.FullName, isRecursive, suffix));
                }
            }

            return pathList;
        }

        #endregion

        #endregion
    }
}
