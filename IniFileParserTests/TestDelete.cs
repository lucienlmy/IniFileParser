// Copyright (c) 2019-2026 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using SoftCircuits.IniFileParser;

namespace IniFileParserTests
{
    public class TestDelete
    {

        [Fact]
        public void Delete_DeleteSetting()
        {
            IniFile file = new();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    file.SetSetting($"Section{i + 1}", $"Setting{j + 1}", $"Value{j + 1}");
                }
            }
            byte[] buffer = file.SaveToBytes();

            file.Clear();
            file.LoadFromBytes(buffer);

            Assert.True(file.DeleteSection("Section2"));
            Assert.True(file.DeleteSetting("Section3", "Setting3"));
            buffer = file.SaveToBytes();

            file.Clear();
            file.LoadFromBytes(buffer);

            var sectionSettings = file.GetSectionSettings("Section1");
            Assert.Equal(5, sectionSettings.Count());
            sectionSettings = file.GetSectionSettings("Section2");
            Assert.Empty(sectionSettings);

            Assert.NotNull(file.GetSetting("Section3", "Setting1"));
            Assert.NotNull(file.GetSetting("Section3", "Setting2"));
            Assert.Null(file.GetSetting("Section3", "Setting3"));
        }
    }
}
