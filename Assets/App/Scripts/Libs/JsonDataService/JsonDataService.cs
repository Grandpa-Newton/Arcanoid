﻿using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace App.Scripts.Libs.JsonDataService
{
    public static class JsonDataService
    {
        public static T LoadFromResources<T>(string filePath)
        {
            var file = Resources.Load<TextAsset>(filePath);

            if (file != null)
            {
                return JsonConvert.DeserializeObject<T>(file.text);
            }

            Debug.LogWarning("File not found: " + filePath);
            return default;
        }
        
        public static bool SaveData<T>(string relativePath, T data)
        {
            var directoryPath = Path.Combine(Application.persistentDataPath,
                Path.GetDirectoryName(relativePath) ?? string.Empty);

            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(Application.persistentDataPath, relativePath);
                
                File.WriteAllText(filePath, JsonConvert.SerializeObject(data));

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Unable to save data due to: {e.Message}\n{e.StackTrace}");
                return false;
            }
        }

        
        public static T LoadData<T>(string relativePath)
        {
            var path = Path.Combine(Application.persistentDataPath, relativePath);

            if (!File.Exists(path))
            {
                Debug.LogError($"Cannot load file at {path}. File does not exist!");
                throw new FileNotFoundException($"{path} does not exist!");
            }

            try
            {
                var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return data;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load data due to: {e.Message}\n{e.StackTrace}");
                throw;
            }
        }

        public static bool TryLoadData<T>(string relativePath, out T data)
        {
            var path = Path.Combine(Application.persistentDataPath, relativePath);

            data = default;

            try
            {
                if (!File.Exists(path)) return false;

                data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load data due to: {e.Message}\n{e.StackTrace}");
                return false;
            }
        }
    }
}