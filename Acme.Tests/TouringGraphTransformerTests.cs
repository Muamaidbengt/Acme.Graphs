using Acme.Graphs;
using Acme.Graphs.Strategies;
using Acme.Tests.TestHelpers;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Acme.Tests {
    public class TouringGraphTransformerTests {
        /* Example from Anurag Dwarakanath, Aruna Jankiti. 
         * Minimum Number of Test Paths for Prime Path and Other
         * Structural Coverage Criteria, section 3.2 */
        [Fact]
        public void ExampleFromPaper() {
            var graph = CreateGraph();
            var paths = CreatePaths();
            var xform = TouringGraphTransformer
                .Transform(graph, paths, NodeIdentity.Of("S"), NodeIdentity.Of("T"));

            xform.EdgeCount.Should().Be(17);
        }

        private DirectedGraph CreateGraph() {
            return GraphFactory.BuildGraph("S-1", "1-2", "2-T",
                "1-3", "3-4", "4-1", "4-5", "5-4");
        }

        private IEnumerable<Path> CreatePaths() {
            return new[] {
                GraphFactory.CreatePath("S", "1", "3", "4", "5"), // p0
                GraphFactory.CreatePath("3", "4", "1", "2", "T"), // p1
                GraphFactory.CreatePath("5", "4", "1", "2", "T"), // p2
                GraphFactory.CreatePath("1", "3", "4", "1"),      // p3
                GraphFactory.CreatePath("S", "1", "2", "T"),      // p4
                GraphFactory.CreatePath("3", "4", "1", "3"),      // p5
                GraphFactory.CreatePath("5", "4", "1", "3"),      // p6
                GraphFactory.CreatePath("4", "1", "3", "4"),      // p7
                GraphFactory.CreatePath("5", "4", "5"),           // p8
                GraphFactory.CreatePath("4", "5", "4")            // p9
            };
        }
    }
}
