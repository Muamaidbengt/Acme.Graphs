using Acme.Graphs;
using Acme.Graphs.Helpers;
using FluentAssertions;
using System;
using Xunit;

namespace Acme.Tests {
    public class ObjectReferenceHelpersTests {
        [Fact]
        public void InstanceHasSameRefId() {
            var p1 = NodeIdentity.Of("test");
            var firstRefId = p1.GetRefId();
            var secondRefId = p1.GetRefId();

            secondRefId.Should().Be(firstRefId);
        }

        [Fact]
        public void TwoPointersToSameInstanceHasSameRefId() {
            var p1 = NodeIdentity.Of("test");
            var p2 = p1;

            p2.GetRefId().Should().Be(p1.GetRefId());
        }

        [Fact]
        public void TwoInstancesWithSameValuesHaveDifferentRefId() {
            var p1 = NodeIdentity.Of("test");
            var p2 = NodeIdentity.Of("test");

            p2.GetRefId().Should().NotBe(p1.GetRefId());
        }

        [Fact]
        public void NullValueHasEmptyGuidRefId() {
            var p1 = (NodeIdentity)null;

            p1.GetRefId().Should().Be(Guid.Empty);
        }
    }
}
