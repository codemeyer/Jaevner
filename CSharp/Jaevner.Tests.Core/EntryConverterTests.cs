using System;
using FluentAssertions;
using Google.GData.Calendar;
using Google.GData.Extensions;
using Jaevner.Core;
using Xunit;

namespace Jaevner.Tests.Core
{
    public class EntryConverterTests
    {
        public class GetJaevnerEntry
        {
            [Fact]
            public void ConvertingFilledItem()
            {
                var eventEntry = new EventEntry("Title", "Description", "Location");
                var eventTime = new When(DateTime.Now, DateTime.Now.AddHours(1));
                var extended = new ExtendedProperty();
                extended.Name = "UniqueId";
                extended.Value = "4321";
                eventEntry.ExtensionElements.Add(extended);
                eventEntry.Times.Add(eventTime);

                JaevnerEntry entry = EntryConverter.GetJaevnerEntry(eventEntry);

                entry.Title.Should().Be("Title");
                entry.Description.Should().Be("Description");
                entry.Location.Should().Be("Location");
                entry.StartDateTime.Should().BeCloseTo(DateTime.Now, 5000);
                entry.EndDateTime.Should().BeCloseTo(DateTime.Now.AddHours(1), 5000);
                entry.UniqueId.Should().Be("4321");
            }

            [Fact]
            public void EventEntryWithoutLocationReturnsJaevnerEntryWithEmptyLocation()
            {
                var eventEntry = new EventEntry("Title", "Description");
                var eventTime = new When(DateTime.Now, DateTime.Now.AddHours(1));
                eventEntry.Times.Add(eventTime);

                JaevnerEntry entry = EntryConverter.GetJaevnerEntry(eventEntry);

                entry.Location.Should().BeEmpty();
            }
        }

        public class GetEventEntry
        {
            [Fact]
            public void FilledEventEntryReturnsExpectedJaevnerEntry()
            {
                var entry = new JaevnerEntry();
                entry.Title = "Title";
                entry.Description = "Description";
                entry.Location = "Location";
                entry.StartDateTime = DateTime.Now;
                entry.EndDateTime = DateTime.Now.AddHours(1);

                EventEntry eventEntry = EntryConverter.GetEventEntry(entry);

                eventEntry.Title.Text.Should().Be("Title");
                eventEntry.Content.Content.Should().Be("Description");
                eventEntry.Locations[0].ValueString.Should().Be("Location");
                eventEntry.Times[0].StartTime.Should().BeCloseTo(DateTime.Now, 5000);
                eventEntry.Times[0].EndTime.Should().BeCloseTo(DateTime.Now.AddHours(1), 5000);
            }
        }
    }
}
