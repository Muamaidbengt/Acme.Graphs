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
            var path = Path.Empty;
            path.Should().BeEmpty();

            ((IEnumerable)path).GetEnumerator()
                .MoveNext().Should().BeFalse();

            Path.IsEmpty(path).Should().BeTrue();
        }

        [Fact]
        public void EmptyPathHasLengthZero() {
            Path.Empty.Length.Should().Be(0);
        }

        [Fact]
        public void NonEmptyPathIsNotEmpty() {
            var path = Path.Of(NodeIdentity.Of("foo"));
            path.Should().NotBeEmpty();
            Path.IsEmpty(path).Should().BeFalse();
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

        [Fact]
        public void ThePathOfAbcStartsInA() {
            var path = Path.Of(NodeIdentity.Of("A"), NodeIdentity.Of("B"), NodeIdentity.Of("C"));
            path.Start.Should().Be(NodeIdentity.Of("A"));
        }

        [Fact]
        public void ThePathOfAbcEndsInC() {
            var path = Path.Of(NodeIdentity.Of("A"), NodeIdentity.Of("B"), NodeIdentity.Of("C"));
            path.End.Should().Be(NodeIdentity.Of("C"));
        }

        [Fact]
        public void ThePathOfAbcHasLength3() {
            var path = Path.Of(NodeIdentity.Of("A"), NodeIdentity.Of("B"), NodeIdentity.Of("C"));
            path.Length.Should().Be(3);
        }
    }
}
