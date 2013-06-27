using System;
using System.IO;
using FluentAssertions;
using Jaevner.Core;
using NSubstitute;
using Xunit;

namespace Jaevner.Tests.Core
{
    public class JaevnerRunnerTests
    {
        public class Running
        {
            [Fact]
            public void MissingCalendarFileThrowsFileNotFoundException()
            {
                string path = @"c:\calendar.csv";
                var fileSystem = Substitute.For<IFileSystem>();
                fileSystem.FileExists(path).Returns(false);
                var service = new JaevnerService(null);

                var runner = new JaevnerRunner(fileSystem, service);

                Action act = () => runner.Run(new[] { path });

                act.ShouldThrow<FileNotFoundException>();
            }
        }
    }
}
