using NUnit.Framework;
using TauCode.Data.Graphs;

namespace TauCode.Algorithms.Graphs.Tests;

[TestFixture]
public abstract class GraphTestBase
{
    protected IGraph Graph { get; set; }

    [SetUp]
    public void SetUpBase()
    {
        this.Graph = new Graph();
    }

    [TearDown]
    public void TearDownBase()
    {
        this.Graph = null!;
    }
}
