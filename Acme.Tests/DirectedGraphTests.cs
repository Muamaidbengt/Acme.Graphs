using Acme.Graphs;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Acme.Tests {
    public class DirectedGraphTests {
        [Fact]
        public void MustHaveEdges() {
            typeof(DirectedGraph).Invoking(_ => DirectedGraph.Of(null, CreateNodes()))
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("edges");
        }

        [Fact]
        public void MustHaveNodes() {
            typeof(DirectedGraph).Invoking(_ => DirectedGraph.Of(CreateEdges(), null))
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("nodes");
        }

        [Fact]
        public void MustNotHaveDuplicateNodes() {
            typeof(DirectedGraph).Invoking(_ => DirectedGraph.Of(CreateEdges(), CreateNodes("A", "A")))
                .Should().Throw<ArgumentException>()
                .Which.ParamName.Should().Be("nodes");
        }

        private IEnumerable<DirectedEdge> CreateEdges() {
            yield return null;
        }

        private IEnumerable<NodeIdentity> CreateNodes(params string[] identifiers) {
            foreach (var i in identifiers) {
                yield return NodeIdentity.Of(i);
            }
        }
    }
}
