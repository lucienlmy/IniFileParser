// Copyright (c) 2019-2026 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using SoftCircuits.IniFileParser;

namespace IniFileParserTests
{
    public class TestSections
    {
        [Fact]
        public void Section_Test()
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
            var settings = file.GetSectionSettings(TestData.Sections[0]).ToArray();
            Assert.Equal(TestData.TotalItems, settings.Length);

            int i = 0, j;
            for (j = 0; j < TestData.StringValues.Length; j++)
            {
                Assert.Equal(TestData.StringValues[j].Name, settings[j + i].Name);
                Assert.Equal(TestData.StringValues[j].Value, settings[j + i].Value);
            }
            i += j;
            for (j = 0; j < TestData.IntValues.Length; j++)
            {
                Assert.Equal(TestData.IntValues[j].Name, settings[j + i].Name);
                Assert.Equal(TestData.IntValues[j].Value, int.Parse(settings[j + i].Value!));
            }
            i += j;
            for (j = 0; j < TestData.DoubleValues.Length; j++)
            {
                Assert.Equal(TestData.DoubleValues[j].Name, settings[j + i].Name);
                Assert.Equal(TestData.DoubleValues[j].Value, double.Parse(settings[j + i].Value!));
            }
            i += j;
            for (j = 0; j < TestData.BoolValues.Length; j++)
            {
                Assert.Equal(TestData.BoolValues[j].Name, settings[j + i].Name);
                Assert.Equal(TestData.BoolValues[j].Value, bool.Parse(settings[j + i].Value!));
            }
            i += j;
            for (j = 0; j < TestData.DateTimeValues.Length; j++)
            {
                Assert.Equal(TestData.DateTimeValues[j].Name, settings[j + i].Name);
                Assert.Equal(TestData.DateTimeValues[j].Value, DateTime.ParseExact(settings[j + i].Value!, IniFile.DefaultDateTimeFormat, null));
            }
        }

        [Fact]
        public void TestSameSectionNamesAreCombined()
        {
            string contents = """
                [Section1]
                test1=1
                test2=2
                test3=3

                [Section2]
                test1=1
                test2=2
                test3=3

                [Section3]
                test1=1
                test2=2
                test3=3

                [Section1]
                test4=4
                test5=5
                test6=6

                [Section2]
                test4=4
                test5=5
                test6=6

                [Section3]
                test4=4
                test5=5
                test6=6

                [Section1]
                test7=7
                test8=8
                test9=9

                [Section2]
                test7=7
                test8=8
                test9=9

                [Section3]
                test7=7
                test8=8
                test9=9
                """;

            IniFile file = new();
            file.LoadFromString(contents);
            var sections = file.GetSections();

            Assert.Equal(3, sections.Count());

            foreach (var section in sections)
            {
                Assert.Equal(9, file.GetSectionSettings(section).Count());
            }
        }
    }
}
