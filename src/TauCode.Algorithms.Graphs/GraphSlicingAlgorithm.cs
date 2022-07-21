using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TauCode.Algorithms.Abstractions;
using TauCode.Data.Graphs;

namespace TauCode.Algorithms.Graphs
{
    public class GraphSlicingAlgorithm : IAlgorithm<IGraph, IReadOnlyList<IGraph>>
    {
        private IGraph[] Slice()
        {
            if (this.Input == null)
            {
                throw new InvalidOperationException($"'{nameof(Input)}' is null.");
            }

            var list = new List<IGraph>();

            while (true)
            {
                var nodes = this.GetTopLevelNodes();
                if (nodes.Count == 0)
                {
                    if (this.Input.Any())
                    {
                        list.Add(this.Input);
                    }

                    break;
                }

                var slice = new Graph();
                slice.CaptureVerticesFrom(this.Input, nodes);
                list.Add(slice);
            }

            return list.ToArray();
        }

        private IReadOnlyList<IVertex> GetTopLevelNodes()
        {
            var result = new List<IVertex>();

            foreach (var node in this.Input)
            {
                // todo: unoptimized. efficiency ~ O(n^2)
                var outgoingEdges = node.GetOutgoingArcsLyingInGraph(this.Input);

                var isTopLevel = true;

                foreach (var outgoingEdge in outgoingEdges)
                {
                    if (outgoingEdge.Head == node)
                    {
                        // node referencing self, don't count - it still might be "top-level"
                        continue;
                    }

                    // node referencing another node, i.e. is not "top-level"
                    isTopLevel = false;
                    break;
                }

                if (isTopLevel)
                {
                    result.Add(node);
                }
            }

            return result;
        }

        public void Run()
        {
            var list = this.Slice();
            this.Output = list.ToArray();
        }

        public Task RunAsync(CancellationToken cancellationToken = default)
        {
            this.Run();
            return Task.CompletedTask;
        }

        public IGraph Input { get; set; }

        public IReadOnlyList<IGraph> Output { get; private set; }
    }
}
