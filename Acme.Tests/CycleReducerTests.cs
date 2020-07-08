using Acme.Graphs.Strategies;
using Acme.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Acme.Tests {
    public class CycleReducerTests {
        [Fact]
        public void ReducedGraphsHaveCorrectNodeAndCycleCount() {
            var graph = GraphFactory.BuildGraph(
                "0-1", "0-4",
                "1-2", "1-5",
                "2-3",
                "3-1",
                "4-6", "4-4",
                "5-6");
            var acyclic = CycleReducer.RemoveCycles(graph);
            acyclic.GraphWithoutCycles.Nodes.Should().HaveCount(5);
            acyclic.Cycles.Should().HaveCount(2); // 1-2-3-1 and 4-4
        }
    }
}
