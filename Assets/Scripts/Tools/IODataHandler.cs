using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Tools
{
    public static class IODataHandler
    {

        /// <summary>
        /// Takes in serializable class and saves it in at a specific path
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="filePath">path to file</param>
        /// <param name="fileName">name of file including extension</param>
        /// <param name="data">data to save</param>
        public static void Save<T>(string filePath, string fileName, T data)
        {
            FileStream stream = null;
            string path = Path.Combine(filePath, fileName);

            try
            {
                stream = File.Open(path, FileMode.OpenOrCreate);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            stream?.Close();
        }

        /// <summary>
        /// Takes in a path and a file name
        /// returns the file if available otherwise returns a default value
        /// </summary>
        /// <typeparam name="T">Data Type</typeparam>
        /// <param name="filePath">path to file</param>
        /// <param name="fileName">file name including extension</param>
        /// <returns></returns>
        public static T Load<T>(string filePath, string fileName)
        {
            T data = default;
            FileStream stream = null;
            BinaryFormatter formatter;
            string path = Path.Combine(filePath, fileName);

            if (File.Exists(path))
            {
                try
                {
                    stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    formatter = new BinaryFormatter();
                    data = (T)formatter.Deserialize(stream);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }

            stream?.Close();

            return data;
        }

        public static bool Exist(string filePath, string fileName)
        {
            string path = Path.Combine(filePath, fileName);
            return File.Exists(path);
        }
    }
}