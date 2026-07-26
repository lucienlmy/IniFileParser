// Copyright (c) 2019-2026 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using SoftCircuits.IniFileParser;
using System.Text;

namespace IniFileParserTests
{
    public class TestComments
    {
        [Fact]
        public void Comments_TestCharacter()
        {
            List<(char c, string[] keys)> comments =
            [
                ('\0', new [] { "a", "#c", "@d" }),
                (';', new [] { "a", "#c", "@d" }),
                ('#', new [] { "a", ";b", "@d" }),
                ('@', new [] { "a", ";b", "#c" }),
            ];

            string contents =
                """
                [General]
                a=0
                ;b=1
                #c=2
                @d=3
                """;

            foreach (var (c, keys) in comments)
            {
                IniFile file = new();

                if (c != '\0')
                    file.CommentCharacter = c;
                file.LoadFromString(contents);
                var settings = file.GetSectionSettings(IniFile.DefaultSectionName);
                Assert.Equal(keys, settings.Select(s => s.Name));
                //Assert.Collection(settings.Select(s => s.Name), )
                //CollectionAssert.AreEqual(keys, settings.Select(s => s.Name).ToList());
            }
        }

        [Fact]
        public void Comments_WriteAndRead()
        {
            IniFile file = new();

            file.Comments.Add("Abc");
            file.Comments.Add("123");
            file.Comments.Add("");
            file.Comments.Add(null);

            file.SetSetting(IniFile.DefaultSectionName, "Test", "Abc");
            file.SetSetting(IniFile.DefaultSectionName, "Test2", "123");
            byte[] buffer = file.SaveToBytes();

            file.LoadFromBytes(buffer);

            Assert.Equal(
                $"""
                {file.CommentCharacter}Abc
                {file.CommentCharacter}123
                {file.CommentCharacter}
                {file.CommentCharacter}

                [{IniFile.DefaultSectionName}]
                Test=Abc
                Test2=123

                """, Encoding.UTF8.GetString(buffer));

            Assert.Equal(4, file.Comments.Count);
            Assert.Equal("Abc", file.Comments[0]);
            Assert.Equal("123", file.Comments[1]);
            Assert.Equal("", file.Comments[2]);
            Assert.Equal("", file.Comments[3]);

            Assert.Equal("Abc", file.GetSetting(IniFile.DefaultSectionName, "Test"));
            Assert.Equal("123", file.GetSetting(IniFile.DefaultSectionName, "Test2"));
        }
    }
}
