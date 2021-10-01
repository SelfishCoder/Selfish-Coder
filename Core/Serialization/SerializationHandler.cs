using System.IO;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SelfishCoder.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class SerializationHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool ExistsAtPersistentDataPath(string fileName)
        {
            return File.Exists($"{Application.persistentDataPath}/{fileName}.dat");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool ExistsAtSavesPath(string fileName)
        {
            return Directory.Exists($"{Application.persistentDataPath}/Saves") && File.Exists($"{Application.persistentDataPath}/Saves/{fileName}.dat");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool ExistsAtStreamingAssetsPath(string fileName)
        {
            return File.Exists($"{Application.streamingAssetsPath}/{fileName}.dat");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        /// <param name="fileMode"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="SerializationException"></exception>
        public void Serialize<T>(T data, string fileName, FileMode fileMode = FileMode.Create)
        {
            string directoryPath = $"{Application.persistentDataPath}/Saves/";
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            string filePath = string.Concat(directoryPath, $"{fileName}.dat");
            FileStream fileStream = new FileStream(filePath, fileMode);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                binaryFormatter.Serialize(fileStream, data);
            }
            catch (SerializationException exception)
            {
                throw new SerializationException($"Serialization failed. Error: {exception.Message}");
            }
            finally
            {
                fileStream.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="SerializationException"></exception>
        public T Deserialize<T>(string fileName)
        {
            string path = $"{Application.persistentDataPath}/Saves/{fileName}.dat";
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            T load;
            try
            {
                load = (T) formatter.Deserialize(fileStream);
            }
            catch (SerializationException exception)
            {
                throw new SerializationException($"Deserialization failed. Error: {exception.Message}" + $"{fileStream}" + $"{path}");
            }
            finally
            {
                fileStream.Close();
            }

            return load;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void Delete(string path)
        {
            if (!Exists(path)) return;
            File.Delete(path);
        }
    }
}