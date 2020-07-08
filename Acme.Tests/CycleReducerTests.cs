using Acme.Graphs.Strategies;
using Acme.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Acme.Tests {
    public class CycleReducerTests {
        [Fact]
        public void GraphWithoutCyclesRemainsUntouched() {
            var graph = GraphFactory.BuildGraph("0-1", "1-2", "2-3", "2-4");
            var acyclic = CycleReducer.RemoveCycles(graph);
            acyclic.Cycles.Should().HaveCount(0);
            acyclic.GraphWithoutCycles.Nodes.Should().HaveCount(5);
        }

        [Fact]
        public void GraphWithOnlyACycleIsSubstituted() {
            var graph = GraphFactory.BuildGraph("0-1", "1-2", "2-0");
            var acyclic = CycleReducer.RemoveCycles(graph);
            acyclic.Cycles.Should().HaveCount(1);
            acyclic.GraphWithoutCycles.Nodes.Should().HaveCount(1);
        }

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
