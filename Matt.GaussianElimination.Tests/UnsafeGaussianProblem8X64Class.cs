namespace Matt.GaussianElimination.Tests
{
    using Xunit;

    public class UnsafeGaussianProblem8X64Class
    {
        public class XorMethodShould
        {
            [Fact]
            public unsafe void AffectDataCorrectly()
            {
                var problem = new UnsafeGaussianProblem8X64();
                byte coefficients0 = 0, coefficients1 = 0;
                ulong datum0 = 0xa5a5a5a5_5a5a5a5aUL, datum1 = 0x5a5a5a5a_a5a5a5a5UL;
                problem.Add(&coefficients0, &datum0);
                problem.Add(&coefficients1, &datum1);

                problem.Xor(1, 0);

                Assert.Equal(~0UL, datum0);
                Assert.Equal(0x5a5a5a5a_a5a5a5a5UL, datum1);
            }
        }

        public class HasCoefficientMethodShould
        {
            [Fact]
            public unsafe void ReflectAddedCoefficients()
            {
                var problem = new UnsafeGaussianProblem8X64();
                byte coefficients = 0xaf;
                ulong datum = 0;
                problem.Add(&coefficients, &datum);

                Assert.True(problem.HasCoefficient(0, 0));
                Assert.False(problem.HasCoefficient(0, 1));
                Assert.True(problem.HasCoefficient(0, 2));
                Assert.False(problem.HasCoefficient(0, 3));
                Assert.True(problem.HasCoefficient(0, 4));
                Assert.True(problem.HasCoefficient(0, 5));
                Assert.True(problem.HasCoefficient(0, 6));
                Assert.True(problem.HasCoefficient(0, 7));
            }
        }
    }
}