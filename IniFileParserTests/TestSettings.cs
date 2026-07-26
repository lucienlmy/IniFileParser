// Copyright (c) 2019-2026 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using SoftCircuits.IniFileParser;
using System.Text;

namespace IniFileParserTests
{
    public class TestSettings
    {
        [Fact]
        public void Settings_TestValues()
        {
            IniFile file = new();
            foreach (string section in TestData.Sections)
            {
                foreach ((string Name, string Value) in TestData.StringValues)
                    file.SetSetting(section, Name, Value);
                foreach ((string Name, int Value) in TestData.IntValues)
                    file.SetSetting(section, Name, Value);
                foreach ((string Name, double Value) in TestData.DoubleValues)
                    file.SetSetting(section, Name, Value);
                foreach ((string Name, bool Value) in TestData.BoolValues)
                    file.SetSetting(section, Name, Value);
                foreach ((string Name, DateTime Value) in TestData.DateTimeValues)
                    file.SetSetting(section, Name, Value);
            }
            byte[] buffer = file.SaveToBytes();

            file.Clear();
            Assert.Empty(file.GetSections());

            file.LoadFromBytes(buffer);
            Assert.Equal(TestData.Sections.Length, file.GetSections().Count());
            foreach (string section in TestData.Sections)
            {
                Assert.Equal(TestData.TotalItems, file.GetSectionSettings(section).Count());
                foreach ((string Name, string Value) in TestData.StringValues)
                    Assert.Equal(Value, file.GetSetting(section, Name, string.Empty));
                foreach ((string Name, int Value) in TestData.IntValues)
                    Assert.Equal(Value, file.GetSetting(section, Name, 0));
                foreach ((string Name, double Value) in TestData.DoubleValues)
                    Assert.Equal(Value, file.GetSetting(section, Name, 0.0));
                foreach ((string Name, bool Value) in TestData.BoolValues)
                    Assert.Equal(Value, file.GetSetting(section, Name, false));
                foreach ((string Name, DateTime Value) in TestData.DateTimeValues)
                    Assert.Equal(Value, file.GetSetting(section, Name, DateTime.MinValue));
            }
        }

        [Fact]
        public void Settings_TestWhitespace()
        {
            StringBuilder builder = new();

            builder.AppendLine("  ;  Comment");
            builder.AppendLine("  ;  Comment");
            builder.AppendLine("  ;  Comment");

            foreach (string section in TestData.Sections)
            {
                builder.AppendLine();
                string spaces = Spaces();
                builder.AppendLine($"{spaces}[{spaces}{section}{spaces}{spaces}]{spaces}");
                foreach ((string Name, string Value) in TestData.StringValues)
                    builder.AppendLine($"{spaces}{Name}{spaces}={Value}");
                foreach ((string Name, int Value) in TestData.IntValues)
                    builder.AppendLine($"{spaces}{Name}{spaces}={Value}");
                foreach ((string Name, double Value) in TestData.DoubleValues)
                    builder.AppendLine($"{spaces}{Name}{spaces}={Value}");
                foreach ((string Name, bool Value) in TestData.BoolValues)
                    builder.AppendLine($"{spaces}{Name}{spaces}={Value}");
                foreach ((string Name, DateTime Value) in TestData.DateTimeValues)
                    builder.AppendLine($"{spaces}{Name}{spaces}={Value.ToString(IniFile.DefaultDateTimeFormat)}");
            }

            IniFile file = new();
            file.LoadFromString(builder.ToString());
            Assert.Equal(TestData.Sections.Length, file.GetSections().Count());
            foreach (string section in TestData.Sections)
            {
                Assert.Equal(TestData.TotalItems, file.GetSectionSettings(section).Count());
                foreach ((string Name, string Value) in TestData.StringValues)
                    Assert.Equal(Value, file.GetSetting(section, Name, string.Empty));
                foreach ((string Name, int Value) in TestData.IntValues)
                    Assert.Equal(Value, file.GetSetting(section, Name, 0));
                foreach ((string Name, double Value) in TestData.DoubleValues)
                    Assert.Equal(Value, file.GetSetting(section, Name, 0.0));
                foreach ((string Name, bool Value) in TestData.BoolValues)
                    Assert.Equal(Value, file.GetSetting(section, Name, false));
                foreach ((string Name, DateTime Value) in TestData.DateTimeValues)
                    Assert.Equal(Value, file.GetSetting(section, Name, DateTime.MinValue));
            }
        }

        private readonly (string Name, string Value)[] StringValuesWithWhitespace =
        [
            ("gbmoAGoUAX", "nhsvoVCFeS"),
            ("XknXpFZwAn", " lWShEVKQja"),
            ("HDekUKYhQI", "JECcqkbsWj "),
            ("OMzEAOThDc", "  AOLxPMBlys"),
            ("nLQfiLEUbC", "IwmKthVzgI  "),
            ("pBUqOjUVrP", "   KUlfghqlwM"),
            ("vMKHoVuDdp", "GLNbCnhGQR   "),
            ("yhUWOpEMys", " hQlQolVhVy "),
            ("cxIygWRbgc", "  jjYvzGlYEg  "),
            ("iBJOskHHDX", "      DazniZGfot      "),
        ];

        [Fact]
        public void TestTrimValues()
        {
            IniFile file = new();

            foreach ((string Name, string Value) in StringValuesWithWhitespace)
                file.SetSetting(IniFile.DefaultSectionName, Name, Value);

            byte[] buffer = file.SaveToBytes();

            file.Clear();
            Assert.Empty(file.GetSections());

            file.LoadFromBytes(buffer);
            foreach ((string Name, string Value) in StringValuesWithWhitespace)
            {
                Assert.Equal(Value, file.GetSetting(IniFile.DefaultSectionName, Name, string.Empty));
                Assert.Equal(Value, file.GetSetting(IniFile.DefaultSectionName, Name, string.Empty));
                Assert.Equal(Value, file.GetSetting(IniFile.DefaultSectionName, Name, string.Empty));
            }

            file.TrimValues = true;
            file.LoadFromBytes(buffer);
            foreach ((string Name, string Value) in StringValuesWithWhitespace)
            {
                Assert.Equal(Value.Trim(), file.GetSetting(IniFile.DefaultSectionName, Name, string.Empty));
                Assert.Equal(Value.Trim(), file.GetSetting(IniFile.DefaultSectionName, Name, string.Empty));
                Assert.Equal(Value.Trim(), file.GetSetting(IniFile.DefaultSectionName, Name, string.Empty));
            }
        }

        private static string Spaces() => new(' ', Random.Shared.Next(2, 14));

        [Fact]
        public void Settings_TestStringComparer()
        {
            IniFile file = new(StringComparer.Ordinal);
            foreach (string section in TestData.Sections)
            {
                foreach ((string Name, string Value) in TestData.StringValues)
                    file.SetSetting(section, Name, Value);
                foreach ((string Name, int Value) in TestData.IntValues)
                    file.SetSetting(section, Name, Value);
                foreach ((string Name, double Value) in TestData.DoubleValues)
                    file.SetSetting(section, Name, Value);
                foreach ((string Name, bool Value) in TestData.BoolValues)
                    file.SetSetting(section, Name, Value);
                foreach ((string Name, DateTime Value) in TestData.DateTimeValues)
                    file.SetSetting(section, Name, Value);
            }
            byte[] buffer = file.SaveToBytes();

            file.Clear();
            Assert.Empty(file.GetSections());

            file.LoadFromBytes(buffer);
            Assert.Equal(TestData.Sections.Length, file.GetSections().Count());
            foreach (string section in TestData.Sections)
            {
                Assert.Equal(TestData.TotalItems, file.GetSectionSettings(section).Count());
                foreach ((string Name, string Value) in TestData.StringValues)
                {
                    Assert.Equal(string.Empty, file.GetSetting(section, Name.ToUpper(), string.Empty));
                    Assert.Equal(string.Empty, file.GetSetting(section, Name.ToLower(), string.Empty));
                    Assert.Equal(Value, file.GetSetting(section, Name, string.Empty));
                }
                foreach ((string Name, int Value) in TestData.IntValues)
                {
                    Assert.Equal(0, file.GetSetting(section, Name.ToUpper(), 0));
                    Assert.Equal(0, file.GetSetting(section, Name.ToLower(), 0));
                    Assert.Equal(Value, file.GetSetting(section, Name, 0));
                }
                foreach ((string Name, double Value) in TestData.DoubleValues)
                {
                    Assert.Equal(0.0, file.GetSetting(section, Name.ToUpper(), 0.0));
                    Assert.Equal(0.0, file.GetSetting(section, Name.ToLower(), 0.0));
                    Assert.Equal(Value, file.GetSetting(section, Name, 0.0));
                }
                foreach ((string Name, bool Value) in TestData.BoolValues)
                {
                    Assert.False(file.GetSetting(section, Name.ToUpper(), false));
                    Assert.False(file.GetSetting(section, Name.ToLower(), false));
                    Assert.Equal(Value, file.GetSetting(section, Name, false));
                }
                DateTime now = DateTime.Now;
                foreach ((string Name, DateTime Value) in TestData.DateTimeValues)
                {
                    Assert.Equal(now, file.GetSetting(section, Name.ToUpper(), now));
                    Assert.Equal(now, file.GetSetting(section, Name.ToLower(), now));
                    Assert.Equal(Value, file.GetSetting(section, Name, now));
                }
            }
        }

        [Fact]
        public void Setting_DuplicateSettingNameOverridesPreviousValue()
        {
            string contents = """
                [General]
                test1=1
                test2=2
                test3=3
                test1=4
                test2=5
                test3=6
                test1=7
                test2=8
                test3=9
                """;

            IniFile file = new();
            file.LoadFromString(contents);
            var sections = file.GetSections();

            Assert.Single(sections);

            for (int i = 1; i <= 3; i++)
            {
                var setting = file.GetSetting("General", $"test{i}");
                Assert.Equal($"{6 + i}", setting);
            }
        }
    }
}
