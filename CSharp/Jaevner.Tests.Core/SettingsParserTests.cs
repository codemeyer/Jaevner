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
        }

        public class GetSyncSettings
        {
            [Fact]
            public void ReturnsAllSettingsFromJson()
            {
                string[] args = new[] { @"C:\file.csv" };
                string json =
                    "{\"CalendarUrl\": \"http://jsonurl\", \"UserName\": \"jsonuser\",\"Password\": \"jsonpwd\",\"DaysToKeep\": \"10\"}";

                var parser = new SettingsParser();

                SyncSettings settings = parser.GetSyncSettings(args, json);

                settings.CalendarUrl.Should().Be("http://jsonurl");
                settings.UserName.Should().Be("jsonuser");
                settings.Password.Should().Be("jsonpwd");
                settings.DaysToKeep.Should().Be(10);
            }

            [Fact]
            public void ReturnsDefaultNumberOfDaysToKeepIfNotSpecified()
            {
                string[] args = new[] { @"C:\file.csv" };
                string json =
                    "{\"CalendarUrl\": \"http://jsonurl\", \"UserName\": \"jsonuser\",\"Password\": \"jsonpwd\",\"DaysToKeep\": \"\"}";

                var parser = new SettingsParser();

                SyncSettings settings = parser.GetSyncSettings(args, json);

                settings.DaysToKeep.Should().Be(14);
            }

            [Fact]
            public void ReturnsNumberOfDaysToKeepFromCommandLineIfNotSpecifiedInSettings()
            {
                string[] args = new[] { @"C:\file.csv", "77" };
                string json =
                    "{\"CalendarUrl\": \"http://jsonurl\", \"UserName\": \"jsonuser\",\"Password\": \"jsonpwd\",\"DaysToKeep\": \"\"}";

                var parser = new SettingsParser();

                SyncSettings settings = parser.GetSyncSettings(args, json);

                settings.DaysToKeep.Should().Be(77);
            }

            [Fact]
            public void ReturnsDefaultNumberOfDaysToKeepIfCommandLineArgumentIsInvalid()
            {
                string[] args = new[] { @"C:\file.csv", "abc" };
                string json =
                    "{\"CalendarUrl\": \"http://jsonurl\", \"UserName\": \"jsonuser\",\"Password\": \"jsonpwd\",\"DaysToKeep\": \"\"}";

                var parser = new SettingsParser();

                SyncSettings settings = parser.GetSyncSettings(args, json);

                settings.DaysToKeep.Should().Be(14);
            }

        }
    }
}
