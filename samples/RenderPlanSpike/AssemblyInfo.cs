using System.Runtime.CompilerServices;

// let the golden tests + benchmarks reach the generated internal Views__Optimized class
[assembly: InternalsVisibleTo("CC.CSX.RenderPlan.Tests")]
[assembly: InternalsVisibleTo("CC.CSX.Benchmarks")]
