using Acme.Graphs;
using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Acme.Tests {
    public class PathTests {
        [Fact]
        public void CanBeEnumerated() {
            typeof(Path).Should().Implement<IEnumerable<NodeIdentity>>();
        }

        [Fact]
        public void StepsMustNotBeNull() {
            typeof(Path).Invoking(_ => Path.Of(null))
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("steps");
        }

        [Fact]
        public void EmptyPathIsEmpty() {
            Path.Empty.Should().BeEmpty();

            ((IEnumerable)Path.Empty).GetEnumerator()
                .MoveNext().Should().BeFalse();
        }

        [Fact]
        public void NonEmptyPathIsNotEmpty() {
            Path.Of(NodeIdentity.Of("foo")).Should().NotBeEmpty();
        }

        [Fact]
        public void CannotBeMutated() {
            var nodes = new List<NodeIdentity> { NodeIdentity.Of("foo") };
            var path = Path.Of(nodes);
            nodes.Add(NodeIdentity.Of("bar"));
            
            path.Should().NotContain(NodeIdentity.Of("bar"));
        }

        [Fact]
        public void StepOrderIsRetained() {
            var path = Path.Of(NodeIdentity.Of("first"), NodeIdentity.Of("second"));
            path.Should().ContainInOrder(NodeIdentity.Of("first"), NodeIdentity.Of("second"));
        }

        // TODO: equality tests
    }
}
