namespace Matt.GaussianElimination.Tests
{
    using System;
    using GaussianElimination;
    using Moq;
    using Xunit;

    public class GaussianSolverClass
    {
        public class SolveMethodShould
        {
            [Fact]
            public void ReturnFalseForIncompleteProblem()
            {
                var problem = new VectorizedGaussianProblem(10, 0);

                Assert.False(GaussianSolver.Solve(problem));
            }

            [Fact]
            public void ReturnTrueAndMakeNoModificationsToAlreadySolvedProblem()
            {
                var problem = new Mock<IGaussianProblem>();
                problem
                    .Setup(x => x.Xor(It.IsAny<int>(), It.IsAny<int>()))
                    .Throws(new Exception("The solver tried to XOR"));
                problem
                    .Setup(x => x.HasCoefficient(It.IsAny<int>(), It.IsAny<int>()))
                    .Returns<int, int>((row, coefficient) => row == coefficient);
                problem
                    .SetupGet(x => x.NumCoefficients)
                    .Returns(10);
                problem
                    .SetupGet(x => x.NumRows)
                    .Returns(10);

                Assert.True(GaussianSolver.Solve(problem.Object));
            }

            [Fact]
            public void SolveSolvableProblem()
            {
                var problem = new VectorizedGaussianProblem(2, 1);
                var coefficients0 = new [] { true, true };
                var row0 = new byte[] { 'a' ^ 'b' };
                problem.Add(
                    coefficients0,
                    row0
                );
                var coefficients1 = new [] { true, false };
                var row1 = new [] {(byte) 'a'};
                problem.Add(
                    coefficients1,
                    row1
                );

                Assert.True(GaussianSolver.Solve(problem));

                Assert.Equal<bool>(new [] { true, false }, coefficients0);
                Assert.Equal<bool>(new [] { false, true }, coefficients1);
                Assert.Equal<byte>(new [] { (byte)'a' }, row0);
                Assert.Equal<byte>(new [] { (byte)'b' }, row1);
            }
        }
    }
}