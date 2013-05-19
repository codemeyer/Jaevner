using System;
using System.Collections.Generic;

namespace Jaevner.Core
{
    public class CsvParser
    {
        public List<JaevnerEntry> Parse(string data)
        {
            string[] lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var entries = new List<JaevnerEntry>();
 
            foreach (string line in lines)
            {
                string[] lineData = line.Split(new[] { "\",\"" }, StringSplitOptions.None);

                if (lineData.Length < 9)
                {
                    throw new ArgumentOutOfRangeException("data");
                }

                var entry = new JaevnerEntry();
                
                entry.Title = lineData[0].TrimStart(new[] { '"' });
                entry.StartDateTime = GetDateFromDateTimeStrings(lineData[1], lineData[2]);
                entry.EndDateTime = GetDateFromDateTimeStrings(lineData[3], lineData[4]);
                entry.AllDayEvent = Convert.ToBoolean(lineData[5]);
                entry.Location = lineData[6];
                entry.Description = lineData[7];
                entry.UniqueId = lineData[8].TrimEnd(new[] { '"' });

                entries.Add(entry);
            }

            return entries;
        }

        private DateTime GetDateFromDateTimeStrings(string date, string time)
        {
            string dateString = string.Format("{0} {1}", date, time);
            DateTime parsedDateTime = DateTime.Parse(dateString);
            return parsedDateTime;
        }
    }
}
