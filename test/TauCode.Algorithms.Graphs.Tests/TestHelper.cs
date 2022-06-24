using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TauCode.Data.Graphs;

// todo clean
namespace TauCode.Algorithms.Graphs.Tests
{
    internal static class TestHelper
    {
        internal static IArc[] LinkTo(this IVertex node, params IVertex[] otherNodes)
        {
            var list = new List<IArc>();

            foreach (var otherNode in otherNodes)
            {
                var edge = new Arc();
                edge.Connect(node, otherNode);
                list.Add(edge);
            }

            return list.ToArray();

            //return otherNodes
            //    .Select(node.DrawEdgeTo)
            //    .ToArray();
        }

        internal static IVertex GetNode(this IGraph graph, string nodeValue)
        {
            return graph.Single(x => x.Name == nodeValue);
        }

        internal static void AssertNode(
            this IGraph graph,
            IVertex node,
            IVertex[] linkedToNodes,
            IArc[] linkedToEdges,
            IVertex[] linkedFromNodes,
            IArc[] linkedFromEdges)
        {
            if (linkedToNodes.Length != linkedToEdges.Length)
            {
                throw new ArgumentException();
            }

            if (linkedFromNodes.Length != linkedFromEdges.Length)
            {
                throw new ArgumentException();
            }

            Assert.That(graph.Contains(node), Is.True);

            // check 'outgoing' edges
            Assert.That(node.GetOutgoingArcsLyingInGraph(graph).Count, Is.EqualTo(linkedToNodes.Length));

            foreach (var outgoingEdge in node.GetOutgoingArcsLyingInGraph(graph))
            {
                Assert.That(outgoingEdge.Tail, Is.EqualTo(node));

                var head = outgoingEdge.Head;
                Assert.That(graph.Contains(head), Is.True);
                Assert.That(head.IncomingArcs, Does.Contain(outgoingEdge));

                var index = Array.IndexOf(linkedToNodes, head);
                Assert.That(index, Is.GreaterThanOrEqualTo(0));
                Assert.That(outgoingEdge, Is.SameAs(linkedToEdges[index]));
            }

            // check 'incoming' edges
            Assert.That(node.GetIncomingArcsLyingInGraph(graph).Count, Is.EqualTo(linkedFromNodes.Length));

            foreach (var incomingEdge in node.GetIncomingArcsLyingInGraph(graph))
            {
                Assert.That(incomingEdge.Head, Is.EqualTo(node));

                var tail = incomingEdge.Tail;
                Assert.That(graph.Contains(tail), Is.True);
                Assert.That(tail.OutgoingArcs, Does.Contain(incomingEdge));

                var index = Array.IndexOf(linkedFromNodes, tail);
                Assert.That(index, Is.GreaterThanOrEqualTo(0));
                Assert.That(incomingEdge, Is.SameAs(linkedFromEdges[index]));
            }
        }

        internal static IVertex AddNamedNode(this IGraph graph, string name)
        {
            var vertex = new Vertex
            {
                Name = name,
            };

            graph.Add(vertex);
            return vertex;
        }
    }
}
