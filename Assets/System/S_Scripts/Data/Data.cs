using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Kelvin.MasterData
{
    /// <summary>
    ///     Master class holder data in memory
    /// </summary>
    public static class Data
    {
        private const int INIT_SIZE = 64;
        private static int profile;
        private static Dictionary<string, byte[]> datas = new();

        public static event Action OnSaveEvent;

        #region Internal Stuff

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Init()
        {
            if (IsInitialized) return;
            IsInitialized = true;
            LoadAll();
            App.AddFocusCallback(OnApplicationFocus);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] Serialize<T>(T data)
        {
            return Kelvin.Serialize.ToBinary(data);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T Deserialize<T>(byte[] bytes)
        {
            return Kelvin.Serialize.FromBinary<T>(bytes);
        }

        private static void OnApplicationFocus(bool focus)
        {
            if (!focus) SaveAll();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void RequireNullCheck()
        {
            if (datas == null) LoadAll();
            if (datas == null) throw new NullReferenceException();
        }

        private static string GetPath => Path.Combine(Application.persistentDataPath, $"masterdata_{profile}.fo");

        #endregion

        #region Public API

        public static bool IsInitialized { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ChangeProfile(int profile)
        {
            if (Data.profile == profile) return;

            SaveAll();
            Data.profile = profile;
            LoadAll();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool VerifyProfile(int profile)
        {
            return Data.profile == profile;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SaveAll()
        {
            Debug.Log("saveAll");
            OnSaveEvent?.Invoke();
            var bytes = Serialize(datas);
            File.WriteAllBytes(GetPath, bytes);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void SaveAllAsync()
        {
            OnSaveEvent?.Invoke();

            var bytes = Serialize(datas);
            await File.WriteAllBytesAsync(GetPath, bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LoadAll()
        {
            if (!File.Exists(GetPath))
            {
                var stream = File.Create(GetPath);
                stream.Close();
            }

            var bytes = File.ReadAllBytes(GetPath);
            if (bytes.Length == 0)
            {
                datas.Clear();
                return;
            }

            datas = Deserialize<Dictionary<string, byte[]>>(bytes) ?? new Dictionary<string, byte[]>(INIT_SIZE);
            Debug.Log("LoadAll");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void LoadAllAsync()
        {
            if (!File.Exists(GetPath))
            {
                var stream = File.Create(GetPath);
                stream.Close();
            }

            var bytes = await File.ReadAllBytesAsync(GetPath);
            if (bytes.Length == 0)
            {
                datas.Clear();
                return;
            }

            datas = Deserialize<Dictionary<string, byte[]>>(bytes) ?? new Dictionary<string, byte[]>(INIT_SIZE);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default">
        ///     If value of <paramref name="key" /> can not be found or empty! will return the default value of
        ///     data type!
        /// </param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Load<T>(string key, T @default = default)
        {
            RequireNullCheck();

            datas.TryGetValue(key, out var value);
            if (value == null || value.Length == 0) return @default;

            return Deserialize<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryLoad<T>(string key, out T data)
        {
            RequireNullCheck();

            bool hasKey;
            if (datas.TryGetValue(key, out var value))
            {
                data = Deserialize<T>(value);
                hasKey = true;
            }
            else
            {
                data = default;
                hasKey = false;
            }

            return hasKey;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Save<T>(string key, T data)
        {
            RequireNullCheck();
            var bytes = Serialize(data);
            if (datas.TryAdd(key, bytes)) return;
            datas[key] = bytes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasKey(string key)
        {
            return datas.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteKey(string key)
        {
            datas.Remove(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteAll()
        {
            datas.Clear();
        }

        /// <summary>
        ///     Get raw byte[] of all data of profile
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] Backup()
        {
            return Kelvin.Serialize.ToBinary(datas);
        }

        /// <summary>
        ///     Load from byte[]
        /// </summary>
        /// <param name="bytes"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Restore(byte[] bytes)
        {
            datas = Kelvin.Serialize.FromBinary<Dictionary<string, byte[]>>(bytes);
        }

        #endregion
    }
}