using System;
using System.Collections.Generic;
using Jaevner.Core;
using NSubstitute;
using Xunit;

namespace Jaevner.Tests.Core
{
    public class JaevnerServiceTests
    {
        public class RemoveIrrelevantEntries
        {
            [Fact]
            public void ExistingEntryInCalendarThatIsNotIncludedInNewDataIsDeleted()
            {
                const string uniqueId = "123456";
                var repository = Substitute.For<ICalendarRepository>();
                repository.ListEntries().Returns(new List<JaevnerEntry>
                    {
                        new JaevnerEntry
                            {
                                UniqueId = uniqueId
                            }
                    });
                
                var service = new JaevnerService(repository);

                service.RemoveIrrelevantEntries(new List<JaevnerEntry>(), 5);

                repository.Received().Remove(Arg.Is<JaevnerEntry>(e => e.UniqueId.Equals(uniqueId)));
            }

            [Fact]
            public void ExistingEntryThatIsOlderThanNumberOfDaysToKeepIsDeleted()
            {
                const string uniqueId = "123456";
                var repository = Substitute.For<ICalendarRepository>();
                var entries = new List<JaevnerEntry>();
                entries.Add(new JaevnerEntry
                    {
                        UniqueId = uniqueId,
                        StartDateTime = DateTime.Now.AddDays(-6)
                    });
                repository.ListEntries().Returns(entries);
                
                var service = new JaevnerService(repository);

                service.RemoveIrrelevantEntries(entries, 5);

                repository.Received().Remove(Arg.Is<JaevnerEntry>(e => e.UniqueId.Equals(uniqueId)));
            }

            [Fact]
            public void ExistingEntryThatIsNotOlderThanNumberOfDaysToKeepIsKept()
            {
                const string uniqueId = "123456";
                var repository = Substitute.For<ICalendarRepository>();
                var entries = new List<JaevnerEntry>();
                entries.Add(new JaevnerEntry
                {
                    UniqueId = uniqueId,
                    StartDateTime = DateTime.Now.AddDays(-2)
                });
                repository.ListEntries().Returns(entries);

                var service = new JaevnerService(repository);

                service.RemoveIrrelevantEntries(entries, 10);

                repository.DidNotReceive().Remove(Arg.Is<JaevnerEntry>(e => e.UniqueId.Equals(uniqueId)));
            }
        }

        public class ProcessEntries
        {
            [Fact]
            public void NewEntryIsAddedToRepository()
            {
                var repository = Substitute.For<ICalendarRepository>();
                repository.ListEntries().Returns(new List<JaevnerEntry>());
                const string uniqueId = "123456";

                var service = new JaevnerService(repository);

                var entries = new List<JaevnerEntry>();
                entries.Add(new JaevnerEntry { UniqueId = "123456", StartDateTime = DateTime.Now });
                service.ProcessEntries(entries);

                repository.Received().Insert(Arg.Is<JaevnerEntry>(e => e.UniqueId.Equals(uniqueId)));
            }

            [Fact]
            public void ExistingEntryIsUpdated()
            {
                const string uniqueId = "123456";
                var entries = new List<JaevnerEntry>();
                entries.Add(new JaevnerEntry { UniqueId = uniqueId, StartDateTime = DateTime.Now });
                var repository = Substitute.For<ICalendarRepository>();
                repository.EntryExists(uniqueId).Returns(true);

                var service = new JaevnerService(repository);

                service.ProcessEntries(entries);

                repository.Received().Update(Arg.Is<JaevnerEntry>(e => e.UniqueId.Equals(uniqueId)));
            }
        }
    }
}
