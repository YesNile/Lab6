using System;

namespace lab_6_ochkoshnik
{
    public class DataItem
    {
        public string Id { get; set; }
        public string RegDate { get; private set; }

        public static DataItem RandomInstance() =>
            new DataItem
            {
                Id = Generator.GenRndCharSeq(new Random().Next(3, 11)),
                RegDate = Generator.GenRndDate()
            };
    }
}