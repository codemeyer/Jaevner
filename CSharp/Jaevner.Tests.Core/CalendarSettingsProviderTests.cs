using FluentAssertions;
using Jaevner.Core;
using NSubstitute;
using Xunit;

namespace Jaevner.Tests.Core
{
    public class CalendarSettingsProviderTests
    {
        public class ReadingSettings
        {
            [Fact]
            public void ReturnsAllSettingsFromJson()
            {
                string json =
                    "{\"CalendarUrl\": \"http://jsonurl\", \"UserName\": \"jsonuser\",\"Password\": \"jsonpwd\"}";
                var settingsFileReader = Substitute.For<ISettingsFileReader>();
                settingsFileReader.ReadJson().Returns(json);
                var settings = new CalendarSettingsProvider(settingsFileReader);

                settings.CalendarUrl.Should().Be("http://jsonurl");
                settings.UserName.Should().Be("jsonuser");
                settings.Password.Should().Be("jsonpwd");
            }

            [Fact]
            public void ReturnsEmptyValuesIfJsonCannotBeDeserialized()
            {
                string json = "broken";
                var settingsFileReader = Substitute.For<ISettingsFileReader>();
                settingsFileReader.ReadJson().Returns(json);
                var settings = new CalendarSettingsProvider(settingsFileReader);

                settings.CalendarUrl.Should().BeBlank();
                settings.UserName.Should().BeBlank();
                settings.Password.Should().BeBlank();
            }
        }
    }
}
