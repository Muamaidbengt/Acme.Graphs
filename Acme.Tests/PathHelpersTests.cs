using Acme.Tests.TestHelpers;
using Acme.Graphs.Helpers;
using Xunit;
using FluentAssertions;
using Acme.Graphs;

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

        [Fact]
        public void PathOverAbcContainsAb() {
            var path = GraphFactory.CreatePath("A", "B", "C");
            path.ContainsExactly(new[] { GraphFactory.CreateEdge("A-B") }).Should().BeTrue();
        }

        [Fact]
        public void PathOverAbcDoesNotContainBa() {
            var path = GraphFactory.CreatePath("A", "B", "C");
            path.ContainsExactly(new[] { GraphFactory.CreateEdge("B-A") }).Should().BeFalse();
        }

        [Fact]
        public void PathOverAbcDoesNotContainBb() {
            var path = GraphFactory.CreatePath("A", "B", "C");
            path.ContainsExactly(new[] { GraphFactory.CreateEdge("B-B") }).Should().BeFalse();
        }
    }
}
