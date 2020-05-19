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
            typeof(Path).Invoking(_ => Path.Of((IEnumerable<NodeIdentity>)null))
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

        [Fact]
        public void TwoPathsOverSameNodesAreEqual() {
            object p1 = Path.Of(NodeIdentity.Of("A"), NodeIdentity.Of("B"));
            object p2 = Path.Of(NodeIdentity.Of("A"), NodeIdentity.Of("B"));
            p1.Should().Be(p2);
            ((Path)p1 == (Path)p2).Should().BeTrue();
            ((Path)p1 != (Path)p2).Should().BeFalse();
        }

        [Fact]
        public void TwoPathsOverSameNodesButDifferentOrderAreNotEqual() {
            object p1 = Path.Of(NodeIdentity.Of("A"), NodeIdentity.Of("B"));
            object p2 = Path.Of(NodeIdentity.Of("B"), NodeIdentity.Of("A"));
            p1.Should().NotBe(p2);
            ((Path)p1 == (Path)p2).Should().BeFalse();
            ((Path)p1 != (Path)p2).Should().BeTrue();
        }

        [Fact]
        public void TwoPathsOverSameNodesHaveSameHashcode() {
            var p1 = Path.Of(NodeIdentity.Of("A"), NodeIdentity.Of("B"));
            var p2 = Path.Of(NodeIdentity.Of("A"), NodeIdentity.Of("B"));
            p1.GetHashCode().Should().Be(p2.GetHashCode());
        }

        [Fact]
        public void TwoPathsOverSameNodesButDifferentOrderHaveDifferentHashcode() {
            var p1 = Path.Of(NodeIdentity.Of("A"), NodeIdentity.Of("B"));
            var p2 = Path.Of(NodeIdentity.Of("B"), NodeIdentity.Of("A"));
            p1.GetHashCode().Should().NotBe(p2.GetHashCode());
        }

        [Fact]
        public void APathIsNotEqualToNull() {
            var p1 = Path.Empty;
            (null == p1).Should().BeFalse();
            (p1 == null).Should().BeFalse();
            p1.Should().NotBeNull();
        }
    }
}
