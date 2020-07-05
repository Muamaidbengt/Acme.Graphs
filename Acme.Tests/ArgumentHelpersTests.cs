using Acme.Graphs;
using FluentAssertions;
using System;
using Xunit;

namespace Acme.Tests {
    public class ArgumentHelpersTests {
        [Fact]
        public void DoesNotThrowIfExpressionIsNotNull() {
            typeof(ArgumentHelpers).Invoking(_ =>
                ArgumentHelpers.ThrowIfNull(() => "not null"))
                .Should().NotThrow();
        }

        [Fact]
        public void ThrowsIfExpressionIsNull() {
            typeof(ArgumentHelpers).Invoking(_ =>
                ArgumentHelpers.ThrowIfNull<object>(() => null))
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("() => null");
        }

        [Fact]
        public void IndicatesParameterByNameIfNull() {
            string nullParam = null;
            typeof(ArgumentHelpers).Invoking(_ =>
                ArgumentHelpers.ThrowIfNull(() => nullParam))
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be(nameof(nullParam));
        }
    }
}
