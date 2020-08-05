using System;

namespace WpfFungusApp.Model
{
    public class RegistryItemSerialiser : IConfigurationSerialiser
    {
        private string _keyPath = System.Environment.Is64BitOperatingSystem ? @"SOFTWARE\Wow6432Node\WpfFungusApp\DatabaseSettings" : @"SOFTWARE\WpfFungusApp\DatabaseSettings";

        public bool OpenKey()
        {
            CurrentRegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(_keyPath, true);
            return CurrentRegistryKey != null;
        }

        public bool IsOpen
        {
            get
            {
                return CurrentRegistryKey != null;
            }
        }

        public void Close()
        {
            if (CurrentRegistryKey != null)
            {
                CurrentRegistryKey.Close();
            }
            CurrentRegistryKey = null;
        }

        public bool CreateKey()
        {
            CurrentRegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(_keyPath, true);
            return CurrentRegistryKey != null;
        }

        private static T LoadRegistryEntry<T>(Microsoft.Win32.RegistryKey key, string name, T value)
        {
            if (key != null)
            {
                object obj = key.GetValue(name);
                if (obj != null)
                {
                    try
                    {
                        if (typeof(T).IsEnum)
                        {
                            obj = Enum.Parse(typeof(T), obj as string);
                        }
                        
                        value = (T)Convert.ChangeType(obj, typeof(T));
                    }
                    catch
                    {
                        // Fall through
                    }
                }
            }

            return value;
        }

        private Microsoft.Win32.RegistryKey CurrentRegistryKey { get; set; }

        #region Model.IConfigurationSerialiser

        public void WriteEntry<T>(string name, T value)
        {
            if (value != null)
            {
                CurrentRegistryKey.SetValue(name, value.ToString());
            }
        }

        public T ReadEntry<T>(string name, T defaultValue)
        {
            return LoadRegistryEntry<T>(CurrentRegistryKey, name, defaultValue);
        }

        #endregion Model.IConfigurationSerialiser
    }
}
