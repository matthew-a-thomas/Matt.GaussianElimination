namespace Matt.GaussianElimination
{
    /// <summary>
    /// A problem that can be solved with binary Gaussian Elimination.
    /// </summary>
    public interface IGaussianProblem
    {
        /// <summary>
        /// The number of binary coefficients in this problem. This is also the number of values in the solution.
        /// </summary>
        int NumCoefficients { get; }

        /// <summary>
        /// The number of rows in this problem.
        /// </summary>
        int NumRows { get; }

        /// <summary>
        /// Returns true if the indicated coefficient is set.
        /// </summary>
        bool HasCoefficient(int row, int coefficient);

        /// <summary>
        /// XORs the indicated row into the other indicated row.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The row indicated by <paramref name="from"/> is modified.
        /// </para>
        /// </remarks>
        void Xor(int from, int to);
    }
}