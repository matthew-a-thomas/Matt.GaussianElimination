namespace Matt.GaussianElimination
{
    /// <summary>
    /// Solves certain kinds of problems with Gaussian Elimination.
    /// </summary>
    public static class GaussianSolver
    {
        /// <summary>
        /// Solves the given <paramref name="problem"/> using Gaussian Elimination.
        /// </summary>
        public static bool Solve<T>(T problem)
            where T : IGaussianProblem
        {
            var numCoefficients = problem.NumCoefficients;
            var numRows = problem.NumRows;

            if (numRows < numCoefficients)
                return false; // Not enough rows

            // First, get it into row echelon form
            for (var row = 0; row < numCoefficients; ++row)
            {
                // Ensure leading one
                if (!problem.HasCoefficient(row, row))
                {
                    // We need to set the coefficient. Because of the code below, and since we've made it this far, we
                    // know that no later rows have any of the previous coefficients set. That's important because it
                    // means we don't have to check for that after we XOR in a later row
                    var foundOtherRow = false;
                    for (var otherRow = row + 1; otherRow < numRows; ++otherRow)
                    {
                        if (!problem.HasCoefficient(otherRow, row))
                            continue;
                        problem.Xor(otherRow, row);
                        foundOtherRow = true;
                        break;
                    }
                    if (!foundOtherRow)
                        return false; // We are unable to get the problem into row echelon form
                }

                // Wipe out later rows' coefficients in this position
                for (var otherRow = row + 1; otherRow < numRows; ++otherRow)
                {
                    if (problem.HasCoefficient(otherRow, row))
                        problem.Xor(row, otherRow);
                }
            }

            // Now we wipe out the dangling non-zeros and we're done!
            for (var row = 0; row < numCoefficients; ++row)
            {
                for (var coefficient = row + 1; coefficient < numCoefficients; ++coefficient)
                {
                    if (problem.HasCoefficient(row, coefficient))
                        problem.Xor(coefficient, row);
                }
            }

            return true;
        }
    }
}