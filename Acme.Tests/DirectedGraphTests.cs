using Acme.Graphs;
using Acme.Tests.TestHelpers;
using FluentAssertions;
using System;
using Xunit;

namespace Acme.Tests {
    public class DirectedGraphTests {
        [Fact]
        public void MustHaveEdges() {
            typeof(DirectedGraph)
                .Invoking(_ => DirectedGraph.Of(
                    null, GraphFactory.CreateNodes()))
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("edges");
        }

        [Fact]
        public void MustHaveNodes() {
            typeof(DirectedGraph).Invoking(_ =>
                DirectedGraph.Of(
                    GraphFactory.CreateEdges(), null))
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("nodes");
        }

        [Fact]
        public void MustNotHaveDuplicateNodes() {
            typeof(DirectedGraph)
                .Invoking(_ => DirectedGraph.Of(
                    GraphFactory.CreateEdges(),
                    GraphFactory.CreateNodes("A", "A")))
                .Should().Throw<ArgumentException>()
                .Which.ParamName.Should().Be("nodes");
        }

        [Fact]
        public void MayHaveDuplicateEdges() {
            var graph = typeof(DirectedGraph)
                .Invoking(_ => DirectedGraph.Of(
                    GraphFactory.CreateEdges("A-B", "A-B"),
                    GraphFactory.CreateNodes("A", "B")))
                .Should().NotThrow()
                .Subject;
            graph.EdgeCount.Should().Be(2);
        }
    }
}
