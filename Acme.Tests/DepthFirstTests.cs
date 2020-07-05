using Acme.Graphs;
using Acme.Graphs.Strategies;
using Acme.Tests.TestHelpers;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Acme.Tests {
    public class DepthFirstTests {
        [Fact]
        public void GraphCannotBeNull() {
            typeof(DepthFirst).Invoking(_ =>
                DepthFirst.VisitAll(null, NodeIdentity.Of("A")).ToList())
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("graph");
        }

        [Fact]
        public void StartCannotBeNull() {
            typeof(DepthFirst).Invoking(_ =>
                DepthFirst.VisitAll(CreateSimpleGraph(), null).ToList())
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("start");
        }

        [Fact]
        public void VisitsNodesInOrder() {
            DepthFirst.VisitAll(CreateSimpleGraph(), NodeIdentity.Of("A"))
                .Should().ContainInOrder(
                    NodeIdentity.Of("A"),
                    NodeIdentity.Of("B2"),
                    NodeIdentity.Of("C2"),
                    NodeIdentity.Of("C1"),
                    NodeIdentity.Of("B1")
                ).And.HaveCount(5);
        }

        [Fact]
        public void DoesNotVisitDisconnectedNodes() {
            DepthFirst.VisitAll(
                GraphFactory.BuildGraph("A-B", "C-D"), NodeIdentity.Of("A"))
                .Should()
                .ContainInOrder(
                    NodeIdentity.Of("A"),
                    NodeIdentity.Of("B")
                ).And.HaveCount(2);
        }

        private static DirectedGraph CreateSimpleGraph() {
            return GraphFactory.BuildGraph("A-B1", "A-B2", "B1-C1", "B2-C1", "B2-C2");
        }
    }
}
