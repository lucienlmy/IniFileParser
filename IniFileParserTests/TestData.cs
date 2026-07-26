// Copyright (c) 2019-2026 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
namespace IniFileParserTests
{
    public class TestData
    {
        public static readonly string[] Sections =
        [
            "GrRkfUPlTn",
            "MHvqnvgjqL",
            "mSXWTrLnvh",
            "EYEkCmmnpH",
            "GMtwdWTGLi",
            "nOQdCPysMC",
            "aKZSjRkKyl",
            "oZtZhwJjBq",
            "PTKHCxEBbT",
            "PidCWOuCUy",
        ];

        public static readonly (string Name, string Value)[] StringValues =
        [
            ("gbmoAGoUAX", "nhsvoVCFeS"),
            ("XknXpFZwAn", "lWShEVKQja"),
            ("HDekUKYhQI", "JECcqkbsWj"),
            ("OMzEAOThDc", "AOLxPMBlys"),
            ("nLQfiLEUbC", "IwmKthVzgI"),
            ("pBUqOjUVrP", "KUlfghqlwM"),
            ("vMKHoVuDdp", "GLNbCnhGQR"),
            ("yhUWOpEMys", "hQlQolVhVy"),
            ("cxIygWRbgc", "jjYvzGlYEg"),
            ("iBJOskHHDX", "DazniZGfot"),
        ];

        public static readonly (string Name, int Value)[] IntValues =
        [
            ("RdxljKJfFz", 98423023),
            ("EnMeiBRqNg", 202612153),
            ("bUHJQbePEf", 386456548),
            ("nyvZolLMUc", 127322448),
            ("ynqwRVbRwF", 197385250),
            ("gKukfnfwLp", 106966128),
            ("tBlZXBkTlB", 67822807),
            ("HnonxtSYwE", 277714502),
            ("vejHyhMynk", 179483129),
            ("SXpdOXFNVr", 138444389),
        ];

        public static readonly (string Name, double Value)[] DoubleValues =
        [
            ("OwKRKjYfPH", 0.7080722530),
            ("FanKstopNJ", 0.5025695865),
            ("zEJOKrIoRN", 0.8479851980),
            ("XIIdmOtRLq", 0.4750674692),
            ("vPveQiqyTX", 0.1760267518),
            ("YXVkXUaAwS", 0.0971605970),
            ("VGhLoQaPqB", 0.8404616145),
            ("dIvDIbeEhZ", 0.2999469886),
            ("EZTmbnoMFN", 0.0680769511),
            ("TWfWqkMubh", 0.1385922434),
        ];

        public static readonly (string Name, bool Value)[] BoolValues =
        [
            ("QKgfNDkkjI", true),
            ("dcciuRwwyD", true),
            ("pvKHvDqNCm", false),
            ("oOriwnnKni", true),
            ("aPnWFKvbzK", false),
            ("NeMmlJsCwa", true),
            ("wMfdfKYEKj", false),
            ("GLdUrFdAex", false),
            ("VsmGCAwcTp", true),
            ("KcNDDohDUb", false),
        ];

        public static readonly (string Name, DateTime Value)[] DateTimeValues =
        [
            ("RcgbLRxalW", new(2024, 3, 24, 12, 45, 13, 123)),
            ("dcdibRwxyD", new(1900, 12, 25)),
            ("pvKRvDqOCm", new(1961, 10, 29)),
            ("oXriwrtKni", new(2001, 6, 19)),
            ("bPqWFRvbaK", new(2024, 6, 22, 11, 14, 9, 234)),
            ("NeMnlWrCwa", new(1974, 11, 18, 22, 30, 59, 999)),
            ("VskGCAxcVp", new(2022, 8, 17, 15, 45, 1, 1)),
            ("KsNDDjjDUb", new(2018, 10, 23, 16, 18, 37, 829)),
            ("wMssfPREKj", DateTime.MinValue),
            //("GLeUrFdBfy", DateTime.MaxValue),    // Fails because format does not include microseconds
        ];

        // Sum of all values lists
        public static readonly int TotalItems;

        static TestData()
        {
            TotalItems = StringValues.Length + IntValues.Length + DoubleValues.Length + BoolValues.Length + DateTimeValues.Length;
        }
    }
}
