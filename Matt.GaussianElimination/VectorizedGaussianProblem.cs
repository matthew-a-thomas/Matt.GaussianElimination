using Matt.Accelerated;

namespace Matt.GaussianElimination
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class VectorizedGaussianProblem : IGaussianProblem
    {
        readonly List<Memory<bool>> _coefficients = new();
        readonly List<Memory<byte>> _rows = new();
        readonly int _rowWidth;

        public VectorizedGaussianProblem(int numCoefficients, int rowWidth)
        {
            _rowWidth = rowWidth;
            NumCoefficients = numCoefficients;
        }

        public int NumCoefficients { get; }

        public int NumRows => _rows.Count;

        public void Add(
            Memory<bool> coefficients,
            Memory<byte> row)
        {
            if (coefficients.Length != NumCoefficients)
                throw new Exception();
            if (row.Length != _rowWidth)
                throw new Exception();
            _coefficients.Add(coefficients);
            _rows.Add(row);
        }

        public bool HasCoefficient(int row, int coefficient) => _coefficients[row].Span[coefficient];

        public void Xor(int from, int to)
        {
            Bitwise.Xor(
                MemoryMarshal.AsBytes(_coefficients[from].Span),
                MemoryMarshal.AsBytes(_coefficients[to].Span)
            );

            Bitwise.Xor(
                _rows[from].Span,
                _rows[to].Span
            );
        }
    }
}