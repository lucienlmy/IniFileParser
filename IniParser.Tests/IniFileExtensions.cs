// Copyright (c) 2019-2026 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.IniFileParser;
using System.IO;
using System.Text;

namespace IniParser.Tests
{
    public static class IniFileExtensions
    {
        public static byte[] SaveToBytes(this IniFile file)
        {
            using MemoryStream stream = new();
            using StreamWriter writer = new(stream);
            file.Save(writer);
            writer.Flush();
            return stream.ToArray();
        }

        public static void LoadFromBytes(this IniFile file, byte[] buffer)
        {
            using MemoryStream stream = new(buffer);
            using StreamReader reader = new(stream);
            file.Load(reader);
        }

        public static void LoadFromString(this IniFile file, string contents)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(contents);
            using MemoryStream stream = new(buffer);
            using StreamReader reader = new(stream);
            file.Load(reader);
        }
    }
}
