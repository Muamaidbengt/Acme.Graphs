using Acme.Graphs;
using Acme.Tests.TestHelpers;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Acme.Tests {
    public class BreadthFirstTests {
        [Fact]
        public void GraphCannotBeNull() {
            typeof(BreadthFirst).Invoking(_ =>
                BreadthFirst.VisitAll(null, NodeIdentity.Of("A")).ToList())
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("graph");
        }

        [Fact]
        public void StartCannotBeNull() {
            typeof(BreadthFirst).Invoking(_ =>
                BreadthFirst.VisitAll(CreateSimpleGraph(), null).ToList())
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("start");
        }

        [Fact]
        public void VisitsNodesInOrder() {
            BreadthFirst.VisitAll(CreateSimpleGraph(), NodeIdentity.Of("A"))
                .Should().ContainInOrder(
                    NodeIdentity.Of("A"),
                    NodeIdentity.Of("B1"),
                    NodeIdentity.Of("B2"),
                    NodeIdentity.Of("C1"),
                    NodeIdentity.Of("C2")
                ).And.HaveCount(5);
        }

        [Fact]
        public void DoesNotVisitDisconnectedNodes() {
            BreadthFirst.VisitAll(
                GraphFactory.BuildGraph("A-B", "C-D"), NodeIdentity.Of("A"))
                .Should()
                .ContainInOrder(
                    NodeIdentity.Of("A"),
                    NodeIdentity.Of("B")
                ).And.HaveCount(2);
        }

        [Fact]
        public void CanFindStraightPath() {
            BreadthFirst.FindPath(GraphFactory.BuildGraph("A-B", "B-C", "C-D"), NodeIdentity.Of("A"), node => node == NodeIdentity.Of("D"))
                .Should().ContainInOrder(
                    NodeIdentity.Of("A"),
                    NodeIdentity.Of("B"),
                    NodeIdentity.Of("C"),
                    NodeIdentity.Of("D")
                ).And.HaveCount(4);
        }

        [Fact]
        public void FindsShortestPath() {
            BreadthFirst.FindPath(
                GraphFactory.BuildGraph("A-B", "B-C", "C-D", "A-D"),
                NodeIdentity.Of("A"),
                node => node == NodeIdentity.Of("D"))
                .Should().ContainInOrder(
                    NodeIdentity.Of("A"),
                    NodeIdentity.Of("D")
                ).And.HaveCount(2);
        }

        [Fact]
        public void ReturnsEmptyPathIfNoPathIsFound() {
            var path = (object)BreadthFirst.FindPath(CreateSimpleGraph(),
                NodeIdentity.Of("A"),
                node => node == NodeIdentity.Of("notfound"));
            path.Should().Be(Path.Empty);
        }

        private static DirectedGraph CreateSimpleGraph() {
            return GraphFactory.BuildGraph("A-B1", "A-B2", "B1-C1", "B2-C1", "B2-C2");
        }
    }
}
