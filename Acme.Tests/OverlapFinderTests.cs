using Acme.Graphs;
using Acme.Graphs.Strategies;
using Acme.Tests.TestHelpers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Acme.Tests {
    public class OverlapFinderTests {
        [Theory]
        [InlineData("ABC", "DEF")]
        [InlineData("BCD", "ABC")]
        [InlineData("3412T", "5412T")]
        public void NotOverlappingPaths(string first, string second) {
            var firstPath = CreatePath(first);
            var secondPath = CreatePath(second);

            ((object)OverlapFinder.FindOverlap(firstPath, secondPath))
                .Should().Be(Path.Empty);
        }

        [Theory]
        [InlineData("ABC", "CDE", "ABCDE")]
        [InlineData("ABC", "BCA", "ABCA")]
        [InlineData("ABCA", "ADE", "ABCADE")]
        [InlineData("ABC", "CA", "ABCA")]
        [InlineData("ABC", "C", "ABC")]
        public void OverlappingPaths(string first, string second, string expected) {
            var firstPath = CreatePath(first);
            var secondPath = CreatePath(second);
            var expectedOverlap = CreatePath(expected);
            ((object)OverlapFinder.FindOverlap(firstPath, secondPath))
                .Should().Be(expectedOverlap);
        }

        private Path CreatePath(string sequence) {
            return GraphFactory.CreatePath(sequence.Select(letter => letter.ToString()).ToArray());
        }
    }
}
