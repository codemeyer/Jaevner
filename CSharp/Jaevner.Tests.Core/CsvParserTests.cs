using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Jaevner.Core;
using Xunit;

namespace Jaevner.Tests.Core
{
    public class CsvParserTests
    {
        public class Parse
        {
            [Fact]
            public void CanParseSingleLineDataIntoCorrectProperties()
            {
                string data = "\"Meeting\",\"2013-01-02\",\"14:30:00\",\"2013-01-02\",\"16:00:00\",\"True\",\"The Room\",\"Description\",\"4321\"";

                var parser = new CsvParser();

                List<JaevnerEntry> entries = parser.Parse(data);

                entries.Count.Should().Be(1);
                JaevnerEntry entry = entries.First();
                entry.Title.Should().Be("Meeting");
                entry.StartDateTime.Should().Be(new DateTime(2013, 01, 02, 14, 30, 00));
                entry.EndDateTime.Should().Be(new DateTime(2013, 01, 02, 16, 00, 00));
                entry.Location.Should().Be("The Room");
                entry.Description.Should().Be("Description");
                entry.AllDayEvent.Should().BeTrue();
                entry.UniqueId.Should().Be("4321");
            }

            [Fact]
            public void LineWithTooFewParametersThrowsException()
            {
                string data = "\"Too few params\",\"2013-01-02\"";
                var parser = new CsvParser();

                Action act = () => parser.Parse(data);

                act.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }
    }
}
