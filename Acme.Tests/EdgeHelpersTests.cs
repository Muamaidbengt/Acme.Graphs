using Acme.Graphs.Helpers;
using Acme.Tests.TestHelpers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Acme.Tests {
    public class EdgeHelpersTests {
        [Theory]
        [InlineData("ABC", "D-A", true)]
        [InlineData("ABC", "D-B", true)]
        [InlineData("ABC", "D-E", false)]
        [InlineData("ABC", "A-B", false)]
        [InlineData("ABC", "C-A", false)]
        public void LeadsInto(string pathDescription, string edgeDescription, bool expected) {
            var path = GraphFactory.CreatePath(pathDescription.Select(c => c.ToString()).ToArray());
            var edge = GraphFactory.CreateEdge(edgeDescription);

            edge.LeadsInto(path).Should().Be(expected);
        }

        [Theory]
        [InlineData("ABC", "A-D", true)]
        [InlineData("ABC", "B-D", true)]
        [InlineData("ABC", "D-E", false)]
        [InlineData("ABC", "A-B", false)]
        [InlineData("ABC", "C-A", false)]
        public void LeadsOutOf(string pathDescription, string edgeDescription, bool expected) {
            var path = GraphFactory.CreatePath(pathDescription.Select(c => c.ToString()).ToArray());
            var edge = GraphFactory.CreateEdge(edgeDescription);

            edge.LeadsOutOf(path).Should().Be(expected);
        }
    }
}
