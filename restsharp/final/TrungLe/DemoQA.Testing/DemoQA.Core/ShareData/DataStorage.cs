using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Core.ShareData
{
    public class DataStorage
    {
        private static AsyncLocal<Dictionary<string, object>> _data = new AsyncLocal<Dictionary<string, object>>();

        public static void InitData()
        {
            _data.Value = new Dictionary<string, object>();
        }

        public static void SetData(string key, object value)
        {
            EnsureInitialized();
            _data.Value[key] = value;
        }

        public static object GetData(string key)
        {
            EnsureInitialized();
            return _data.Value.TryGetValue(key, out var value) ? value : null;
        }

        public static void ClearData()
        {
            EnsureInitialized();
            _data.Value.Clear();
        }

        private static void EnsureInitialized()
        {
            if (_data.Value == null)
            {
                _data.Value = new Dictionary<string, object>();
            }
        }
    }
}
