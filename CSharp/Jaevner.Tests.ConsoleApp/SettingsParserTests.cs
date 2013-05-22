using FluentAssertions;
using Jaevner.ConsoleApp;
using Jaevner.Core;
using Xunit;

namespace Jaevner.Tests.ConsoleApp
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
            public void ReturnsFilePathForCommandLineWithSingleEntryWithCorrectPrefix()
            {
                string[] args = new[] { @"--file=C:\file.csv" };
                var parser = new SettingsParser();

                string path = parser.GetCalendarFile(args);

                path.Should().Be(@"C:\file.csv");
            }
        }

        public class GetSyncSettings
        {
            [Fact]
            public void ReturnsSettingsFromCommandLineIfBothCommandLineAndSettingsJsonAreSpecified()
            {
                string[] args = new[] { @"--file=C:\file.csv", "--calendarUrl=https://cmdurl", "--user=cmduser", "--pwd=cmdpwd", "--days=7" };
                string json =
                    "{\"CalendarUrl\": \"http://jsonurl\", \"UserName\": \"jsonuser\",\"Password\": \"jsonpwd\",\"DaysToKeep\": \"10\"}";
                var parser = new SettingsParser();

                SyncSettings settings = parser.GetSyncSettings(args, json);

                settings.CalendarUrl.Should().Be("https://cmdurl");
                settings.UserName.Should().Be("cmduser");
                settings.Password.Should().Be("cmdpwd");
                settings.DaysToKeep.Should().Be(7);
            }

            [Fact]
            public void ReturnsSettingsFromJsonIfNoCommandLineSettingsAreSpecified()
            {
                string[] args = new[] { @"--file=C:\file.csv" };
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
                string[] args = new[] { @"--file=C:\file.csv" };
                string json =
                    "{\"CalendarUrl\": \"http://jsonurl\", \"UserName\": \"jsonuser\",\"Password\": \"jsonpwd\",\"DaysToKeep\": \"\"}";

                var parser = new SettingsParser();

                SyncSettings settings = parser.GetSyncSettings(args, json);

                settings.DaysToKeep.Should().Be(14);
            }

        }
    }
}
