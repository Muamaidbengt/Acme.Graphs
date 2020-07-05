using Acme.Graphs;
using FluentAssertions;
using System;
using Xunit;

namespace Acme.Tests {
    public class DirectedEdgeTests {
        private readonly NodeIdentity foo = NodeIdentity.Of("foo");
        private readonly NodeIdentity bar = NodeIdentity.Of("bar");

        [Fact]
        public void HasUsefulToStringOverride() {
            var edge = DirectedEdge.Between(foo, bar);
            edge.ToString().Should().Be("foo -> bar");
        }

        [Fact]
        public void FromCanBeSameAsTo() {
            var edge = DirectedEdge.Between(foo, foo);
            edge.From.Should().Be(edge.To);
        }

        [Fact]
        public void FromCannotBeNull() {
            typeof(DirectedEdge).Invoking(_ => DirectedEdge.Between(null, foo))
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("from");
        }

        [Fact]
        public void ToCannotBeNull() {
            typeof(DirectedEdge).Invoking(_ => DirectedEdge.Between(foo, null))
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("to");
        }
    }
}
