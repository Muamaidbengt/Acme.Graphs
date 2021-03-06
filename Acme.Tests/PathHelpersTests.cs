﻿using Acme.Graphs;
using Acme.Graphs.Helpers;
using Acme.Tests.TestHelpers;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Acme.Tests {
    public class PathHelpersTests {
        [Fact]
        public void AnEmptyPathIsNotACycle() {
            var path = GraphFactory.CreateEdges();
            path.DescribesCycle().Should().BeFalse();
        }

        [Fact]
        public void ANodeLeadingToItselfIsACycle() {
            var path = GraphFactory.CreateEdges("A-A");
            path.DescribesCycle().Should().BeTrue();
        }

        [Fact]
        public void PathLongerThan2WhichEndsWhereItStartsIsACycle() {
            var path = GraphFactory.CreateEdges("A-B", "B-C", "C-A");
            path.DescribesCycle().Should().BeTrue();
        }

        [Fact]
        public void PathContainingACycleIsNotACycle() {
            var path = GraphFactory.CreateEdges("A-B", "B-C", "C-A", "A-D");
            path.DescribesCycle().Should().BeFalse();
        }

        [Fact]
        public void PathVisitsItsStartNode() {
            var path = GraphFactory.CreateEdges("A-B", "B-C", "C-D", "D-E");
            path.Visits(NodeIdentity.Of("A")).Should().BeTrue();
        }

        [Fact]
        public void PathVisitsItsEndNode() {
            var path = GraphFactory.CreateEdges("A-B", "B-C", "C-D", "D-E");
            path.Visits(NodeIdentity.Of("E")).Should().BeTrue();
        }

        [Fact]
        public void PathVisitsNodeInMiddle() {
            var path = GraphFactory.CreateEdges("A-B", "B-C", "C-D", "D-E");
            path.Visits(NodeIdentity.Of("C")).Should().BeTrue();
        }

        [Fact]
        public void PathDoesNotVisitOtherNodes() {
            var path = GraphFactory.CreateEdges("A-B", "B-C", "C-D", "D-E");
            path.Visits(NodeIdentity.Of("Q")).Should().BeFalse();
        }

        [Theory]
        [InlineData("ABCD", "BC")]
        [InlineData("AABCDD", "BC")]
        [InlineData("AABCDD", "ABCD")]
        [InlineData("ABC", "BC")]
        [InlineData("ABC", "AB")]
        [InlineData("ABC", "ABC")]
        [InlineData("ABC", "")]
        public void PathContains(string first, string other) {
            var firstPath = CreatePath(first);
            var otherPath = CreatePath(other);
            firstPath.Contains(otherPath).Should().BeTrue();
            var otherEdges = CreateEdges(other);
            firstPath.Contains(otherEdges).Should().BeTrue();
        }

        [Theory]
        [InlineData("ABC", "CD")]
        [InlineData("ABC", "AC")]
        [InlineData("", "AB")]
        public void PathDoesNotContain(string first, string other) {
            var firstPath = CreatePath(first);
            var otherPath = CreatePath(other);
            firstPath.Contains(otherPath).Should().BeFalse();
            var otherEdges = CreateEdges(other);
            firstPath.Contains(otherEdges).Should().BeFalse();
        }

        [Theory]
        [InlineData("ABC", "BCD", "ABCD")]
        [InlineData("ABC", "CD", "ABCD")]
        [InlineData("AB", "CD", "ABCD")]
        [InlineData("AB", "D", "ABCD")]
        [InlineData("A", "D", "AD")]
        public void Extend(string from, string to, string expected) {
            var graph = GraphFactory.BuildGraph("A-B", "B-C", "C-D", "A-D");
            var fromPath = CreatePath(from);
            var toPath = CreatePath(to);
            var expectedPath = CreatePath(expected);

            object extension = fromPath.Extend(toPath, graph);
            extension.Should().Be(expectedPath);
        }

        private Path CreatePath(string sequence) =>
            GraphFactory.CreatePath(sequence.Select(letter => letter.ToString()).ToArray());

        private IEnumerable<DirectedEdge> CreateEdges(string sequence) {
            if (string.IsNullOrEmpty(sequence)) {
                yield break;
            }
            var prev = sequence[0];
            foreach (var c in sequence.Skip(1)) {
                yield return GraphFactory.CreateEdge(string.Format("{0}-{1}", prev, c));
                prev = c;
            }
        }
    }
}
