using Acme.Graphs;
using FluentAssertions;
using System;
using Xunit;

namespace Acme.Tests {
    public class NodeIdentityTests {
        [Fact]
        public void HasUsefulToStringOverride() {
            NodeIdentity.Of("foobar").ToString().Should().Be("foobar");
        }

        [Fact]
        public void IdentityCannotBeNull() {
            typeof(NodeIdentity).Invoking(_ => NodeIdentity.Of(null))
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("identity");
        }

        [Fact]
        public void ObjectEquals() {
            var x = NodeIdentity.Of("foo");
            var y = NodeIdentity.Of("foo");
            x.Should().Be(y);
        }

        [Fact]
        public void ObjectEqualsItself() {
            var x = NodeIdentity.Of("foo");
            x.Should().Be(x);
        }

        [Fact]
        public void ObjectEqualsNegativeTest() {
            var x = NodeIdentity.Of("foo");
            var y = NodeIdentity.Of("bar");
            x.Should().NotBe(y);
        }

        [Fact]
        public void EqualsOp() {
            var x = NodeIdentity.Of("foo");
            var y = NodeIdentity.Of("foo");

            (x == y).Should().BeTrue();
        }

        [Fact]
        public void EqualsOpItself() {
            var x = NodeIdentity.Of("foo");
#pragma warning disable CS1718 // Comparison made to same variable
            (x == x).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [Fact]
        public void EqualsOpNegativeTest() {
            var x = NodeIdentity.Of("foo");
            var y = NodeIdentity.Of("bar");

            (x == y).Should().BeFalse();
        }

        [Fact]
        public void EqualsNullOp() {
            var x = NodeIdentity.Of("foo");

            (x == null).Should().BeFalse();
        }

        [Fact]
        public void NotEqualsOp() {
            var x = NodeIdentity.Of("foo");
            var y = NodeIdentity.Of("bar");

            (x != y).Should().BeTrue();
        }

        [Fact]
        public void NotEqualsOpItself() {
            var x = NodeIdentity.Of("foo");

#pragma warning disable CS1718 // Comparison made to same variable
            (x != x).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [Fact]
        public void NotEqualsOpNegativeTest() {
            var x = NodeIdentity.Of("foo");
            var y = NodeIdentity.Of("foo");

            (x != y).Should().BeFalse();
        }

        [Fact]
        public void NotEqualsNullOp() {
            var x = NodeIdentity.Of("foo");

            (x != null).Should().BeTrue();
        }

        [Fact]
        public void Equatable() {
            var x = NodeIdentity.Of("foo");
            var y = NodeIdentity.Of("foo");
            x.Equals(y).Should().BeTrue();
        }

        [Fact]
        public void EquatableToItself() {
            var x = NodeIdentity.Of("foo");
            x.Equals(x).Should().BeTrue();
        }

        [Fact]
        public void EquatableNegativeTest() {
            var x = NodeIdentity.Of("foo");
            var y = NodeIdentity.Of("bar");
            x.Equals(y).Should().BeFalse();
        }

        [Fact]
        public void EquatableNull() {
            var x = NodeIdentity.Of("foo");
            x.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void HashCode() {
            var x = NodeIdentity.Of("foo");
            var y = NodeIdentity.Of("foo");

            x.GetHashCode().Should().Be(y.GetHashCode());
        }

        [Fact]
        public void HashCodeNegativeTest() {
            var x = NodeIdentity.Of("foo");
            var y = NodeIdentity.Of("bar");

            x.GetHashCode().Should().NotBe(y.GetHashCode());
        }
    }
}
