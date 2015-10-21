using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using AssetPackage;

[assembly: Dependency(typeof(DataStorage.Droid.Bridge_Android))]

namespace DataStorage.Droid
{
    public class Bridge_Android : IDataStorage, IDataArchive
    {
        /// <summary>
        /// Creates path to file.
        /// </summary>
        ///
        /// <param name="filename"> Filename of the file. </param>
        ///
        /// <returns>
        /// The new path to file.
        /// </returns>
        private string CreatePathToFile(string filename)
        {
            string docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(docsPath, "DataStorage");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Path.Combine(path, filename);
        }

        /// <summary>
        /// Creates path to archive.
        /// </summary>
        ///
        /// <param name="filename"> Filename of the file. </param>
        ///
        /// <returns>
        /// The new path to archive.
        /// </returns>
        private string CreatePathToArchive(string filename)
        {
            string docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(docsPath, "DataArchive");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Path.Combine(path, filename);
        }

        /// <summary>
        /// Deletes the given fileId.
        /// </summary>
        ///
        /// <param name="fileId"> The file identifier to delete. </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public bool Delete(string fileId)
        {
            string path = CreatePathToFile(fileId);

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determine if 'fileId' exists.
        /// </summary>
        ///
        /// <param name="fileId"> Identifier for the file. </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public bool Exists(string fileId)
        {
            string path = CreatePathToFile(fileId);

            return File.Exists(path);
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        ///
        /// <returns>
        /// An array of filenames.
        /// </returns>
        public String[] Files()
        {
            string path = CreatePathToFile(String.Empty);

            return Directory.GetFiles(path).ToList().ConvertAll(new Converter<String, String>(
                            p => p.Replace(path + Path.DirectorySeparatorChar, "")
                        )).ToArray();
        }

        /// <summary>
        /// Loads the given file.
        /// </summary>
        ///
        /// <param name="fileId"> The file identifier to load. </param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public string Load(string fileId)
        {
            string path = CreatePathToFile(fileId);

            using (StreamReader sr = File.OpenText(path))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// Saves the given file.
        /// </summary>
        ///
        /// <param name="fileId">   Identifier for the file. </param>
        /// <param name="fileData"> Information describing the file. </param>
        public void Save(string fileId, string fileData)
        {
            string path = CreatePathToFile(fileId);
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(fileData);
            }
        }

        /// <summary>
        /// Archives the given file.
        /// </summary>
        ///
        /// <param name="fileId"> Identifier for the file. </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public bool Archive(string fileId)
        {
            String stampName = String.Format("{0}-{1}{2}",
                Path.GetFileNameWithoutExtension(fileId),
               DateTime.Now.ToString("yyyy-MM-dd [HH mm ss fff]"),
                Path.GetExtension(fileId));

            string origin = CreatePathToFile(fileId);
            string dest = CreatePathToArchive(stampName);

            if (File.Exists(origin))
            {
                if (File.Exists(dest))
                {
                    File.Delete(dest);
                }


                File.Move(origin, CreatePathToArchive(stampName));

                return true;
            }

            return false;
        }
    }
}