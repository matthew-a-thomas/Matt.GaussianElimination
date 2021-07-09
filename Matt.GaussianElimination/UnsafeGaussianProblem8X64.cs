namespace Matt.GaussianElimination
{
    using System.Collections.Generic;

    /// <summary>
    /// A <see cref="IGaussianProblem"/> whose solution is eight sixty-four bit values.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The rows are pointers to values that you maintain (unsafely).
    /// </para>
    /// </remarks>
    public sealed unsafe class UnsafeGaussianProblem8X64 : IGaussianProblem
    {
        readonly List<BytePointer> _coefficients = new();
        readonly List<ULongPointer> _data = new();

        public int NumCoefficients => 8;

        public int NumRows => _data.Count;

        public void Add(byte* coefficients, ulong* datum)
        {
            _coefficients.Add(new BytePointer(coefficients));
            _data.Add(new ULongPointer(datum));
        }

        public bool HasCoefficient(int row, int coefficient) => ((*_coefficients[row].Pointer >> (7 - coefficient)) & 0x1) == 0x1;

        public void Xor(int from, int to)
        {
            *_coefficients[to].Pointer ^= *_coefficients[from].Pointer;
            *_data[to].Pointer ^= *_data[from].Pointer;
        }
    }
}