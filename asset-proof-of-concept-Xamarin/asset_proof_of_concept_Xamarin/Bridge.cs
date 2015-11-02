// <copyright file="Bridge.cs" company="RAGE"> Copyright (c) 2015 RAGE. All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>13-4-2015</date>
// <summary>Implements a Bridge with 3 interfaces</summary>
namespace AssetPackage
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Xamarin.Forms;

    /// <summary>
    /// A bridge.
    /// </summary>
    class Bridge : IBridge, ILogger, IDataStorage, IDataArchive, IDefaultSettings
    {
        /// <summary>
        /// Initializes a new instance of the AssetPackage.Bridge class.
        /// </summary>
        public Bridge()
        {
            this.Prefix = "";
        }

        /// <summary>
        /// Initializes a new instance of the AssetPackage.Bridge class.
        /// </summary>
        ///
        /// <param name="prefix"> The prefix. </param>
        public Bridge(String prefix)
            : base()
        {
            this.Prefix = prefix;
        }

        #region ILogger Members

        /// <summary>
        /// Executes the log operation.
        /// 
        /// Implement this in Game Engine Code.
        /// </summary>
        ///
        /// <param name="msg"> The message. </param>
        public void doLog(string msg)
        {
            //! Microsoft .Net Specific Code.
            // 
            Debug.WriteLine(Prefix + msg);
        }

        #endregion

        #region ILogger Properties

        /// <summary>
        /// The prefix.
        /// </summary>
        public String Prefix
        {
            get;
            set;
        }

        #endregion

        #region IDataStorage Members

        IDataStorage dataStorage = DependencyService.Get<IDataStorage>();
        IDataArchive dataArchive = DependencyService.Get<IDataArchive>();

        /// <summary>
        /// Exists the given file.
        /// </summary>
        ///
        /// <param name="fileId"> The file identifier to delete. </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public bool Exists(string fileId)
        {
            return dataStorage.Exists(fileId);
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
            return dataStorage.Files();
        }

        /// <summary>
        /// Saves the given file.
        /// </summary>
        ///
        /// <param name="fileId">   The file identifier to delete. </param>
        /// <param name="fileData"> Information describing the file. </param>
        public void Save(string fileId, string fileData)
        {
            dataStorage.Save(fileId, fileData);
        }

        /// <summary>
        /// Loads the given file.
        /// </summary>
        ///
        /// <param name="fileId"> The file identifier to delete. </param>
        ///
        /// <returns>
        /// A String.
        /// </returns>
        public string Load(string fileId)
        {
            return dataStorage.Load(fileId);
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
            return dataStorage.Delete(fileId);
        }

        #endregion

        #region IDataArchive Members

        /// <summary>
        /// Archives the given file.
        /// </summary>
        ///
        /// <param name="fileId"> The file identifier to delete. </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public bool Archive(string fileId)
        {
            return dataArchive.Archive(fileId);
        }

        /// <summary>
        /// Derive asset name.
        /// </summary>
        ///
        /// <param name="Class"> The class. </param>
        /// <param name="Id">    The identifier. </param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        private string DeriveAssetName(string Class, string Id)
        {
            return string.Format("{0}AppSettings", Class);
        }

        /// <summary>
        /// Query if 'Class' has default settings.
        /// </summary>
        ///
        /// <param name="Class"> The class. </param>
        /// <param name="Id">    The identifier. </param>
        ///
        /// <returns>
        /// true if default settings, false if not.
        /// </returns>
        public bool HasDefaultSettings(string Class, string Id)
        {
            String fn = DeriveAssetName(Class, Id) + ".xml";

            return dataStorage.Exists(fn);
        }

        /// <summary>
        /// Loads default settings.
        /// </summary>
        ///
        /// <param name="Class"> The class. </param>
        /// <param name="Id">    The identifier. </param>
        ///
        /// <returns>
        /// The default settings.
        /// </returns>
        public string LoadDefaultSettings(string Class, string Id)
        {
            String fn = DeriveAssetName(Class, Id) + ".xml";

            return dataStorage.Load(fn);
        }

        /// <summary>
        /// Saves a default settings.
        /// </summary>
        ///
        /// <param name="Class">    The class. </param>
        /// <param name="Id">       The identifier. </param>
        /// <param name="fileData"> Information describing the file. </param>
        public void SaveDefaultSettings(string Class, string Id, string fileData)
        {
            String fn = DeriveAssetName(Class, Id) + ".xml";

            dataStorage.Save(fn, fileData);
        }

        #endregion
    }
}
