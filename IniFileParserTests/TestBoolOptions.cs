// Copyright (c) 2019-2026 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using SoftCircuits.IniFileParser;

namespace IniFileParserTests
{
    public class TestBoolOptions
    {
        private static readonly List<(string Setting, string Word, bool Value, bool CanRead)> BoolOptionData =
        [
            ("Setting1", "vraie", true, true),
            ("Setting2", "faux", false, true),
            ("Setting3", "oui", true, true),
            ("Setting4", "non", false, true),
            ("Setting5", "sur", true, true),
            ("Setting6", "de", false, true),
            ("Setting7", "Boolean", false, false),
            ("Setting8", "Double", false, false),
            ("Setting9", "Vraie", true, false),
            ("Setting10", "Faux", false, false),
            ("Setting11", "Oui", true, false),
            ("Setting12", "Non", false, false),
            ("Setting13", "Sur", true, false),
            ("Setting14", "De", false, false),
            ("Setting15", "1", true, false),
            ("Setting16", "0", false, false),
            ("Setting17", "True", true, false),
            ("Setting18", "False", false, false),
        ];

        [Fact]
        public void Bool_TestOptions()
        {
            string stringSection = "StringSection";
            string boolSection = "BooleanSection";
            BoolOptions options = new(StringComparer.Ordinal);
            options.SetBoolWords([
                new BoolWord("vraie", true),
                new BoolWord("faux", false),
                new BoolWord("oui", true),
                new BoolWord("non", false),
                new BoolWord("sur", true),
                new BoolWord("de", false),
            ]);
            options.NonZeroNumbersAreTrue = false;

            IniFile file = new(null, options);
            // Write as string values
            foreach ((string Setting, string Word, bool _, bool _) in BoolOptionData)
                file.SetSetting(stringSection, Setting, Word);
            // Write as bool values
            foreach ((string Setting, string _, bool Value, bool _) in BoolOptionData)
                file.SetSetting(boolSection, Setting, Value);
            byte[] buffer = file.SaveToBytes();

            file.Clear();
            Assert.Empty(file.GetSections());
            file.LoadFromBytes(buffer);

            foreach ((string Setting, string _, bool Value, bool CanRead) in BoolOptionData)
            {
                bool result = file.GetSetting(stringSection, Setting, !Value);
                if (CanRead)
                    Assert.Equal(Value, result);
                else
                    Assert.Equal(!Value, result);
            }

            foreach ((string Setting, string _, bool Value, bool _) in BoolOptionData)
            {
                bool result = file.GetSetting(boolSection, Setting, !Value);
                Assert.Equal(Value, result);
            }
        }
    }
}
