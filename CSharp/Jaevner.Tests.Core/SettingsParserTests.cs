using System;
using FluentAssertions;
using Jaevner.Core;
using Xunit;

namespace Jaevner.Tests.Core
{
    public class SettingsParserTests
    {
        public class GetCalendarFile
        {
            [Fact]
            public void ReturnsFilePathForCommandLineWithSingleEntryWithoutPrefix()
            {
                string[] args = new[] { @"C:\file.csv" };
                var parser = new SettingsParser();

                string path = parser.GetCalendarFile(args);

                path.Should().Be(@"C:\file.csv");
            }

            [Fact]
            public void ReturnsEmptyStringIfCommandLineWasNotSupplied()
            {
                string[] args = new string[] {};
                var parser = new SettingsParser();

                string path = parser.GetCalendarFile(args);

                path.Should().BeEmpty();
            }
        }

        public class GetNumberOfDaysToKeep
        {
            [Fact]
            public void ReturnsDefaultNumberOfDaysIfNoCommandLineValueIsSpecified()
            {
                string[] args = new[] { @"C:\file.csv" };

                var parser = new SettingsParser();

                int daysToKeep = parser.GetNumberOfDaysToKeep(args);

                daysToKeep.Should().Be(14);
            }

            [Fact]
            public void UsesCommandLineValueIfSpecified()
            {
                string[] args = new[] { @"C:\file.csv", "77" };

                var parser = new SettingsParser();

                int daysToKeep = parser.GetNumberOfDaysToKeep(args);

                daysToKeep.Should().Be(77);
            }

            [Fact]
            public void ReturnsDefaultNumberOfDaysToKeepIfCommandLineArgumentIsInvalid()
            {
                string[] args = new[] { @"C:\file.csv", "abc" };

                var parser = new SettingsParser();

                int daysToKeep = parser.GetNumberOfDaysToKeep(args);

                daysToKeep.Should().Be(14);
            }
        }

        public class GetSyncSettings
        {
            [Fact]
            public void ReturnsAllSettingsFromJson()
            {
                string json =
                    "{\"CalendarUrl\": \"http://jsonurl\", \"UserName\": \"jsonuser\",\"Password\": \"jsonpwd\"}";

                var parser = new SettingsParser();

                SyncSettings settings = parser.GetSyncSettings(json);

                settings.CalendarUrl.Should().Be("http://jsonurl");
                settings.UserName.Should().Be("jsonuser");
                settings.Password.Should().Be("jsonpwd");
            }

            [Fact]
            public void ThrowsExceptionIfJsonCannotBeDeserialized()
            {
                string json = "broken";

                var parser = new SettingsParser();

                Action action = () => parser.GetSyncSettings(json);

                action.ShouldThrow<Exception>();
            }

        }
    }
}
