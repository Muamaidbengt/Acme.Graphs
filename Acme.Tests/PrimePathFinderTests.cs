using Acme.Graphs.Strategies;
using Acme.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Acme.Tests {
    public class PrimePathFinderTests {
        // Introduction to Software Testing p. 40
        [Fact]
        public void ExampleFromIntroToSoftwareTesting() {
            var graph = GraphFactory.BuildGraph(
                "0-1", "0-4",
                "1-2", "1-5",
                "2-3",
                "3-1",
                "4-4", "4-6",
                "5-6");
            var sut = new PrimePathFinder();
            var primePaths = sut.FindPrimePaths(graph);

            primePaths.Should()
                .BeEquivalentTo(
                    GraphFactory.CreatePath("4", "4"),
                    GraphFactory.CreatePath("0", "4", "6"),
                    GraphFactory.CreatePath("0", "1", "2", "3"),
                    GraphFactory.CreatePath("0", "1", "5", "6"),
                    GraphFactory.CreatePath("1", "2", "3", "1"),
                    GraphFactory.CreatePath("2", "3", "1", "2"),
                    GraphFactory.CreatePath("3", "1", "2", "3"),
                    GraphFactory.CreatePath("2", "3", "1", "5", "6")
                );
        }
    }
}
